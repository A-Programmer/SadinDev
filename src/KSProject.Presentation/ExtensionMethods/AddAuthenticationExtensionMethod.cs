using KSProject.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace KSProject.Presentation.ExtensionMethods;
public static class AddAuthenticationExtensionMethod
{
	public static IServiceCollection AddCustomAuthentication(this IServiceCollection services,
		JwtOptions settings)
	{
		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

		}).AddJwtBearer(options =>
		{
			var secretKey = Encoding.UTF8.GetBytes(settings.SecretKey);

			options.RequireHttpsMetadata = false;
			options.SaveToken = true;
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ClockSkew = TimeSpan.Zero,
				RequireSignedTokens = true,

				ValidateIssuer = true,
				ValidIssuer = settings.Issuer,

				ValidateAudience = true,
				ValidAudience = settings.Audience,

				RequireExpirationTime = true,
				ValidateLifetime = true,

				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(secretKey)
			};

			// options.Events = new JwtBearerEvents
			// {
			//     OnAuthenticationFailed = context =>
			//     {
			//         if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
			//         {
			//             context.Response.Headers.Add("Token-Expired", "true");
			//         }
			//
			//         if (context.Exception != null)
			//             throw new UnauthorizedAccessException();
			//         return Task.CompletedTask;
			//     },
			//     OnTokenValidated = async context =>
			//     {
			//         var _mediator = context.HttpContext.RequestServices.GetRequiredService<IMediator>();
			//
			//         var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
			//         if (claimsIdentity.Claims?.Any() != true)
			//             context.Fail("No claims has been found.");
			//
			//         var securityStamp =
			//             claimsIdentity.FindFirstValue(new ClaimsIdentityOptions().SecurityStampClaimType);
			//
			//         if (!securityStamp.HasValue())
			//             throw new UnauthorizedAccessException("No security stamp found.");
			//
			//         var userIdString = claimsIdentity.GetUserId();
			//         var userId = new Guid();
			//         var userIdConvertResult = Guid.TryParse(userIdString, out userId);
			//
			//         var getUserByIdQuery = new GetUserByIdQuery(userId);
			//         var user = await _mediator.Send(getUserByIdQuery);
			//
			//         //var user = await userService.FindByIdAsync(userIdString);
			//
			//
			//         // //Validate by securitystamp
			//         // var validateSecurityStampCommand = new ValidateSecurityStampCommand(securityStamp);
			//         // var validateSecurityStampResult = await _mediator.Send(validateSecurityStampCommand);
			//         //
			//         // if (!validateSecurityStampResult)
			//         //     throw new AppException(ApiResultStatusCode.ServerError, "مهر امنیتی معتبر نمی باشد", HttpStatusCode.Unauthorized);
			//         //
			//         // //Custom validation like IsActive
			//         // if (!user.IsActive)
			//         //     context.Fail("کاربر فعال نیست");
			//         //
			//         // var updateLastLoginDateCommand = new UpdateLastLoginDateCommand(user.Id);
			//         // await _mediator.Send(updateLastLoginDateCommand);
			//     },
			//     OnChallenge = context =>
			//     {
			//         // Skip the default logic.
			//         context.HandleResponse();
			//
			//         var payload = new JObject
			//         {
			//             ["status"] = false, ["message"] = "کاربر احراز نشده است", ["code"] = 401
			//         };
			//
			//         context.Response.ContentType = "application/json";
			//         context.Response.StatusCode = 401;
			//
			//         return context.Response.WriteAsync(payload.ToString());
			//
			//         //if (context.AuthenticateFailure != null)
			//         //    throw new AuthenticationException(context.AuthenticateFailure.Message, context.AuthenticateFailure.InnerException);
			//         //throw new UnauthorizedAccessException("OnChallenge exception" + context.Error + "--" + context.ErrorDescription);
			//
			//     }
			// };
		});
		return services;
	}
}
