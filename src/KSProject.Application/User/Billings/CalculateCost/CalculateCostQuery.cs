using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.User.Billings.CalculateCost;

public sealed record CalculateCostQuery(CalculateCostQueryRequest Payload) : IQuery<CalculateCostQueryResponse>;
