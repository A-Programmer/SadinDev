using KSFramework.KSMessaging.Abstraction;
using KSProject.Application.Billing.CalculateCost;
using KSProject.Presentation.BaseControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KSProject.Presentation.Controllers.AdminControllers;

[AllowAnonymous] // یا [Authorize] بر اساس نیاز
public class BillingController(ISender sender) : SecureBaseController(sender)
{
    [HttpPost]
    [AllowAnonymous] // اگر عمومی باشه, иначе [Permission("CalculateCost")]
    [Route(Routes.Billings_Admin.CALCULATE_COST)] // فرض Routes ثابت
    [Produces(typeof(CalculateCostQueryResponse))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CalculateCostQueryResponse>> CalculateCostAsync(
        [FromBody] CalculateCostQueryRequest payload,
        CancellationToken cancellationToken = default)
    {
        CalculateCostQuery query = new(payload);
        CalculateCostQueryResponse result = await Sender.Send(query, cancellationToken);

        return Ok(result);
    }
}
