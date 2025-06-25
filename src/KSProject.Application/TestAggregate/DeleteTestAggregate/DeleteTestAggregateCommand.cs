using KSFramework.Contracts;
using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.TestAggregate.DeleteTestAggregate;

public record DeleteTestAggregateCommand(
    DeleteTestAggregateRequest Payload
    ) : ICommand<DeleteTestAggregateResponse>, IInjectable;