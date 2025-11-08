using KSProject.Application.Contracts;
using KSProject.Presentation.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace KSProject.Presentation.Controllers.AdminControllers;
[ApiController]
[Route("api/[controller]")]
[Permission("PermissionsController")]
public class PermissionsController : ControllerBase
{
	private readonly IPermissionDiscoveryService _permissionService;

	public PermissionsController(IPermissionDiscoveryService permissionService)
	{
		_permissionService = permissionService;
	}

	[HttpGet]
	public async Task<IActionResult> GetAsync()
	{
		var groupedPermissions = _permissionService.GetGroupedPermissions();
		return Ok(groupedPermissions);
	}
}
