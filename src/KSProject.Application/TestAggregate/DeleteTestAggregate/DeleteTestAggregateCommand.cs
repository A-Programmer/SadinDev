using KSFramework.Contracts;
using KSFramework.KSMessaging.Abstraction;
using Newtonsoft.Json;

namespace KSProject.Application.TestAggregate.DeleteTestAggregate;

public record DeleteTestAggregateCommand(
    [property:JsonProperty("Payload")]
    DeleteTestAggregateRequest Payload
    ) : ICommand<DeleteTestAggregateResponse>, IInjectable;