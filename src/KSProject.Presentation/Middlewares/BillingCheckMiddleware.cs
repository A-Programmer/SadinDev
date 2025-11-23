using KSFramework.KSMessaging.Abstraction;
using KSProject.Application.Admin.Users.GetUserByApiKey;
using KSProject.Application.Admin.Wallets.GetWalletBalance;
using KSProject.Application.Admin.Wallets.GetWalletBalanceByApiKey;
using KSProject.Application.User.Billings.CalculateCost;
using KSProject.Common.Extensions;
using KSProject.Domain.Attributes;
using KSProject.Domain.Contracts;
using KSProject.Presentation.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace KSProject.Presentation.Middlewares;

public class BillingCheckMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<BillingCheckMiddleware> _logger;

    public BillingCheckMiddleware(RequestDelegate next, ILogger<BillingCheckMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, ISender sender, ICurrentUserService currentUser)
    {
        var endpoint = context.GetEndpoint();
        var paidAttr = endpoint?.Metadata.GetMetadata<PaidServiceAttribute>();
        var serviceTypeAttr = endpoint?.Metadata.GetMetadata<ServiceTypeAttribute>();
        
        if (!context.Request.Headers.TryGetValue("X-Api-Key", out var apiKeyValue))
        {
            _logger.LogWarning("Api Key missing from request to {Path}", context.Request.Path);
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Api Key missing.");
            return;
        }

        if (endpoint?.Metadata.GetMetadata<FreeEndpoint>() != null || endpoint?.Metadata.GetMetadata<PaidServiceAttribute>() == null)
        {
            await _next(context);
            return;
        }
        
        var userQuery = new GetUserByApiKeyQuery(new(apiKeyValue));
        var user = await sender.Send(userQuery);
        
        if (user.IsInternal || user.IsSuperAdmin)
        {
            await _next(context);
            return;
        }
        if (!user.IsUserActive)
        {
            _logger.LogWarning("The user is not active");
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("The user is not active");
            return;
        }
        if (!user.IsApiKeyActive)
        {
            _logger.LogWarning("Your API Key is not active");
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Your API Key is not active");
            return;
        }

        if (user.Domain.ToLower() != context.GetOnlyDomain().ToLower())
        {
            _logger.LogWarning("This API Key does not belong to this domain");
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("This API Key does not belong to this domain");
            return;
        }

        decimal metricValue = GetMetricValueFromRequest(context);

        var calculateRequest = new CalculateCostQueryRequest
        {
            ServiceType = serviceTypeAttr?.ServiceType ?? "Unknown",
            MetricType = paidAttr.MetricType,
            MetricValue = metricValue,
            Variant = user.Variant
        };
        var calculateQuery = new CalculateCostQuery(calculateRequest);
        var costResponse = await sender.Send(calculateQuery);

        if (costResponse.TotalCost == 0)
        {
            await _next(context);
            return;
        }

        var balanceQueryByApiKey = new GetWalletBalanceByApiKeyQuery(apiKeyValue);
        var balanceResponseByApiKey = await sender.Send(balanceQueryByApiKey);
        
        if (balanceResponseByApiKey.Balance < costResponse.TotalCost)
        {
            _logger.LogWarning("Insufficient balance for user {UserId} on endpoint {Path}", currentUser.UserId, context.Request.Path);
            context.Response.StatusCode = 402;
            await context.Response.WriteAsync("Insufficient balance.");
            return;
        }

        await _next(context);
    }

    private decimal GetMetricValueFromRequest(HttpContext context)
    {
        if (context.Request.Query.TryGetValue("count", out var valueStr) && decimal.TryParse(valueStr, out var value))
            return value;

        return 1m; // default for simple calls
    }
}
