using KSFramework.Contracts;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace KSProject.Application.Users.GetUserById;

public sealed class GetUserByIdRequest : IInjectable
{
    [property :JsonProperty(nameof(id))]
    public required Guid id { get; set; }
}