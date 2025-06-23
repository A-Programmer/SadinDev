using System.Reflection;
using KSFramework.KSMessaging.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace KSProject.Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.AddKSFramework(AssemblyReference.Assembly);
        return services;
    }

    public static WebApplication UseApplication(this WebApplication app)
    {
        return app;
    }
    
    private static void RegisterAllImplementationsOf<TInterface>(this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Scoped,
        params Assembly[] assemblies)
    {
        var interfaceType = typeof(TInterface);

        foreach (var assembly in assemblies)
        {
            var implementations = assembly.DefinedTypes
                .Where(type => type.IsClass && !type.IsAbstract && interfaceType.IsAssignableFrom(type))
                .ToList();

            foreach (var implementation in implementations)
            {
                switch (lifetime)
                {
                    case ServiceLifetime.Transient:
                        services.AddTransient(implementation);
                        break;
                    case ServiceLifetime.Scoped:
                        services.AddScoped(implementation);
                        break;
                    case ServiceLifetime.Singleton:
                        services.AddSingleton(implementation);
                        break;
                    default:
                        throw new ArgumentException("Invalid service lifetime", nameof(lifetime));
                }
            }
        }
    }
}
