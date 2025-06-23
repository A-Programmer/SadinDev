using Newtonsoft.Json;

namespace KSProject.Application.TestAggregate.GetAllTestAggregates;

public sealed class GetAllTestAggregatesResponse
{
    /// <summary>
    /// TestAggregate Id
    /// </summary>
    [property:JsonProperty("id")]
    public required Guid Id { get; set; }
    
    /// <summary>
    /// TestAggregate Title
    /// </summary>
    [property:JsonProperty("title")]
    public required string Title { get; init; }
}