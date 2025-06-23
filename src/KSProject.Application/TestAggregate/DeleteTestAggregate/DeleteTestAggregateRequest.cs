using KSFramework.Contracts;
using Newtonsoft.Json;

namespace KSProject.Application.TestAggregate.DeleteTestAggregate;

public sealed class DeleteTestAggregateRequest : IInjectable
{
    [property:JsonProperty("id")]
    public Guid id { get; init; }
}