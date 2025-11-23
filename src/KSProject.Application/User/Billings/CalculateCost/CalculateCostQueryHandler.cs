using KSFramework.Exceptions;
using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Aggregates.Billings;
using KSProject.Domain.Contracts;

namespace KSProject.Application.User.Billings.CalculateCost;

public sealed class CalculateCostQueryHandler :
    IQueryHandler<CalculateCostQuery, CalculateCostQueryResponse>
{
    private readonly IKSProjectUnitOfWork _uow;
    private readonly ICurrentUserService _currentUser;

    public CalculateCostQueryHandler(IKSProjectUnitOfWork uow,
        ICurrentUserService currentUser)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    public async Task<CalculateCostQueryResponse> Handle(CalculateCostQuery request,
        CancellationToken cancellationToken)
    {
        if (_currentUser.IsInternal)
        {
            return new CalculateCostQueryResponse(0, 0); // free
        }
        
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
