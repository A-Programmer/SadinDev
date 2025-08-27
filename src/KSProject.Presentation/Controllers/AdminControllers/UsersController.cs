using KSFramework.KSMessaging.Abstraction;
using KSFramework.Pagination;
using KSProject.Application.Users.CreateUser;
using KSProject.Application.Users.DeleteUser;
using KSProject.Application.Users.GetPaginatedUsers;
using KSProject.Application.Users.GetUserById;
using KSProject.Application.Users.GetUserPermissionsByUserId;
using KSProject.Application.Users.UpdateUser;
using KSProject.Application.Users.UpdateUserRoles;
using KSProject.Presentation.Attributes;
using KSProject.Presentation.BaseControllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KSProject.Presentation.Controllers.AdminControllers;

public sealed class UsersController(ISender sender) : BaseController(sender)
{
	[HttpGet]
	[Route(Routes.Users_Admin.GetPagedUsers)]
	[Permission("GetPagedUsers")]
	[ProducesResponseType(typeof(PaginatedList<UsersListItemResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<PaginatedList<UsersListItemResponse>>> GetAllAsync(
		[FromQuery] GetPaginatedUsersRequest options,
		CancellationToken cancellationToken = default)
	{
		GetPaginatedUsersQuery query = new(options);

		PaginatedList<UsersListItemResponse> result =
			await Sender.Send(query, cancellationToken);

		return Ok(result);
	}

	[HttpGet]
	[Permission("GetUserById")]
	[Route(Routes.Users_Admin.GetUserById)]
	[Produces(typeof(UserResponse))]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<UserResponse>> GetAsync(
		[FromRoute] GetUserByIdRequest request,
		CancellationToken cancellationToken)
	{
		GetUserByIdQuery query = new(request);

		UserResponse result = await Sender.Send(query, cancellationToken);

		return Ok(result);
	}


	[HttpGet]
	[Permission("GetUserPermissions")]
	[Route(Routes.Users_Admin.User_Permissions.GetUserPermissions)]
	[Produces(typeof(UserPermissionsResponse))]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<UserPermissionsResponse>> GetPermissionsAsync(
		[FromRoute] GetUserPermissionsByIdRequest request,
		CancellationToken cancellationToken)
	{
		GetUserPermissionsByIdQuery query = new(request);

		UserPermissionsResponse result = await Sender.Send(query, cancellationToken);

		return Ok(result);
	}

	[HttpPost]
	[Permission("CreateUser")]
	[Route(Routes.Users_Admin.CreateUser)]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status200OK)]
	public async Task<ActionResult<CreateUserResponse>> PostAsync(
		[FromBody] CreateUserRequest request,
		CancellationToken cancellationToken = default)
	{
		CreateUserCommand command = new(request);

		CreateUserResponse result = await Sender.Send(command, cancellationToken);

		return Ok(result);
	}

	[HttpPut]
	[Permission("UpdateUser")]
	[Route(Routes.Users_Admin.UpdateUser)]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(UpdateUserResponse), StatusCodes.Status200OK)]
	public async Task<ActionResult<UpdateUserResponse>> PutAsync(
		Guid id,
		[FromBody] UpdateUserRequest request,
		CancellationToken cancellationToken)
	{
		if (id != request.Id)
			return BadRequest("The record could not be found.");

		UpdateUserCommand command = new(request);

		UpdateUserResponse result = await Sender.Send(command, cancellationToken);

		return Ok(result);
	}

	// TODO: Change Password should be implemented. Change Password and Reset Password are different actions, ResetPassword should be implemented in AuthController

	[HttpPut]
	[Permission("UpdateUserRoles")]
	[Route(Routes.Users_Admin.UpdateUserRoles)]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<UpdateUserResponse>> PutAsync(
		Guid id,
		[FromBody] UpdateUserRolesRequest request,
		CancellationToken cancellationToken)
	{
		if (id != request.Id)
			return BadRequest("The record could not be found.");

		UpdateUserRolesCommand command = new(request);

		await Sender.Send(command, cancellationToken);

		return NoContent();
	}

	[HttpDelete]
	[Permission("DeleteUser")]
	[Route(Routes.Users_Admin.DeleteUser)]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> DeleteAsync(
		[FromRoute] DeleteUserRequest request,
		CancellationToken cancellationToken)
	{
		DeleteUserCommand command = new(request);

		await Sender.Send(command, cancellationToken);

		return NoContent();
	}
}