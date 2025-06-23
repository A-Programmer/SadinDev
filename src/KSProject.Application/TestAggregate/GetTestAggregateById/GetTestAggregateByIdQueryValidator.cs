using FluentValidation;

namespace KSProject.Application.TestAggregate.GetTestAggregateById;

public sealed class GetTestAggregateByIdQueryValidator : AbstractValidator<GetTestAggregateByIdQuery>
{
    public GetTestAggregateByIdQueryValidator()
    {
        RuleFor(x => x.Payload.id)
            .NotNull()
            .NotEmpty();
    }
}