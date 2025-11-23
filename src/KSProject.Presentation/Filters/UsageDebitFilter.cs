using KSFramework.KSMessaging.Abstraction;
using KSProject.Application.Admin.ApiKeys.GetUserIdByApiKey;
using KSProject.Application.Admin.Users.GetUserByApiKey;
using KSProject.Application.User.Billings.CalculateCost;
using KSProject.Application.User.Wallets.DebitWallet;
using KSProject.Common.Constants.Enums;
using KSProject.Domain.Attributes;
using KSProject.Domain.Contracts;
using KSProject.Presentation.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KSProject.Presentation.Filters;

public class UsageDebitFilter : IAsyncActionFilter
{
    private readonly ISender _sender;
    private readonly ICurrentUserService _currentUser;

    public UsageDebitFilter(ISender sender, ICurrentUserService currentUser)
    {
        _sender = sender;
        _currentUser = currentUser;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var executed = await next();

        if (executed.Exception != null || executed.Result is not OkObjectResult)
            return;

        var endpoint = context.HttpContext.GetEndpoint();
        var paidAttr = endpoint?.Metadata.GetMetadata<PaidServiceAttribute>();
        var serviceTypeAttr = endpoint?.Metadata.GetMetadata<ServiceTypeAttribute>();

        if (paidAttr == null || _currentUser.IsInternal)
            return;
        context.HttpContext.Request.Headers.TryGetValue("X-Api-Key", out var apiKeyValue);
        
        var userQuery = new GetUserByApiKeyQuery(new(apiKeyValue));
        var user = await _sender.Send(userQuery);
        
        if (user.IsInternal)
            return;
        if (user.IsSuperAdmin)
            return;

        var calculateRequest = new CalculateCostQueryRequest
        {
            ServiceType = serviceTypeAttr?.ServiceType ?? "Unknown",
            MetricType = paidAttr.MetricType,
            MetricValue = GetMetricValueFromContext(context),
            Variant = user.Variant
        };
        var calculateQuery = new CalculateCostQuery(calculateRequest);
        var costResponse = await _sender.Send(calculateQuery);
        
        
        // var getUserIdByApiKeyQuery = new GetUserIdByApiKeyQuery(apiKeyValue);
        // var userIdResponse = await _sender.Send(getUserIdByApiKeyQuery);

        if (costResponse.TotalCost > 0)
        {
            var debitRequest = new DebitWalletCommandRequest(user.Id,
                -costResponse.TotalCost,
                calculateRequest.MetricValue,
                TransactionTypes.Usage,
                calculateRequest.ServiceType,
                calculateRequest.MetricType);
            
            var debitCommand = new DebitWalletCommand(debitRequest);
            await _sender.Send(debitCommand);
        }
    }

    private decimal GetMetricValueFromContext(ActionExecutingContext context)
    {
        // customize - from arguments or body
        return 1m;
    }
}
