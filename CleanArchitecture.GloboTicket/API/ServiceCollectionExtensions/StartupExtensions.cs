using System.Security.Claims;
using API.Middleware;
using API.Services;
using Application;
using Application.Contracts.Api;
using Identity;
using Identity.Models;
using Infrastructure.ServiceCollectionExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.ServiceCollectionExtensions;
using Serilog;

namespace API.ServiceCollectionExtensions;

public static class StartupExtensions
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, services, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .WriteTo.Console();
        });
        
        builder.Services.RegisterIdentityServices(builder.Configuration);
        builder.Services.RegisterApplicationServices();
        builder.Services.RegisterInfrastructureServices(builder.Configuration);
        builder.Services.RegisterPersistenceServices(builder.Configuration);
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();
        builder.Services.AddOpenApiDocument();

        builder.Services.AddControllers();

        builder.Services.AddCors(
            options =>
                options.AddPolicy(
                    "open",
                    policy =>
                        policy.WithOrigins(builder.Configuration["ApiUrl"] ?? "https://localhost:7020",
                                builder.Configuration["BlazorUrl"] ?? "https://localhost:7080")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            // .AllowAnyOrigin()
                            .SetIsOriginAllowed(p => true)
                            .AllowCredentials()
                ));
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.MapIdentityApi<ApplicationUser>();
        // app.MapPost("/Logout", async (ClaimsPrincipal user, SignInManager<ApplicationUser> signInManager) =>
        // {
        //     await signInManager.SignOutAsync();
        //     return TypedResults.Ok();
        // });
        
        app.UseSerilogRequestLogging();
        app.UseCors("open");
        app.UseHttpsRedirection();
        
        app.UseCustomMiddlewareHandler();
        
        app.MapControllers();
        if (app.Environment.IsDevelopment())
        {
            ConfigureSwagger(app);
        }

        return app;
    }

    private static WebApplication ConfigureSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "GloboTicket API V1");
            c.RoutePrefix = string.Empty;
        });
    
        return app;
    }

    public static async Task ResetDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        try
        {
            var serviceProvider = scope.ServiceProvider;
            var context = serviceProvider.GetRequiredService<GloboTicketDbContext>();
            if (context != null)
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.MigrateAsync();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}