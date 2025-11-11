using KSFramework.Contracts;

namespace KSProject.Application.Billing.CalculateCost;
public sealed class CalculateCostQueryRequest : IInjectable
{
    public required string ServiceType { get; set; }
    public required string MetricType { get; set; }
    public required decimal MetricValue { get; set; }
    public string Variant { get; set; } = "Default";
}
