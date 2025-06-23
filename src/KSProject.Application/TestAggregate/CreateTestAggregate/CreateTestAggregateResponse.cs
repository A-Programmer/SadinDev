using Newtonsoft.Json;

namespace KSProject.Application.TestAggregate.CreateTestAggregate;

public sealed class CreateTestAggregateResponse
{
    [property:JsonProperty("id")]
    public Guid Id { get; init; }
}