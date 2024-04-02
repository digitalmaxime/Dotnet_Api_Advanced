using Infrastructure.ServiceCollectionExtensions;
using Persistence.ServiceCollectionExtensions;
using Application;

namespace API;

public static class StartupExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.RegisterApplicationServices();
        builder.Services.RegisterPersistenceServices(builder.Configuration);
        builder.Services.RegisterInfrastructureServices(builder.Configuration);
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

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseCors("open");
        app.UseHttpsRedirection();
        app.MapControllers();

        return app;
    }
}