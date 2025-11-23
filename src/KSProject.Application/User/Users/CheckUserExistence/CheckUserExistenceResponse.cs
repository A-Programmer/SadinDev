using System.Text.Json.Serialization;
using KSFramework.Contracts;

namespace KSProject.Application.User.Users.CheckUserExistence;

public sealed class CheckUserExistenceResponse : IInjectable
{
    [JsonPropertyName("id")]
    public Guid? Id { get; set; }
}
