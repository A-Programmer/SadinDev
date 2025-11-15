using System.Security.Claims;
using KSProject.Domain.Aggregates.Users;
using KSProject.Domain.Attributes;
using KSProject.Domain.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

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
    private readonly ILogger<ApiKeyAuthenticationMiddleware> _logger;

    public ApiKeyAuthenticationMiddleware(RequestDelegate next, ILogger<ApiKeyAuthenticationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, IKSProjectUnitOfWork uow)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint?.Metadata.GetMetadata<PublicEndpointAttribute>() != null)
        {
            await _next(context); // Skip for public endpoints
            return;
        }
        
        if (!context.Request.Headers.TryGetValue("X-Api-Key", out var apiKeyValue))
        {
            _logger.LogWarning("Api Key missing from request.");
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Api Key missing.");
            return;
        }

        var apiKey = await uow.Users.GetApiKeyByKeyAsync(apiKeyValue);
        if (apiKey == null || !apiKey.IsActive || apiKey.IsExpired())
        {
            _logger.LogWarning("Invalid or expired Api Key: {ApiKeyValue}", apiKeyValue);
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid or expired Api Key.");
            return;
        }

        // چک scopes اگر نیاز باشه (مثل برای endpoint خاص)
        // var requiredScopes = context.GetEndpoint()?.Metadata.GetMetadata<RequiredScopesAttribute>()?.Scopes;
        // if (requiredScopes != null && !apiKey.Scopes.Split(',').Any(s => requiredScopes.Contains(s.Trim())))
        // {
        //     context.Response.StatusCode = 403;
        //     await context.Response.WriteAsync("Insufficient scopes.");
        //     return;
        // }

        // Set user claims
        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, apiKey.UserId.ToString())
        }, "ApiKey");
        context.User = new ClaimsPrincipal(identity);

        await _next(context);
    }
}
