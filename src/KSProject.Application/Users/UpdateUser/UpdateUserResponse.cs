using System.Text.Json.Serialization;
using KSFramework.Contracts;

namespace KSProject.Application.Users.UpdateUser;

public sealed class UpdateUserResponse : IInjectable
{
    [JsonPropertyName("id")] public Guid Id { get; set; }
}