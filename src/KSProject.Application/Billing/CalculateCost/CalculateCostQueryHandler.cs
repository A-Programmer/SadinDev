using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Contracts;
using KSFramework.Exceptions;
using KSProject.Domain.Aggregates.Billings;

namespace KSProject.Application.Billing.CalculateCost;

public sealed class CalculateCostQueryHandler :
    IQueryHandler<CalculateCostQuery, CalculateCostQueryResponse>
{
    private readonly IKSProjectUnitOfWork _uow;

    public CalculateCostQueryHandler(IKSProjectUnitOfWork uow)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
    }

    public async Task<CalculateCostQueryResponse> Handle(CalculateCostQuery request,
        CancellationToken cancellationToken)
    {
        var rates = await _uow.ServiceRates.GetByServiceAndMetricAsync(request.Payload.ServiceType, request.Payload.MetricType, cancellationToken);

        ServiceRate? matchingRate = rates.FirstOrDefault(sr => sr.Variant == request.Payload.Variant) ?? rates.FirstOrDefault(sr => sr.Variant == "Default");

        if (matchingRate is null)
        {
            throw new KSNotFoundException("No rate found for the specified service and metric.");
        }

        decimal effectiveRate = matchingRate.CalculateEffectiveRate(request.Payload.MetricValue);
        decimal totalCost = effectiveRate * request.Payload.MetricValue;

        return new CalculateCostQueryResponse(totalCost, effectiveRate);
    }
}
