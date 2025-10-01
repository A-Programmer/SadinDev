using KSFramework.KSMessaging.Abstraction;
using KSProject.Application.Otp.SendOtp;
using KSProject.Application.Users.CheckUserExistence;
using KSProject.Presentation.BaseControllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KSProject.Presentation.Controllers;

public class OtpController : BaseController
{
    public OtpController(ISender sender) : base(sender)
    {
    }
    
    [HttpPost]
    [Route(Routes.Otp.LOGIN)]
    [Produces(typeof(SendOtpResponse))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SendOtpResponse>> PostAsync(SendOtpRequest request,
        CancellationToken cancellationToken = default)
    {
        CheckUserExistenceRequest existenceRequest = new()
        {
            UserNameOrEmailOrPhoneNumber = request.UserNameOrEmailOrPhoneNumber
        };
        CheckUserExistenceQuery checkUserExistenceQuery = new(existenceRequest);

        CheckUserExistenceResponse userExistenceResponse = await Sender.Send(checkUserExistenceQuery, cancellationToken);
        
        if (userExistenceResponse?.Id is null)
        {
            // Create new User
            // Generate OTP and send to user
            // Fill the response
            // return the result
        }
        else
        {
            // Generate OTP and send to user
            // Fill the response
            // return the result
        }
        
        return Ok();
    }
}
