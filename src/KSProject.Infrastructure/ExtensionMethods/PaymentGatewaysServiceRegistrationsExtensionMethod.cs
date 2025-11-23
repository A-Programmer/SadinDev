using KSProject.Domain.Contracts;
using KSProject.Infrastructure.Payments;
using KSProject.Infrastructure.Payments.PaymentProviders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KSProject.Infrastructure.ExtensionMethods;

public static class PaymentGatewaysServiceRegistrationsExtensionMethod
{
    public static IServiceCollection RegisterPaymentGateways(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPaymentGatewayFactory, PaymentGatewayFactory>();
        services.AddHttpClient("mellatHttpClient", c =>
        {
            c.BaseAddress = new Uri(configuration["PaymentGateways:Mellat:BaseUrl"]);
            c.DefaultRequestHeaders.Add("Accept", "application/json");
        });
        
        services.AddHttpClient("pasargardHttpClient", c =>
        {
            c.BaseAddress = new Uri(configuration["PaymentGateways:Pasargad:BaseUrl"]);
            c.DefaultRequestHeaders.Add("Accept", "application/json");
        });
        
        services.AddHttpClient("nowPaymentHttpClient", c =>
        {
            c.BaseAddress = new Uri(configuration["PaymentGateways:Crypto:BaseUrl"]);
            c.DefaultRequestHeaders.Add("Accept", "application/json");
        });
        
        services.AddHttpClient("zarinPalHttpClient", c =>
        {
            c.BaseAddress = new Uri(configuration["PaymentGateways:ZarinPal:BaseUrl"]);
            c.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        services.AddScoped<PaymentGatewayFactory>();
        services.AddScoped<ZarinPalGateway>();
        services.AddScoped<MellatGateway>();
        services.AddScoped<PasargadGateway>();
        
        return services;
    }
}
