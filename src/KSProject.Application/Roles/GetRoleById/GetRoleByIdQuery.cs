using KSFramework.KSMessaging.Abstraction;
using Newtonsoft.Json;

namespace KSProject.Application.Roles.GetRoleById;

public record GetRoleByIdQuery(
    
    [property: JsonProperty("payload")]
    GetRoleByIdRequest Payload) : IQuery<RoleItemResponse>;