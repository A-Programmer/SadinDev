using System.Security.Claims;
using KSFramework.KSMessaging.Abstraction;
using Microsoft.AspNetCore.Authorization;

namespace KSProject.Presentation.BaseControllers;

[Authorize]
public abstract class SecureBaseController : BaseController
{
    protected SecureBaseController(ISender sender)
        : base(sender)
    {
    }
    
    protected string UserId =>
        User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
            .Value!;
    
    protected string UserEmail =>
        User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value!;
    
    protected Dictionary<string, string> UserClaims =>
        User.Claims
            .ToDictionary(x => x.Type, x => x.Value);
    
    protected bool IsInRole(string role)
    {
        return GetUserRoles()
            .ToList()
            .Contains(role);
    }

    protected IEnumerable<string> GetUserRoles() =>
        User.Claims
            .Where(u => u.Type == ClaimTypes.Role)
            .Select(c => c.Value);

}
