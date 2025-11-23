using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KSProject.Presentation.Attributes;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class PermissionAttribute : Attribute, IAsyncAuthorizationFilter
{
    public string[] Permissions { get; }

    public PermissionAttribute(
        params string[] permissions)
    {
        Permissions = permissions ?? throw new ArgumentNullException(nameof(permissions));
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity?.IsAuthenticated ?? true)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        var superAdminStatusClaimExistence = context.HttpContext.User.Claims.Any(x => x.Type.Equals("is_SuperAdmin", StringComparison.CurrentCultureIgnoreCase));

        if (superAdminStatusClaimExistence)
        {
            var superAdminStatusClaim = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("is_SuperAdmin", StringComparison.CurrentCultureIgnoreCase));
            if (bool.Parse(superAdminStatusClaim.Value))
            {
                return;
            }
        }
        
        var userPermissions = context.HttpContext.User.Claims
            .Where(c => c.Type.ToLower() == "permission")
            .Select(c => c.Value)
            .ToList();

        bool hasPermission = Permissions.All(p => userPermissions.Contains(p));

        if (!hasPermission)
        {
            context.Result = new ForbidResult();
        }
    }
}
