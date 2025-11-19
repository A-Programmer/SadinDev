using KSFramework.KSMessaging.Abstraction;
using KSProject.Application.Billing.CalculateCost;
using KSProject.Application.Wallets.DebitWallet;
using KSProject.Common.Constants.Enums;
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

        if (paidAttr == null || _currentUser.IsInternal)
            return;

        var calculateRequest = new CalculateCostQueryRequest
        {
            ServiceType = endpoint.DisplayName ?? "Unknown",
            MetricType = paidAttr.MetricType,
            MetricValue = GetMetricValueFromContext(context),
            Variant = "Default"
        };
        var calculateQuery = new CalculateCostQuery(calculateRequest);
        var costResponse = await _sender.Send(calculateQuery);

        if (costResponse.TotalCost > 0)
        {
            var debitRequest = new DebitWalletCommandRequest(_currentUser.UserId,
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
