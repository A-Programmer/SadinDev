using FluentValidation;

namespace KSProject.Application.TestAggregate.UpdateTestAggregate;

public class UpdateTestAggregateCommandValidator : AbstractValidator<UpdateTestAggregateCommand>
{
    public UpdateTestAggregateCommandValidator()
    {
        RuleFor(x => x.Payload.Id)
            .NotNull()
            .NotEmpty();
        
        RuleFor(x => x.Payload.Title)
            .NotNull()
            .NotEmpty();
        
        RuleFor(x => x.Payload.Content)
            .NotNull()
            .NotEmpty();
    }
}