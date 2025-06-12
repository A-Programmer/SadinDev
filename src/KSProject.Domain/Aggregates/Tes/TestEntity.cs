using System.Runtime.Serialization;
using KSFramework.KSDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KSProject.Domain.Aggregates.Tes;

public class TestEntity : BaseEntity, ISerializable
{

    public string Title { get; private set; }
    public Guid TestAggregateId { get; private set; }
    public TestAggregate TestAggregate { get; private set; }
    
    
    private TestEntity(Guid id,
        string title)
        : base(id)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
    }

    public static TestEntity Create(string title) =>
        new(Guid.NewGuid(),
            title);
    
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(Id), Id);
        info.AddValue(nameof(Title), Title);
    }
}

public sealed class TestEntitiesConfiguration : IEntityTypeConfiguration<TestEntity>
{
    public void Configure(EntityTypeBuilder<TestEntity> builder)
    {
        builder.HasKey(x => x.Id);
    }
}