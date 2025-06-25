using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace KSProject.Application.TestAggregate.DeleteTestAggregate;

public sealed class DeleteTestAggregateResponse
{
    [JsonPropertyName("id")] public Guid Id { get; init; }
}