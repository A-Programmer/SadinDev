using KSFramework.Contracts;
using Newtonsoft.Json;

namespace KSProject.Application.Admin.Roles.GetRoleById;

public sealed class GetRoleByIdRequest : IInjectable
{
    [property :JsonProperty(nameof(id))]
    public required Guid id { get; set; }
}
