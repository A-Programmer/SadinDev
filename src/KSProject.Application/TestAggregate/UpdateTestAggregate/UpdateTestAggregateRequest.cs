using KSFramework.Contracts;
using Newtonsoft.Json;

namespace KSProject.Application.TestAggregate.UpdateTestAggregate;

public sealed class UpdateTestAggregateRequest : IInjectable
{
    [property: JsonProperty("id")] public Guid Id { get; init; }
    [property: JsonProperty("title")] public string Title { get; init; } = string.Empty;

    [property: JsonProperty("content")] public string Content { get; init; } = string.Empty;
}