using KSProject.Common.Constants.Enums;
using KSProject.Domain.Contracts;
using KSProject.Infrastructure.Payments.PaymentProviders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KSProject.Infrastructure.Payments;

public class PaymentGatewayFactory : IPaymentGatewayFactory
{
    private readonly IConfiguration _configuration;

    public PaymentGatewayFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IPaymentGateway GetGateway(PaymentGatewayTypes gatewayType)
    {
        return gatewayType switch
        {
            PaymentGatewayTypes.ZarinPal => new ZarinPalGateway(_configuration),
            PaymentGatewayTypes.Mellat => new MellatGateway(_configuration),
            PaymentGatewayTypes.Pasargad => new PasargadGateway(_configuration),
            PaymentGatewayTypes.Crypto => new CryptoGateway(_configuration),
            _ => throw new ArgumentException("Invalid gateway type.")
        };
    }
}
