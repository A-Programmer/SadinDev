using System.ComponentModel.DataAnnotations;
using KSFramework.Contracts;

namespace KSProject.Application.Users.Register;

public sealed class RegisterRequest : IInjectable
{
    [Required]
    public required string UserName { get; init; }

    [Required]
    public required string Email { get; init; }

    [Required]
    public required string PhoneNumber { get; init; }

    [Required]
    public required string Password { get; init; }

    [Compare(nameof(Password))]
    public required string ConfirmPassword { get; init; }

    public string? FirstName { get; set; } = string.Empty;

    public string? LastName { get; set; } = string.Empty;

    public string? AboutMe { get; set; } = string.Empty;

    public DateTime? BirthDateUtc { get; set; } = null;
}
