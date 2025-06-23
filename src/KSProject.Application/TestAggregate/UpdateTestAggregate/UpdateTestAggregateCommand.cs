using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.TestAggregate.UpdateTestAggregate;

public record UpdateTestAggregateCommand(
    UpdateTestAggregateRequest Payload
    ) : ICommand<UpdateTestAggregateResponse>;