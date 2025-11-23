[AttributeUsage(AttributeTargets.Method)]
public class ServiceTypeAttribute : Attribute
{
    public string ServiceType { get; }

    public ServiceTypeAttribute(string serviceType)
    {
        ServiceType = serviceType ?? throw new ArgumentNullException(nameof(serviceType));
    }
}
