using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace KSProject.Domain;

public static class DependencyInjection
{
    public static IServiceCollection RegisterDomain(this IServiceCollection services)
    {
        return services;
    }

    public static WebApplication UseDomain(this WebApplication app)
    {
        return app;
    }
}