using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.TestAggregate.CreateTestAggregate;

public record CreateTestAggregateCommand : ICommand<CreateTestAggregateResponse>
{
    public string Title { get; set; }
    public string Content { get; set; }
}