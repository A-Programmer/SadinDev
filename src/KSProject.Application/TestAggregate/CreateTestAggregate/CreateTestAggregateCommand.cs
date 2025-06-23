using KSFramework.Contracts;
using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.TestAggregate.CreateTestAggregate;

public record CreateTestAggregateCommand(CreateTestAggregateRequest Payload) : ICommand<CreateTestAggregateResponse>, IInjectable;