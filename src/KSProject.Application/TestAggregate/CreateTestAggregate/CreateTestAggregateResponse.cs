using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace KSProject.Application.TestAggregate.CreateTestAggregate;

public sealed class CreateTestAggregateResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }
}