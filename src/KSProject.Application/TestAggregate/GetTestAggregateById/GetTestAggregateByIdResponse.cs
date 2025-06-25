using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace KSProject.Application.TestAggregate.GetTestAggregateById;

public sealed class GetTestAggregateByIdResponse
{
    /// <summary>
    /// TestAggregate Id
    /// </summary
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }
    
    /// <summary>
    /// TestAggregate Title
    /// </summary
    [JsonPropertyName("title")]
    public required string Title { get; init; }
    
    /// <summary>
    /// TestAggregate Content
    /// </summary
    [JsonPropertyName("content")]
    public required string Content { get; init; }
}