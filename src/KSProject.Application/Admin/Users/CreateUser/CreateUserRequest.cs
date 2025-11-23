using System.ComponentModel.DataAnnotations;
using KSFramework.Contracts;

namespace KSProject.Application.Admin.Users.CreateUser;

public sealed class CreateUserRequest : IInjectable
{
    [Required]
    public required string UserName { get; set; }

    [Required]
    public required string Email { get; set; }
    
    [Required]
    public required string PhoneNumber { get; set; }

    [Required]
    public required string Password { get; set; }
    
    [Compare(nameof(Password))]
    public required string ConfirmPassword { get; set; }

    public string[] Roles { get; set; }
}
