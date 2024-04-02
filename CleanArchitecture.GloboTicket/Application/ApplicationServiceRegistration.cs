using System.Reflection.Metadata;
using Application.Models.Mail;
using Application.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(typeof(ApplicationLibrary).Assembly);
        serviceCollection.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(typeof(ApplicationLibrary).Assembly));

        return serviceCollection;
    }
}