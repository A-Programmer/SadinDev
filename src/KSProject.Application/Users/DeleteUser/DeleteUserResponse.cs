using System.Text.Json.Serialization;
using KSFramework.Contracts;

namespace KSProject.Application.Users.DeleteUser;

public sealed class DeleteUserResponse : IInjectable
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }
}