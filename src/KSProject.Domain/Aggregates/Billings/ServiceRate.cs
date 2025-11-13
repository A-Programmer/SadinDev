using System.Text.Json;
using KSFramework.KSDomain;
using KSFramework.KSDomain.AggregatesHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
// برای parse RulesJson

// برای Dictionary در rules

namespace KSProject.Domain.Aggregates.Billings;

public sealed class ServiceRate : BaseEntity, IAggregateRoot, ISoftDeletable
{
    public string ServiceType { get; private set; }
    public string MetricType { get; private set; }
    public string Variant { get; private set; } = "Default";
    public decimal RatePerUnit { get; private set; }
    public string? RulesJson { get; private set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOnUtc { get; set; }

    private ServiceRate(Guid id, string serviceType, string metricType, string variant, decimal ratePerUnit, string? rulesJson) : base(id)
    {
        ServiceType = serviceType ?? throw new ArgumentNullException(nameof(serviceType));
        MetricType = metricType ?? throw new ArgumentNullException(nameof(metricType));
        Variant = variant ?? "Default";
        RatePerUnit = ratePerUnit >= 0 ? ratePerUnit : throw new ArgumentException("RatePerUnit cannot be negative.");
        RulesJson = rulesJson;
    }

    // Factory method for creation
    public static ServiceRate Create(Guid id, string serviceType, string metricType, string variant = "Default", decimal ratePerUnit = 0.0m, string? rulesJson = null)
    {
        var serviceRate = new ServiceRate(id, serviceType, metricType, variant, ratePerUnit, rulesJson);
        // Optional: AddDomainEvent(new ServiceRateCreatedEvent { ... }); if needed for events
        return serviceRate;
    }

    // Behavioral methods
    public void UpdateRate(decimal newRatePerUnit, string newVariant = null, string? newRulesJson = null)
    {
        RatePerUnit = newRatePerUnit >= 0 ? newRatePerUnit : throw new ArgumentException("New RatePerUnit cannot be negative.");
        if (!string.IsNullOrEmpty(newVariant))
            Variant = newVariant;
        RulesJson = newRulesJson;
        // Optional: AddDomainEvent(new ServiceRateUpdatedEvent { ... });
    }

    // Calculate effective rate with rules (e.g., for tiers or discounts)
    public decimal CalculateEffectiveRate(decimal metricValue)
    {
        if (string.IsNullOrEmpty(RulesJson))
            return RatePerUnit;

        var rules = JsonSerializer.Deserialize<Dictionary<string, object>>(RulesJson) ?? new Dictionary<string, object>();
        if (rules.TryGetValue("minQuantity", out var minObj) && metricValue > Convert.ToDecimal(minObj))
        {
            if (rules.TryGetValue("discountPercent", out var discObj))
            {
                decimal discount = Convert.ToDecimal(discObj) / 100;
                return RatePerUnit * (1 - discount);
            }
        }
        return RatePerUnit;
    }

    // For future: Validate rules JSON
    public void ValidateRules()
    {
        if (!string.IsNullOrEmpty(RulesJson))
        {
            try
            {
                JsonSerializer.Deserialize<Dictionary<string, object>>(RulesJson);
            }
            catch
            {
                throw new InvalidOperationException("Invalid RulesJson format.");
            }
        }
    }

    protected ServiceRate() { } // For EF Core
}

public class ServiceRateConfiguration : IEntityTypeConfiguration<ServiceRate>
{
    public void Configure(EntityTypeBuilder<ServiceRate> builder)
    {
        builder.HasKey(sr => sr.Id);
        builder.Property(sr => sr.IsDeleted);
        builder.HasIndex(sr => new { sr.ServiceType, sr.MetricType, sr.Variant }).IsUnique();
        builder.Property(sr => sr.RulesJson).HasColumnType("jsonb"); // اگر PostgreSQL, иначе nvarchar(max)
    }
}
