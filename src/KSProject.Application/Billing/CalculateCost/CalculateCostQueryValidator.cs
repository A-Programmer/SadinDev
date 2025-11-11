using FluentValidation;

namespace KSProject.Application.Billing.CalculateCost
{
    public sealed class CalculateCostQueryValidator : AbstractValidator<CalculateCostQuery>
    {
        public CalculateCostQueryValidator()
        {
            RuleFor(q => q.Payload.ServiceType)
                .NotEmpty()
                .WithMessage("ServiceType cannot be empty.");

            RuleFor(q => q.Payload.MetricType)
                .NotEmpty()
                .WithMessage("MetricType cannot be empty.");

            RuleFor(q => q.Payload.MetricValue)
                .GreaterThanOrEqualTo(0)
                .WithMessage("MetricValue must be non-negative.");
        }
    }
}
