using KSFramework.KSMessaging.Abstraction;
using Newtonsoft.Json;

namespace KSProject.Application.Admin.Roles.GetRoleById;

public record GetRoleByIdQuery(
    
    [property: JsonProperty("payload")]
    GetRoleByIdRequest Payload) : IQuery<RoleItemResponse>;
