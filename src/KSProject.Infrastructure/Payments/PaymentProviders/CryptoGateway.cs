using KSProject.Domain.Contracts;
using KSProject.Domain.Dtos;
using Microsoft.Extensions.Configuration;

namespace KSProject.Infrastructure.Payments.PaymentProviders;

public class CryptoGateway : IPaymentGateway
{
    private readonly IConfiguration _configuration;
    // private readonly HttpClient _httpClient;

    public CryptoGateway(
        // string apiKey,
        // IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _configuration = configuration;
        // _httpClient = httpClientFactory.CreateClient("nowPaymentHttpClient");
    }

    public async Task<PaymentResult> ProcessPaymentAsync(decimal amount, Guid userId, string callBackUrl)
    {
        // Logic NowPayments – API call
        // var response = await _httpClient.PostAsync("nowpayments/v1/payment", new StringContent($"api_key={_apiKey}&amount={amount}&currency=BTC&callback={callBackUrl}"));
        // var result = await response.Content.ReadAsStringAsync();
        // Parse
        var apiKey = _configuration["PaymentGatewayPaymentGateways:Crypto:ApiKey"];
        return new PaymentResult(true, "crypto-id", "wallet-address");
    }

    public async Task<PaymentResult> VerifyPaymentAsync(string paymentId, decimal amount)
    {
        // Logic verify
        return new PaymentResult(true, paymentId);
    }
}
