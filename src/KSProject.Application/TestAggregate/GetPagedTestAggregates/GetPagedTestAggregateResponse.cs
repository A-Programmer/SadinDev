using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace KSProject.Application.TestAggregate.GetPagedTestAggregates;

public sealed class GetPagedTestAggregateResponse
{
    /// <summary>
    /// TestAggregate Id
    /// </summary>
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }
    
    /// <summary>
    /// TestAggregate Title
    /// </summary>
    [JsonPropertyName("title")]
    public required string Title { get; init; }
}