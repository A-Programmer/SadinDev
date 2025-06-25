using System.ComponentModel.DataAnnotations;
using KSFramework.Contracts;

namespace KSProject.Application.Users.Login;

public sealed class LoginRequest : IInjectable
{
    [Required]
    public required string UserName { get; init; }

    [Required]
    public required string Password { get; init; }
}