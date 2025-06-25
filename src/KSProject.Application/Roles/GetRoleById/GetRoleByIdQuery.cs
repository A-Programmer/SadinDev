using KSFramework.KSMessaging.Abstraction;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace KSProject.Application.Roles.GetRoleById;

public record GetRoleByIdQuery(
    
    [property: JsonProperty("payload")]
    GetRoleByIdRequest Payload) : IQuery<RoleItemResponse>;