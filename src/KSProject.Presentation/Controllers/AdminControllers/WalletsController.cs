using KSFramework.KSMessaging.Abstraction;
using KSProject.Application.Admin.Wallets.ChargeWallet;
using KSProject.Application.Admin.Wallets.GetWalletBalance;
using KSProject.Presentation.Attributes;
using KSProject.Presentation.BaseControllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KSProject.Presentation.Controllers.AdminControllers;

public class WalletsController(ISender sender) : SecureBaseController(sender)
{
    [HttpPost]
    [Permission("ChargeUserWallet")]
    [Route(Routes.Wallets_Admin.CHARGE_WALLET)]
    [Produces(typeof(ChargeWalletCommandResponse))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ChargeWalletCommandResponse>> GetAsync(
        [FromQuery]  Guid userId,
        [FromBody] ChargeWalletCommandRequest payload,
        CancellationToken cancellationToken = default)
    {
        ChargeWalletCommand command = new(userId, payload);

        ChargeWalletCommandResponse result = await Sender.Send(command, cancellationToken);

        return Ok(result);
    }
    
    [HttpGet]
    [Permission("GetUserWalletBalance")]
    [Route(Routes.Wallets_Admin.GET_BALANCE)]
    [Produces(typeof(GetWalletBalanceQueryResponse))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GetWalletBalanceQueryResponse>> GetBalanceAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        GetWalletBalanceQuery query = new(userId);

        GetWalletBalanceQueryResponse result = await Sender.Send(query, cancellationToken);

        return Ok(result);
    }
}
