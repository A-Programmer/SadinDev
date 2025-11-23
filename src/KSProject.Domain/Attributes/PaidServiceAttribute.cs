namespace KSProject.Domain.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class PaidServiceAttribute : Attribute
{
    public string MetricType { get; }

    public PaidServiceAttribute(string metricType)
    {
        MetricType = metricType ?? throw new ArgumentNullException(nameof(metricType));
    }
}
