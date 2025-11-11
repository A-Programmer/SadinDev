using FluentValidation;

namespace KSProject.Application.Wallets.ChargeWallet
{
    public sealed class ChargeWalletCommandValidator : AbstractValidator<ChargeWalletCommand>
    {
        public ChargeWalletCommandValidator()
        {
            RuleFor(c => c.Payload.UserId)
                .NotEmpty()
                .WithMessage("UserId cannot be empty.");

            RuleFor(c => c.Payload.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than zero.");
        }
    }
}
