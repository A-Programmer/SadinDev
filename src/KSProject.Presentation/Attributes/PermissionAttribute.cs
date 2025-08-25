using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KSProject.Presentation.Attributes;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class PermissionAttribute : Attribute, IAuthorizationFilter
{
	public string[] Permissions { get; }

	public PermissionAttribute(params string[] permissions)
	{
		Permissions = permissions ?? throw new ArgumentNullException(nameof(permissions));
	}

	public void OnAuthorization(AuthorizationFilterContext context)
	{
		if (!context.HttpContext.User.Identity?.IsAuthenticated ?? true)
		{
			context.Result = new UnauthorizedResult();
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
