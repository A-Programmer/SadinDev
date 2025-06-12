using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.TestAggregate.DeleteTestAggregate;

public record DeleteTestAggregateCommand : ICommand<DeleteTestAggregateResponse>
{
    public Guid Id { get; set; }
}
