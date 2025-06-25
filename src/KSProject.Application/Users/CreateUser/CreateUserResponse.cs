using System.Text.Json.Serialization;
using KSFramework.Contracts;

namespace KSProject.Application.Users.CreateUser;

public sealed class CreateUserResponse : IInjectable
{
    [JsonPropertyName("id")] public Guid Id { get; set; }
}