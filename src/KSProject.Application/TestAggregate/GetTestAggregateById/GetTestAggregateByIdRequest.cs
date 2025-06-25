using KSFramework.Contracts;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace KSProject.Application.TestAggregate.GetTestAggregateById;

public sealed class GetTestAggregateByIdRequest : IInjectable
{
    [property: JsonProperty("id")] public required Guid id { get; set; }
}