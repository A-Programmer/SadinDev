using KSFramework.KSMessaging.Abstraction;
using KSFramework.Utilities;
using KSProject.Common.Constants.Enums;
using KSProject.Domain.Attributes;
using KSProject.Presentation.BaseControllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KSProject.Presentation.Controllers.PublicControllers
{
    public sealed class StaticDataController : BaseController
    {
        public StaticDataController(ISender sender) : base(sender) { }

        [PublicEndpoint]
        [HttpGet]
        [Route(Routes.Static_Data.GET_PAYMENT_GTEWAYS)]
        [Produces(typeof(Dictionary<int, string>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPaymentGatewayTypes()
        {
            return Ok(EnumExtensions.ToDescriptionsDictionary<PaymentGatewayTypes>());
        }
    }
}
