using KSFramework.KSMessaging.Abstraction;
using KSFramework.Pagination;
using KSProject.Application.Admin.Roles.CreateRole;
using KSProject.Application.Admin.Roles.DeleteRole;
using KSProject.Application.Admin.Roles.GetAllRoles;
using KSProject.Application.Admin.Roles.GetPaginatedRoles;
using KSProject.Application.Admin.Roles.GetRoleById;
using KSProject.Application.Admin.Roles.GetRolePermissionsByRoleId;
using KSProject.Application.Admin.Roles.UpdateRole;
using KSProject.Presentation.Attributes;
using KSProject.Presentation.BaseControllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KSProject.Presentation.Controllers.AdminControllers;

public sealed class RolesController(ISender sender) : BaseController(sender)
{
    [HttpGet]
    [Permission("GetPagedRoles")]
    [Route(Routes.Roles_Admin.GET_PAGED)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    public async Task<ActionResult<PaginatedList<RolesListItemResponse>>> GetAsync(
        [FromQuery] GetPaginatedRolesRequest options,
        CancellationToken cancellationToken = default)
    {
        GetPaginatedRolesQuery query = new(options);

        PaginatedList<RolesListItemResponse> result = await Sender.Send(query,
            cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [Permission("GetAllRoles")]
    [Route(Routes.Roles_Admin.GET_ALL)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    public async Task<ActionResult<List<GetAllRolesResponse>>> GetAsync(
        [FromQuery] GetAllRolesRequest request,
        CancellationToken cancellationToken = default)
    {
        GetAllRolesQuery query = new(request);

        List<GetAllRolesResponse> result = await Sender.Send(query,
            cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [Permission("GetRoleById")]
    [Route(Routes.Roles_Admin.GET_BY_ID)]
    [Produces(typeof(RoleItemResponse))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RoleItemResponse>> GetAsync(
        [FromRoute] GetRoleByIdRequest payload,
        CancellationToken cancellationToken = default)
    {
        GetRoleByIdQuery query = new(payload);

        RoleItemResponse result = await Sender.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [Permission("GetRolePermissions")]
    [Route(Routes.Roles_Admin.Role_Permissions.GET_ALL)]
    [Produces(typeof(RolePermissionsResponse))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RolePermissionsResponse>> GetRolePermissionsAsync(
        [FromRoute] GetRolePermissionsByRoleIdRequest payload,
        CancellationToken cancellationToken = default)
    {
        GetRolePermissionsQuery query = new(payload);

        RolePermissionsResponse result = await Sender.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpPut]
    [Permission("UpdateRole")]
    [Route(Routes.Roles_Admin.UPDATE)]
    [Produces(typeof(RoleUpdateResponse))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RoleUpdateResponse>> PutAsync(
        Guid id,
        [FromBody] RoleUpdateRequest payload,
        CancellationToken cancellationToken = default)
    {
        if (id != payload.Id)
            return BadRequest("The record could not be found.");

        RoleUpdateCommand command = new(payload);

        RoleUpdateResponse result = await Sender.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [Permission("CreateRole")]
    [Route(Routes.Roles_Admin.CREATE)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CreateRoleResponse),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<CreateRoleResponse>> PostAsync(
        [FromBody] CreateRoleRequest request,
        CancellationToken cancellationToken = default)
    {
        CreateRoleCommand command = new(request);

        CreateRoleResponse result = await Sender.Send(command,
            cancellationToken);

        return Ok(result);
    }

    [HttpDelete]
    [Permission("DeleteRole")]
    [Route(Routes.Roles_Admin.DELETE)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(DeleteRoleResponse),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<DeleteRoleResponse>> DeleteAsync(
        [FromRoute] DeleteRoleRequest request,
        CancellationToken cancellationToken = default)
    {
        DeleteRoleCommand command = new(request);

        DeleteRoleResponse result = await Sender.Send(command,
            cancellationToken);

        return Ok(result);
    }
}
