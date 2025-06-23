using KSFramework.KSMessaging.Abstraction;
using KSFramework.Pagination;
using KSProject.Application.Roles.CreateRole;
using KSProject.Application.Roles.DeleteRole;
using KSProject.Application.Roles.GetAllRoles;
using KSProject.Application.Roles.GetPaginatedRoles;
using KSProject.Application.Roles.GetRoleById;
using KSProject.Application.Roles.UpdateRole;
using KSProject.Presentation.BaseControllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sadin.Application.Roles.GetPaginatedRoles;

namespace KSProject.Presentation.Controllers.AdminControllers;

public sealed class RolesController(ISender sender) : BaseController(sender)
{
    [HttpGet]
    [Route(Routes.Roles.GetPagedRoles)]
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
    [Route(Routes.Roles.GetAllRoles)]
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
    [Route(Routes.Roles.GetRoleById)]
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

    [HttpPut]
    [Route(Routes.Roles.UpdateRole)]
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
    [Route(Routes.Roles.CreateRole)]
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
    [Route(Routes.Roles.DeleteRole)]
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