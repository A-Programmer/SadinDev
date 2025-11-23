using KSProject.Domain.Contracts;
using KSProject.Domain.Dtos;
using Microsoft.Extensions.Configuration;

namespace KSProject.Infrastructure.Payments.PaymentProviders;

public class ZarinPalGateway : IPaymentGateway
{
    private readonly IConfiguration _configuration;

    public ZarinPalGateway(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<PaymentResult> ProcessPaymentAsync(decimal amount, Guid userId, string callBackUrl)
    {
        // // Logic زرین‌پال
        var merchantId = _configuration["PaymentGateways:ZarinPal:MerchantId"];
        // var payment = new Payment(_merchantId, (int)amount);
        // var result = await payment.PaymentRequest("Charge Wallet for User " + userId, callBackUrl);
        // if (result.Status == 100)
        return new PaymentResult(true, "Transaction-Id", $"https://www.zarinpal.com/pg/StartPay/TransactionId");
        // return new PaymentResult(false, null, null, result.Status.ToString());
    }

    public async Task<PaymentResult> VerifyPaymentAsync(string paymentId)
    {
        throw new NotImplementedException();
    }

    public async Task<PaymentResult> VerifyPaymentAsync(string paymentId, decimal amount)
    {
        // var payment = new Payment(_merchantId, (int)amount);
        // var result = await payment.Verification(paymentId);
        // return new PaymentResult(result.Status == 100, result.RefId, null, result.Status.ToString());
        return new PaymentResult(true, "Transaction-Id", "Redirecy-Url", "");
    }
}
