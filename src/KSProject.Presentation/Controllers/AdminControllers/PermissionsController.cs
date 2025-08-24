using KSProject.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace KSProject.Presentation.Controllers.AdminControllers;
[ApiController]
[Route("api/[controller]")]
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
