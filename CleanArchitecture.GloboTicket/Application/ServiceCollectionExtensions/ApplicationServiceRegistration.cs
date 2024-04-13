using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(typeof(ApplicationLibrary).Assembly);


        serviceCollection.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())); // AppDomain.CurrentDomain.GetAssemblies()  typeof(ApplicationLibrary).Assembly
        
        return serviceCollection;
    }
}