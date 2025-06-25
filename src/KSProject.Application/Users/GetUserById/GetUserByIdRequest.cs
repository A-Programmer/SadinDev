using KSFramework.Contracts;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace KSProject.Application.Users.GetUserById;

public sealed class GetUserByIdRequest : IInjectable
{
    [JsonPropertyName("id")] public Guid Id { get; init; }
}