using KSFramework.KSMessaging.Abstraction;
using KSFramework.Pagination;
using KSProject.Application.Admin.ApiKeys.GenerateApiKey;
using KSProject.Application.Admin.ApiKeys.GetUserApiKeys;
using KSProject.Application.Admin.ApiKeys.RevokeApiKey;
using KSProject.Application.Admin.Users.CreateUser;
using KSProject.Application.Admin.Users.DeleteUser;
using KSProject.Application.Admin.Users.GetUserById;
using KSProject.Application.Admin.Users.GetUserPermissionsByUserId;
using KSProject.Application.Admin.Users.UpdateUser;
using KSProject.Application.Admin.Users.UpdateUserPermissions;
using KSProject.Application.Admin.Users.UpdateUserRoles;
using KSProject.Application.User.Users.UserTransactions;
using KSProject.Domain.Attributes;
using KSProject.Presentation.Attributes;
using KSProject.Presentation.BaseControllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KSProject.Presentation.Controllers.AdminControllers;

public class UserController(ISender sender) : SecureBaseController(sender)
{
    [HttpGet]
    [Permission("GetUser")]
    [Route(Routes.Users_User.GET_USER)]
    [Produces(typeof(UserResponse))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserResponse>> GetAsync(
        CancellationToken cancellationToken)
    {
        GetUserByIdQuery queryByIdQuery = new(Guid.Parse(UserId));

        UserResponse result = await Sender.Send(queryByIdQuery, cancellationToken);

        return Ok(result);
    }


    // [HttpGet]
    // [Permission("GetUserPermissions")]
    // [Route(Routes.Users_User.User_Permissions.GET_ALL)]
    // [Produces(typeof(UserPermissionsResponse))]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    // public async Task<ActionResult<UserPermissionsResponse>> GetPermissionsAsync(
    //     [FromRoute] GetUserPermissionsByIdRequest request,
    //     CancellationToken cancellationToken)
    // {
    //     GetUserPermissionsByIdQuery query = new(request);
    //
    //     UserPermissionsResponse result = await Sender.Send(query, cancellationToken);
    //
    //     return Ok(result);
    // }

    // [HttpPut]
    // [Permission("UpdateUser")]
    // [Route(Routes.Users_User.UPDATE)]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // [ProducesResponseType(typeof(UpdateUserResponse), StatusCodes.Status200OK)]
    // public async Task<ActionResult<UpdateUserResponse>> PutAsync(
    //     Guid id,
    //     [FromBody] UpdateUserRequest request,
    //     CancellationToken cancellationToken)
    // {
    //     if (id != request.Id)
    //         return BadRequest("The record could not be found.");
    //
    //     UpdateUserCommand command = new(request);
    //
    //     UpdateUserResponse result = await Sender.Send(command, cancellationToken);
    //
    //     return Ok(result);
    // }

    // TODO: Change Password should be implemented. Change Password and Reset Password are different actions, ResetPassword should be implemented in AuthController

    // [HttpPut]
    // [Permission("UpdateUserRoles")]
    // [Route(Routes.Users_User.User_Roles.UPDATE)]
    // [ProducesResponseType(StatusCodes.Status204NoContent)]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<ActionResult<UpdateUserRolesResponse>> PutAsync(
    //     Guid id,
    //     [FromBody] UpdateUserRolesRequest request,
    //     CancellationToken cancellationToken)
    // {
    //     if (id != request.Id)
    //         return BadRequest("The record could not be found.");
    //
    //     UpdateUserRolesCommand command = new(request);
    //
    //     await Sender.Send(command, cancellationToken);
    //
    //     return NoContent();
    // }
    //
    // [HttpPut]
    // [Permission("UpdateUserPermissions")]
    // [Route(Routes.Users_User.User_Permissions.UPDATE)]
    // [ProducesResponseType(StatusCodes.Status204NoContent)]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<ActionResult<UpdateUserResponse>> PutUserPermissionsAsync(
    //     Guid id,
    //     [FromBody] UpdateUserPermissionsRequest request,
    //     CancellationToken cancellationToken)
    // {
    //     if (id != request.Id)
    //         return BadRequest("The record could not be found.");
    //
    //     UpdateUserPermissionsCommand command = new(request);
    //
    //     await Sender.Send(command, cancellationToken);
    //
    //     return NoContent();
    // }
    //
    // [HttpDelete]
    // [Permission("DeleteUser")]
    // [Route(Routes.Users_User.DELETE)]
    // [ProducesResponseType(StatusCodes.Status204NoContent)]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<IActionResult> DeleteAsync(
    //     [FromRoute] DeleteUserRequest request,
    //     CancellationToken cancellationToken)
    // {
    //     DeleteUserCommand command = new(request);
    //
    //     await Sender.Send(command, cancellationToken);
    //
    //     return NoContent();
    // }
    
    #region API Keys
    // [HttpPost]
    // [Permission("GenerateApiKey")]
    // [Route(Routes.Users_User.ApiKeys_Admin.GENERATE)]
    // [Produces(typeof(GenerateApiKeyCommandResponse))]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    // public async Task<ActionResult<GenerateApiKeyCommandResponse>> GenerateAsync(
    //     [FromQuery] Guid userId,
    //     [FromBody] GenerateApiKeyCommandRequest payload, // e.g., { Scopes = "blog,notification" }
    //     CancellationToken cancellationToken = default)
    // {
    //     var currentUserId = Guid.Parse(UserId);
    //     if (currentUserId != userId)
    //     {
    //         return BadRequest("The userId does not match the request.");
    //     }
    //
    //     var newPayload = payload with { UserId = userId }; // Set in request if needed
    //
    //     GenerateApiKeyCommand command = new(newPayload);
    //     GenerateApiKeyCommandResponse result = await Sender.Send(command, cancellationToken);
    //
    //     return Ok(result);
    // }
    //
    // [HttpGet]
    // [Permission("GetUserApiKeys")]
    // [Route(Routes.Users_User.ApiKeys_Admin.GET_USER_API_KEYS)]
    // [Produces(typeof(GetUserApiKeysQueryResponse))]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    // public async Task<ActionResult<GetUserApiKeysQueryResponse>> GetUserApiKeysAsync(
    //     [FromQuery] Guid userId,
    //     CancellationToken cancellationToken = default)
    // {
    //     var currentUserId = Guid.Parse(UserId);
    //     if (currentUserId != userId)
    //     {
    //         return BadRequest("The userId does not match the request.");
    //     }
    //     
    //     GetUserApiKeysQueryRequest request = new(userId);
    //     GetUserApiKeysQuery query = new(request);
    //     GetUserApiKeysQueryResponse result = await Sender.Send(query, cancellationToken);
    //
    //     return Ok(result);
    // }
    //
    // [HttpPut]
    // [Permission("RevokeApiKey")]
    // [Route(Routes.Users_User.ApiKeys_Admin.REVOKE)]
    // [ProducesResponseType(StatusCodes.Status204NoContent)]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    // public async Task<IActionResult> RevokeAsync(
    //     [FromQuery] Guid userId,
    //     Guid apiKeyId,
    //     CancellationToken cancellationToken = default)
    // {
    //     var currentUserId = Guid.Parse(UserId);
    //     if (currentUserId != userId)
    //     {
    //         return BadRequest("The userId does not match the request.");
    //     }
    //
    //     RevokeApiKeyCommandRequest request = new(userId, apiKeyId);
    //     RevokeApiKeyCommand command = new(request);
    //     await Sender.Send(command, cancellationToken);
    //
    //     return NoContent();
    // }
    //
    // [HttpPut]
    // [Permission("ExtendUderApiKey")]
    // [Route(Routes.Users_User.ApiKeys_Admin.REVOKE)]
    // [ProducesResponseType(StatusCodes.Status204NoContent)]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    // public async Task<IActionResult> ExtendUserApisync(
    //     [FromQuery] Guid userId,
    //     Guid apiKeyId,
    //     CancellationToken cancellationToken = default)
    // {
    //     var currentUserId = Guid.Parse(UserId);
    //     if (currentUserId != userId)
    //     {
    //         return BadRequest("The userId does not match the request.");
    //     }
    //
    //     RevokeApiKeyCommandRequest request = new(userId, apiKeyId);
    //     RevokeApiKeyCommand command = new(request);
    //     await Sender.Send(command, cancellationToken);
    //
    //     return NoContent();
    // }
    #endregion
    
    #region Transations
        // [HttpGet]
        // [FreeEndpoint]
        // [Permission("ViewUserTransactions")]
        // [Route(Routes.Users_User.Wallets.GET_User_TRANSACTIONS)]
        // [ProducesResponseType(typeof(PaginatedList<UserTransactionListItemResponse>), StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        // public async Task<ActionResult<PaginatedList<UserTransactionListItemResponse>>> GetPagedTransactionsAsync(
        //     Guid userId,
        //     [FromQuery] GetUserTransactionsQueryRequest options, // e.g., page, size
        //     CancellationToken ct = default)
        // {
        //     var query = new GetUserTransactionsQuery(userId, options);
        //     var result = await Sender.Send(query, ct);
        //     return Ok(result);
        // }
    #endregion
}
