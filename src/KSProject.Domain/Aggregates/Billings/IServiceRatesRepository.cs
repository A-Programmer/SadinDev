using KSFramework.GenericRepository;

namespace KSProject.Domain.Aggregates.Billings;

public interface IServiceRatesRepository : IRepository<ServiceRate>
{
    Task<IEnumerable<ServiceRate>> GetByServiceAndMetricAsync(string serviceType, string metricType, CancellationToken cancellationToken = default);
}
