using System.Reflection;
using KSFramework.ExtensionMethods;
using KSFramework.KSDomain;
using KSFramework.Utilities;
using KSProject.Infrastructure.Outbox;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KSProject.Infrastructure.Data;

public class KSProjectDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public KSProjectDbContext(DbContextOptions<KSProjectDbContext> options,
        IHttpContextAccessor httpContextAccessor)
        : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var entitiesAssembly = Domain.AssemblyReference.Assembly;

        #region Register All Entities
        modelBuilder.RegisterAllEntities<BaseEntity>(entitiesAssembly);
        #endregion

        // TODO: Fix this by using modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

        #region Apply Entities Configuration
        modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);
        #endregion
        
        
        
        
        
        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => v.Kind == DateTimeKind.Utc ? v : DateTime.SpecifyKind(v, DateTimeKind.Utc),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
        );

        var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
            v => v.HasValue
                ? (v.Value.Kind == DateTimeKind.Utc ? v : DateTime.SpecifyKind(v.Value, DateTimeKind.Utc))
                : v,
            v => v.HasValue
                ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc)
                : v
        );

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.IsOwned())
                continue;

            var dateTimeProps = entityType.ClrType
                .GetProperties()
                .Where(p => p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?));

            foreach (var prop in dateTimeProps)
            {
                var propertyBuilder = modelBuilder.Entity(entityType.ClrType).Property(prop.Name);

                if (prop.PropertyType == typeof(DateTime))
                    propertyBuilder.HasConversion(dateTimeConverter);
                else
                    propertyBuilder.HasConversion(nullableDateTimeConverter);
            }
        }
        
        
        
        
        
        
        #region Apply Soft Delete Global Query Filter
        modelBuilder.ApplyGlobalSoftDeleteFilter();
        #endregion

        #region Config Delete Behevior for not Cascade Delete
        //modelBuilder.AddRestrictDeleteBehaviorConvention();
        #endregion

        #region Add Sequential GUID for Id properties
        //modelBuilder.AddSequentialGuidForIdConvention();
        #endregion

        #region Pluralize Table Names
        modelBuilder.AddPluralizingTableNameConvention();
        #endregion

        #region Data Seeder

        modelBuilder.SeedData();

        #endregion
    }
    #region Fix Persian Chars
    public void FixYeke()
    {
        var changedEntities = ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
        foreach (var item in changedEntities)
        {
            if (item.Entity == null)
                continue;

            var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

            foreach (var property in properties)
            {
                var propName = property.Name;
                var val = (string)property.GetValue(item.Entity, null);

                if (val.HasValue())
                {
                    var newVal = val.Fa2En().FixPersianChars();
                    if (newVal == val)
                        continue;
                    property.SetValue(item.Entity, newVal, null);
                }
            }
        }
    }
    #endregion

    #region Setting detail fields

    public void SetDetailFields()
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.ModifiedAt = DateTime.UtcNow;
                entry.Entity.CreatedBy = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.ModifiedAt = DateTime.UtcNow;
                entry.Entity.ModifiedBy = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";
            }
        }
    }
    #endregion
}
