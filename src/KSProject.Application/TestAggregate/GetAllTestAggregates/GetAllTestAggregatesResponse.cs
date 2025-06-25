using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace KSProject.Application.TestAggregate.GetAllTestAggregates;

public sealed class GetAllTestAggregatesResponse
{
    /// <summary>
    /// TestAggregate Id
    /// </summary>
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }
    
    /// <summary>
    /// TestAggregate Title
    /// </summary>
    [JsonPropertyName("title")]
    public required string Title { get; init; }
}