using KSFramework.KSMessaging.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace KSProject.Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.AddKSMediator(AssemblyReference.Assembly);
        return services;
    }

    public static WebApplication UseApplication(this WebApplication app)
    {
        return app;
    }
}
