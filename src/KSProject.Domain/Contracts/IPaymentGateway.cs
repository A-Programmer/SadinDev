using KSProject.Domain.Dtos;

namespace KSProject.Domain.Contracts;

public interface IPaymentGateway
{
    Task<PaymentResult> ProcessPaymentAsync(decimal amount, Guid userId, string callBackUrl);
    Task<PaymentResult> VerifyPaymentAsync(string paymentId, decimal amount);
}
