using KSFramework.Contracts;
using Newtonsoft.Json;

namespace KSProject.Application.Roles.GetRoleById;

public sealed class GetRoleByIdRequest : IInjectable
{
    [property :JsonProperty(nameof(id))]
    public required Guid id { get; set; }
}