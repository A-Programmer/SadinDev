using KSFramework.Contracts;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace KSProject.Application.TestAggregate.DeleteTestAggregate;

public sealed class DeleteTestAggregateRequest : IInjectable
{
    [JsonPropertyName("id")]
    public Guid id { get; init; }
}