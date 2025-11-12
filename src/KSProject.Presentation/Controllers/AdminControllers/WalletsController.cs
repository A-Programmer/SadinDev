using KSFramework.KSMessaging.Abstraction;
using KSProject.Application.Wallets.ChargeWallet;
using KSProject.Application.Wallets.GetWalletBalance;
using KSProject.Presentation.Attributes;
using KSProject.Presentation.BaseControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KSProject.Presentation.Controllers.AdminControllers;
[AllowAnonymous]
public class WalletsController(ISender sender) : SecureBaseController(sender)
{
    
    [HttpPost]
    [AllowAnonymous]
    // [Permission("ChargeUserWallet")]
    [Route(Routes.Wallets_Admin.CHARGE_WALLET)]
    [Produces(typeof(ChargeWalletCommandResponse))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ChargeWalletCommandResponse>> GetAsync(
        [FromBody] ChargeWalletCommandRequest payload,
        CancellationToken cancellationToken = default)
    {
        if (!Guid.TryParse(UserId, out Guid userId))
        {
            return BadRequest("Invalid User ID.");
        }
        
        ChargeWalletCommand command = new(userId, payload);

        ChargeWalletCommandResponse result = await Sender.Send(command, cancellationToken);

        return Ok(result);
    }
    
    
    [HttpGet]
    [AllowAnonymous]
    // [Permission("GetUserWalletBalance")]
    [Route(Routes.Wallets_Admin.GET_BALANCE)]
    [Produces(typeof(GetWalletBalanceQueryResponse))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GetWalletBalanceQueryResponse>> GetAsync(
        CancellationToken cancellationToken = default)
    {
        if (!Guid.TryParse(UserId, out Guid userId))
        {
            return BadRequest("Invalid User ID.");
        }
        GetWalletBalanceQuery query = new(userId);

        GetWalletBalanceQueryResponse result = await Sender.Send(query, cancellationToken);

        return Ok(result);
    }
}
