using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Billing.CalculateCost;

public sealed record CalculateCostQuery(CalculateCostQueryRequest Payload) : IQuery<CalculateCostQueryResponse>;
