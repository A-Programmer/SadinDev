using Newtonsoft.Json;

namespace KSProject.Application.TestAggregate.DeleteTestAggregate;

public sealed class DeleteTestAggregateResponse
{
    [property:JsonProperty("id")] public Guid Id { get; init; }
}