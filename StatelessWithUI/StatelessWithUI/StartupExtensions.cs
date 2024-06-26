using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using StatelessWithUI.Application.Contracts;
using StatelessWithUI.Application.Services;
using StatelessWithUI.Application.VehicleStateMachineFactory;
using StatelessWithUI.Persistence;
using StatelessWithUI.Persistence.Repositories;

namespace StatelessWithUI;

public static class StartupExtensions
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        // builder.Host.UseSerilog((context, services, configuration) =>
        // {
        //     configuration
        //         .ReadFrom.Configuration(context.Configuration)
        //         .ReadFrom.Services(services)
        //         .Enrich.FromLogContext()
        //         .WriteTo.Console();
        // });
        
        SetupDatabase(builder.Services);
        builder.Services.AddScoped<ICarService, CarService>();
        builder.Services.AddScoped<ITaskService, TaskService>();
        builder.Services.AddScoped<IPlaneService, PlaneService>();
        builder.Services.AddScoped<IStateService, StateService>();
        builder.Services.AddScoped<IPlaneStateMachineService, PlaneStateMachineService>();
        builder.Services.AddScoped<ICarRepository, CarRepository>();
        builder.Services.AddScoped<IPlaneRepository, PlaneRepository>();
        builder.Services.AddScoped<IPlaneStateRepository, PlaneStateRepository>();
        builder.Services.AddScoped<IBuildTaskRepository, BuildTaskRepository>();
        builder.Services.AddSingleton<IVehicleFactory, VehicleFactory>();
        builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddControllers()
            .AddJsonOptions(
            o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
        )
            ;
        builder.Services.AddCors(
            options =>
                options.AddPolicy(
                    "open",
                    policy =>
                        policy.WithOrigins(builder.Configuration["ApiUrl"] ?? "https://localhost:4200")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .SetIsOriginAllowed(p => true)
                            .AllowCredentials()
                ));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }


    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseHsts();
        }

        app.UseCors("open");
        app.UseHttpsRedirection();
        // app.UseStaticFiles();
        // app.UseRouting();

        // app.UseCustomMiddlewareHandler();

        app.MapControllers();
        if (app.Environment.IsDevelopment())
        {
            ConfigureSwagger(app);
        }

        app.MapGet("/", context =>
        {
            context.Response.Redirect("./swagger/index.html", permanent: false);
            return Task.FromResult(0);
        });

        return app;
    }

    private static WebApplication ConfigureSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "StateMachine API V1");
            c.RoutePrefix = string.Empty;
        });

        return app;
    }

    private static void SetupDatabase(IServiceCollection services)
    {
        services.AddDbContext<VehicleDbContext>();
        var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var dbContext = scopedServices.GetRequiredService<VehicleDbContext>();
        dbContext.Database.EnsureCreated();
    }
}