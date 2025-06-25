using KSFramework.KSMessaging.Abstraction;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace KSProject.Application.Roles.GetAllRoles;

public record GetAllRolesQuery(
    [property: JsonProperty("payload")]
    GetAllRolesRequest Payload
    ) : IQuery<List<GetAllRolesResponse>>;