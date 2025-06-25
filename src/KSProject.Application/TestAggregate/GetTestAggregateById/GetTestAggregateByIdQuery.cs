using KSFramework.KSMessaging.Abstraction;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace KSProject.Application.TestAggregate.GetTestAggregateById;

public record GetTestAggregateByIdQuery(
    
    [property: JsonProperty("payload")]
    GetTestAggregateByIdRequest Payload
    ) : IQuery<GetTestAggregateByIdResponse>;