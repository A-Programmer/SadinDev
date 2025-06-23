using Newtonsoft.Json;

namespace KSProject.Application.TestAggregate.UpdateTestAggregate;

public sealed class UpdateTestAggregateResponse
{
    [property:JsonProperty("id")] public required Guid Id { get; init; }
}