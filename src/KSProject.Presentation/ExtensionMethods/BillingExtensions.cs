using KSProject.Presentation.Filters;
using KSProject.Presentation.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace KSProject.Presentation.ExtensionMethods;

public static class BillingExtensions
{
    public static IApplicationBuilder UseBillingCheck(this IApplicationBuilder app)
    {
        app.UseMiddleware<BillingCheckMiddleware>();
        return app;
    }

    public static IServiceCollection AddUsageDebitFilter(this IServiceCollection services)
    {
        services.AddScoped<UsageDebitFilter>();
        return services;
    }
}
