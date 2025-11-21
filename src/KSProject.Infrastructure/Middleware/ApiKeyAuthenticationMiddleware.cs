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
        if (endpoint?.Metadata.GetMetadata<FreeEndpoint>() != null)
        {
            await _next(context); // Skip for public endpoints
            return;
        }
        
        var superAdminStatusClaimExistence = context.User.Claims.Any(x => x.Type.Equals("is_SuperAdmin", StringComparison.CurrentCultureIgnoreCase));

        if (superAdminStatusClaimExistence)
        {
            var superAdminStatusClaim = context.User.Claims.FirstOrDefault(x => x.Type.Equals("is_SuperAdmin", StringComparison.CurrentCultureIgnoreCase));
            if (bool.Parse(superAdminStatusClaim.Value))
            {
                await _next(context);
                return;
            }
        }

        if (!context.Request.Headers.TryGetValue("X-Api-Key", out var apiKeyValue))
        {
            _logger.LogWarning("Api Key missing from request to {Path}", context.Request.Path);
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Api Key missing.");
            return;
        }
        
        var apiKey = await uow.Users.GetApiKeyByKeyAsync(apiKeyValue);
        if (apiKey == null || !apiKey.IsValid())
        {
            _logger.LogWarning("Invalid or expired Api Key: {ApiKeyValue} for request to {Path}", apiKeyValue, context.Request.Path);
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid or expired Api Key.");
            return;
        }

        // TOTO: We need to implement this one, but I am waiting to refactor ServiceRate Entity and change scopes and some other properties to ValueObject or entity
        // Optional: چک scopes اگر attribute RequiredScopes روی endpoint باشه
        // var requiredScopes = endpoint?.Metadata.GetMetadata<RequiredScopesAttribute>()?.Scopes;
        // if (requiredScopes != null && !apiKey.Scopes.Split(',').Select(s => s.Trim()).ToHashSet().IsSupersetOf(requiredScopes))
        // {
        //     _logger.LogWarning("Insufficient scopes for Api Key: {ApiKeyValue}", apiKeyValue);
        //     context.Response.StatusCode = 403;
        //     await context.Response.WriteAsync("Insufficient scopes.");
        //     return;
        // }

        await _next(context);
    }
}
