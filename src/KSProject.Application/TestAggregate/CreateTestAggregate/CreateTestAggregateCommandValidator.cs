using FluentValidation;

namespace KSProject.Application.TestAggregate.CreateTestAggregate;

public class CreateTestAggregateCommandValidator : AbstractValidator<CreateTestAggregateCommand>
{
    public CreateTestAggregateCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .NotNull();
        
        RuleFor(x => x.Content)
            .NotEmpty()
            .NotNull();
    }
}