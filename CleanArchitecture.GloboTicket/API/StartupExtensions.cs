using System.Reflection;
using Infrastructure.ServiceCollectionExtensions;
using Persistence.ServiceCollectionExtensions;
using Application;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace API;

public static class StartupExtensions
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.RegisterApplicationServices();
        builder.Services.RegisterInfrastructureServices(builder.Configuration);
        builder.Services.RegisterPersistenceServices(builder.Configuration);
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

        return builder;
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseCors("open");
        app.UseHttpsRedirection();
        app.MapControllers();

        return app;
    }

    // public static WebApplication ConfigureSwagger(this WebApplication app)
    // {
    //     app.UseSwagger();
    //     app.UseSwaggerUI(c =>
    //     {
    //         c.SwaggerEndpoint("/swagger/v1/swagger.json", "GloboTicket API V1");
    //         c.RoutePrefix = string.Empty;
    //     });
    //
    //     return app;
    // }

    public static async Task ResetDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        try
        {
            var serviceProvider = scope.ServiceProvider;
            var context = serviceProvider.GetRequiredService<GloboTickerDbContext>();
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