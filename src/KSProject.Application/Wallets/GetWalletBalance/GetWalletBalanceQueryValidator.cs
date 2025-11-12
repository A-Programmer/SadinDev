using FluentValidation;

namespace KSProject.Application.Wallets.GetWalletBalance
{
    public sealed class GetWalletBalanceQueryValidator : AbstractValidator<GetWalletBalanceQuery>
    {
        public GetWalletBalanceQueryValidator()
        {
            RuleFor(q => q.UserId)
                .NotEmpty()
                .WithMessage("UserId cannot be empty.");
        }
    }
}
