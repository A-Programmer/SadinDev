using KSFramework.KSMessaging.Abstraction;
using KSProject.Application.Admin.Wallets.GetWalletBalance;
using KSProject.Application.User.Billings.CalculateCost;
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

        if (paidAttr == null || currentUser.IsInternal)
        {
            await _next(context);
            return;
        }

        decimal metricValue = GetMetricValueFromRequest(context);

        var calculateRequest = new CalculateCostQueryRequest
        {
            ServiceType = endpoint.DisplayName ?? "Unknown",
            MetricType = paidAttr.MetricType,
            MetricValue = metricValue,
            Variant = "Default" // or from user claim/role
        };
        var calculateQuery = new CalculateCostQuery(calculateRequest);
        var costResponse = await sender.Send(calculateQuery);

        if (costResponse.TotalCost == 0)
        {
            await _next(context);
            return;
        }

        var balanceQuery = new GetWalletBalanceQuery(currentUser.UserId);
        var balanceResponse = await sender.Send(balanceQuery);

        if (balanceResponse.Balance < costResponse.TotalCost)
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
