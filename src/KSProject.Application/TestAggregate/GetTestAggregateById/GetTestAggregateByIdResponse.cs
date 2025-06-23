using Newtonsoft.Json;

namespace KSProject.Application.TestAggregate.GetTestAggregateById;

public sealed class GetTestAggregateByIdResponse
{
    /// <summary>
    /// TestAggregate Id
    /// </summary
    [property:JsonProperty("id")]
    public required Guid Id { get; init; }
    
    /// <summary>
    /// TestAggregate Title
    /// </summary
    [property:JsonProperty("title")]
    public required string Title { get; init; }
    
    /// <summary>
    /// TestAggregate Content
    /// </summary
    [property:JsonProperty("content")]
    public required string Content { get; init; }
}