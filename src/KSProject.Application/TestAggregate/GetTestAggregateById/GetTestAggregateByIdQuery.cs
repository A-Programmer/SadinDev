using KSFramework.KSMessaging.Abstraction;
using Newtonsoft.Json;

namespace KSProject.Application.TestAggregate.GetTestAggregateById;

public record GetTestAggregateByIdQuery(
    
    [property: JsonProperty("payload")]
    GetTestAggregateByIdRequest Payload
    ) : IQuery<GetTestAggregateByIdResponse>;