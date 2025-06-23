using Newtonsoft.Json;

namespace KSProject.Application.TestAggregate.GetPagedTestAggregates;

public sealed class GetPagedTestAggregateResponse
{
    /// <summary>
    /// TestAggregate Id
    /// </summary>
    [property:JsonProperty("id")]
    public required Guid Id { get; init; }
    
    /// <summary>
    /// TestAggregate Title
    /// </summary>
    [property:JsonProperty("title")]
    public required string Title { get; init; }
}