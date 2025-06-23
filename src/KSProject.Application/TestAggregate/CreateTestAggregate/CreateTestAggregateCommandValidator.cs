using FluentValidation;

namespace KSProject.Application.TestAggregate.CreateTestAggregate;

public class CreateTestAggregateCommandValidator : AbstractValidator<CreateTestAggregateCommand>
{
    public CreateTestAggregateCommandValidator()
    {
        RuleFor(x => x.Payload.Title)
            .NotEmpty()
            .NotNull();
        
        RuleFor(x => x.Payload.Content)
            .NotEmpty()
            .NotNull();
    }
}