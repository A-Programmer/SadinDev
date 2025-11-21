using System.Text.Json.Serialization;
using KSFramework.Contracts;

namespace KSProject.Application.User.Users.Register;

public sealed class RegisterResponse : IInjectable
{
    [JsonPropertyName("id")] public Guid Id { get; set; }
}
