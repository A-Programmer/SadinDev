using FluentValidation;

namespace KSProject.Application.Wallets.DebitWallet;

public sealed class DebitWalletCommandValidator : AbstractValidator<DebitWalletCommand>
{
    public DebitWalletCommandValidator()
    {
        RuleFor(c => c.Payload.UserId)
            .NotEmpty()
            .WithMessage("UserId cannot be empty.");

        RuleFor(c => c.Payload.Amount)
            .LessThan(0)
            .WithMessage("Amount must be negative for debit.");

        RuleFor(c => c.Payload.TransactionType)
            .IsInEnum()
            .WithMessage("Invalid TransactionType.");
    }
}
