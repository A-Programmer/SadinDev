using KSFramework.KSMessaging.Abstraction;
using KSProject.Application.ApiKeys.GenerateApiKey;
using KSProject.Application.ApiKeys.GetUserApiKeys;
using KSProject.Application.ApiKeys.RevokeApiKey;
using KSProject.Presentation.Attributes;
using KSProject.Presentation.BaseControllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KSProject.Presentation.Controllers.AdminControllers;

public class ApiKeysController(ISender sender) : SecureBaseController(sender)
{
    [HttpPost]
    [Permission("GenerateApiKey")]
    [Route(Routes.ApiKeys_Admin.GENERATE)]
    [Produces(typeof(GenerateApiKeyCommandResponse))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GenerateApiKeyCommandResponse>> GenerateAsync(
        [FromBody] GenerateApiKeyCommandRequest payload, // e.g., { Scopes = "blog,notification" }
        CancellationToken cancellationToken = default)
    {
        var userId = Guid.Parse(UserId); // از SecureBaseController

        var newPayload = payload with { UserId = userId }; // Set in request if needed

        GenerateApiKeyCommand command = new(newPayload);
        GenerateApiKeyCommandResponse result = await Sender.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [Permission("GetUserApiKeys")]
    [Route(Routes.ApiKeys_Admin.GET_USER_API_KEYS)]
    [Produces(typeof(GetUserApiKeysQueryResponse))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GetUserApiKeysQueryResponse>> GetUserApiKeysAsync(
        CancellationToken cancellationToken = default)
    {
        var userId = Guid.Parse(UserId);

        GetUserApiKeysQueryRequest request = new(userId);
        GetUserApiKeysQuery query = new(request);
        GetUserApiKeysQueryResponse result = await Sender.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpPut]
    [Permission("RevokeApiKey")]
    [Route(Routes.ApiKeys_Admin.REVOKE)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RevokeAsync(
        Guid apiKeyId,
        CancellationToken cancellationToken = default)
    {
        var userId = Guid.Parse(UserId);

        RevokeApiKeyCommandRequest request = new(userId, apiKeyId);
        RevokeApiKeyCommand command = new(request);
        await Sender.Send(command, cancellationToken);

        return NoContent();
    }
}
