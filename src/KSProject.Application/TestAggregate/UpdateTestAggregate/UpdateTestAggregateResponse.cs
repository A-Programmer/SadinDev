using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace KSProject.Application.TestAggregate.UpdateTestAggregate;

public sealed class UpdateTestAggregateResponse
{
    [JsonPropertyName("id")] public required Guid Id { get; init; }
}