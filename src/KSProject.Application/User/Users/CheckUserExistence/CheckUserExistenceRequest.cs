using System.Text.Json.Serialization;
using KSFramework.Contracts;

namespace KSProject.Application.User.Users.CheckUserExistence;

public class CheckUserExistenceRequest : IInjectable
{
    [JsonPropertyName("userNameOrEmailOrPhoneNumber")] public required string UserNameOrEmailOrPhoneNumber { get; set; }
}
