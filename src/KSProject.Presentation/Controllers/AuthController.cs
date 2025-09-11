using KSFramework.KSMessaging.Abstraction;
using KSProject.Application.Users.CheckUserExistence;
using KSProject.Application.Users.Login;
using KSProject.Application.Users.Register;
using KSProject.Application.Users.ValidateUser;
using KSProject.Common.Exceptions;
using KSProject.Presentation.BaseControllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KSProject.Presentation.Controllers;

public class AuthController : BaseController
{
    public AuthController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    [Route(Routes.Auth.LOGIN)]
    [Produces(typeof(LoginResponse))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LoginResponse>> PostAsync([FromBody] LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        CheckUserExistenceRequest existenceRequest = new()
        {
            UserNameOrEmailOrPhoneNumber = request.UserName
        };
        CheckUserExistenceQuery checkUserExistenceQuery = new(existenceRequest);

        CheckUserExistenceResponse userExistenceResponse = await Sender.Send(checkUserExistenceQuery, cancellationToken);
        if (userExistenceResponse?.Id is null)
        {
            throw new KsAuthenticationException("Invalid Username or Password");
        }

        ValidateUserQuery validateUserQuery = new(new ValidateUserQueryRequest()
        {
            UserName = request.UserName,
            Password = request.Password
        });

        ValidateUserQueryResponse? validateUserQueryResponse = await Sender.Send(validateUserQuery,
            cancellationToken);

        if (validateUserQueryResponse?.Id is null)
        {
            throw new KsAuthenticationException("Username of Password is not correct.");
        }

        if (!validateUserQueryResponse.IsActive)
        {
            throw new KsUserIsNotActiveException("User is not active, please contact with admin.");
        }

        LoginCommand command = new(new()
        {
            UserName = request.UserName,
            Password = request.Password,
            IpAddress = GetUserIp()
        });

        LoginResponse token = await Sender.Send(command, cancellationToken);

        return Ok(token);
    }


    [HttpPost]
    [Route(Routes.Auth.REGISTER)]
    [Produces(typeof(RegisterResponse))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RegisterResponse>> PostAsync([FromBody] RegisterRequest request,
        CancellationToken cancellationToken = default)
    {

        CheckUserExistenceRequest existenceRequest = new()
        {
            UserNameOrEmailOrPhoneNumber = request.UserName
        };
        CheckUserExistenceQuery checkUserExistenceQuery = new(existenceRequest);

        CheckUserExistenceResponse userExistenceResponse = await Sender.Send(checkUserExistenceQuery, cancellationToken);
        if (userExistenceResponse?.Id is not null)
        {
            throw new KsAuthenticationException("This user name/email/mobile is taken, please change the user name and try again.");
        }

        RegisterCommand registerCommand = new(new()
        {
            UserName = request.UserName,
            Password = request.Password,
            ConfirmPassword = request.ConfirmPassword,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            FirstName = request.FirstName,
            LastName = request.LastName,
            AboutMe = request.AboutMe,
            BirthDateUtc = request.BirthDateUtc
        });

        RegisterResponse registerResponse = await Sender.Send(registerCommand, cancellationToken);

        LoginCommand command = new(new()
        {
            UserName = request.UserName,
            Password = request.Password,
            IpAddress = GetUserIp()
        });

        LoginResponse token = await Sender.Send(command, cancellationToken);

        return Ok(token);
    }
}
