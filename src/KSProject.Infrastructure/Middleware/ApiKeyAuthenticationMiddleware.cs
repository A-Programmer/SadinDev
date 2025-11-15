using System.Security.Claims;
using KSProject.Domain.Aggregates.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace KSProject.Infrastructure.Middleware;

public static class UseApiKeyAuthenticationExtensionMethod
{
    public static IApplicationBuilder UseApiKeyAuthentication(this IApplicationBuilder app)
    {
        app.UseMiddleware<ApiKeyAuthenticationMiddleware>();
        return app;
    }
}

public class ApiKeyAuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public ApiKeyAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IUsersRepository userRepository)
    {
        if (!context.Request.Headers.TryGetValue("X-Api-Key", out var apiKeyValue))
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("Api Key missing.");
            return;
        }

        var apiKey = await userRepository.GetApiKeyByKeyAsync(apiKeyValue);
        if (apiKey == null || !apiKey.IsActive || apiKey.IsExpired())
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid or expired Api Key.");
            return;
        }

        // Set user claims if needed (مثل UserId برای SecureBaseController)
        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, apiKey.UserId.ToString())
        }, "ApiKey");
        context.User = new ClaimsPrincipal(identity);

        await _next(context);
    }
}
