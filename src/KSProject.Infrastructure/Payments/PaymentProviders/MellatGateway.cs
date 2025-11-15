using KSProject.Domain.Contracts;
using KSProject.Domain.Dtos;
using Microsoft.Extensions.Configuration;

namespace KSProject.Infrastructure.Payments.PaymentProviders;

public class MellatGateway : IPaymentGateway
{
    // private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public MellatGateway(
        // IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        // _httpClient = httpClientFactory.CreateClient("mellatHttpClient");
        _configuration = configuration;
    }

    public async Task<PaymentResult> ProcessPaymentAsync(decimal amount, Guid userId, string callBackUrl)
    {
        // Logic ملت – فرض API call
        // var response = await _httpClient.PostAsync("mellat/api/pay", new StringContent($"terminalId={_terminalId}&amount={amount}&callback={callBackUrl}"));
        // var result = await response.Content.ReadAsStringAsync();
        // Parse result
        var terminalId = _configuration["PaymentGateways:Mellat:TerminalId"];
        return new PaymentResult(true, "mellat-trans-id", "redirect-url");
    }

    public async Task<PaymentResult> VerifyPaymentAsync(string paymentId, decimal amount)
    {
        // Logic verify
        return new PaymentResult(true, paymentId);
    }
}
