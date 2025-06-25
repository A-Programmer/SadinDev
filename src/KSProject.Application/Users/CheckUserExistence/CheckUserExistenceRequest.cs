using KSFramework.Contracts;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace KSProject.Application.Users.CheckUserExistence;

public class CheckUserExistenceRequest : IInjectable
{
    [JsonPropertyName("userNameOrEmailOrPhoneNumber")] public required string UserNameOrEmailOrPhoneNumber { get; set; }
}