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
        var superAdminStatusClaim = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("is_registered", StringComparison.CurrentCultureIgnoreCase));

        if (superAdminStatusClaim == null)
        {
            Console.WriteLine("\n\n\n\n\n\n The UserId Claim could not be found. \n\n\n\n\n\n\n");
        }
        if (bool.Parse(superAdminStatusClaim.Value))
        {
            return;
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
