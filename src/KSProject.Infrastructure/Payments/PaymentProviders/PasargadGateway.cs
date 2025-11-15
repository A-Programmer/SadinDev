using KSProject.Domain.Contracts;
using KSProject.Domain.Dtos;
using Microsoft.Extensions.Configuration;

namespace KSProject.Infrastructure.Payments.PaymentProviders;

public class PasargadGateway : IPaymentGateway
{
    private readonly IConfiguration _configuration;
    // private readonly HttpClient _httpClient;

    public PasargadGateway(
        // IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _configuration = configuration;
        // _httpClient = httpClientFactory.CreateClient("pasargadGateway");
    }

    public async Task<PaymentResult> ProcessPaymentAsync(decimal amount, Guid userId, string callBackUrl)
    {
        // Logic ملت – فرض API call
        // var response = await _httpClient.PostAsync("pasargad/api/pay", new StringContent($"terminalId={_terminalId}&amount={amount}&callback={callBackUrl}"));
        // var result = await response.Content.ReadAsStringAsync();
        // Parse result
        var terminalId = _configuration["PaymentGateways:Pasargad:TerminalId"];
        return new PaymentResult(true, "pasargad-trans-id", "redirect-url");
    }

    public async Task<PaymentResult> VerifyPaymentAsync(string paymentId, decimal amount)
    {
        // Logic verify
        return new PaymentResult(true, paymentId);
    }
}
