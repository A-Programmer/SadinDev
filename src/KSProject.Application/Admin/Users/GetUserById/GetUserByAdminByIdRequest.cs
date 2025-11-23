using KSFramework.Contracts;
using Newtonsoft.Json;

namespace KSProject.Application.Admin.Users.GetUserById;

public sealed class GetUserByIdRequest : IInjectable
{
    [property :JsonProperty(nameof(id))]
    public required Guid id { get; set; }
}
