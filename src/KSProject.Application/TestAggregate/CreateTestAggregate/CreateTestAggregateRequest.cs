using KSFramework.Contracts;
using Newtonsoft.Json;

namespace KSProject.Application.TestAggregate.CreateTestAggregate;

public sealed class CreateTestAggregateRequest : IInjectable
{
    [property: JsonProperty("title")]
    public required string Title { get; init; } = string.Empty;

    [property: JsonProperty("content")]
    public required string Content { get; init; } = string.Empty;
}