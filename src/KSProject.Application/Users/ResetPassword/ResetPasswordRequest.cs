using System.ComponentModel.DataAnnotations;
using KSFramework.Contracts;

namespace KSProject.Application.Users.ResetPassword;

public sealed class ResetPasswordRequest : IInjectable
{
    [Required]
    public required string NewPassword { get; set; }
    
    [Compare(nameof(NewPassword))]
    public required string ConfirmPassword { get; set; }
    
}