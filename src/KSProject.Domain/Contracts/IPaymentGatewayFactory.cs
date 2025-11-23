using KSProject.Common.Constants.Enums;

namespace KSProject.Domain.Contracts;

public interface IPaymentGatewayFactory
{
    IPaymentGateway GetGateway(PaymentGatewayTypes gatewayType);
}
