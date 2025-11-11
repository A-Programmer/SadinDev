using KSFramework.GenericRepository;
using KSProject.Domain.Aggregates.Billings;
using Microsoft.EntityFrameworkCore;

namespace KSProject.Infrastructure.Data.Repositories
{
    public class ServiceRatesRepository : GenericRepository<ServiceRate>, IServiceRatesRepository
    {
        private readonly DbSet<ServiceRate> _serviceRates;
        public ServiceRatesRepository(DbContext context) : base(context)
        {
            _serviceRates = context.Set<ServiceRate>();
        }

        public async Task<IEnumerable<ServiceRate>> GetByServiceAndMetricAsync(string serviceType, string metricType, CancellationToken cancellationToken = default)
        {
            return await _serviceRates
                .Where(sr => sr.ServiceType == serviceType && sr.MetricType == metricType && !sr.IsDeleted)
                .ToListAsync(cancellationToken);
        }
    }
}
