using System.Runtime.Serialization;
using KSFramework.KSDomain;
using KSFramework.KSDomain.AggregatesHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KSProject.Domain.Aggregates.Test;

public class TestAggregate : BaseEntity, IAggregateRoot, ISerializable
{
    protected TestAggregate(Guid id)
        : base(id)
    { }
    
    private TestAggregate(string title, string content)
    {
        Id = Guid.NewGuid();
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Content = content ?? throw new ArgumentNullException(nameof(content));
        _entities = new List<TestEntity>();
    }

    public static TestAggregate Create(string title, string content)
    {
        TestAggregate testAggregate = new(title, title);
        // Raise Event
        return testAggregate;
    }
    
    public void Update(string title, string content)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Content = content ?? throw new ArgumentNullException(nameof(content));
    }
    
    public void AddTestEntity(TestEntity testEntity)
    {
        _entities.Add(testEntity);
    }

    public void RemoveTestEntity(TestEntity testEntity)
    {
        _entities.Remove(testEntity);
    }

    public string Title { get; private set; }
    public string Content { get; private set; }

    private readonly List<TestEntity> _entities;
    public IReadOnlyCollection<TestEntity> Entities => _entities;
    
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(Id), Id);
        info.AddValue(nameof(Title), Title);
        info.AddValue(nameof(Content), Content);
    }
}

public sealed class TestAggregatessConfiguration : IEntityTypeConfiguration<TestAggregate>
{
    public void Configure(EntityTypeBuilder<TestAggregate> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasMany(a => a.Entities)
            .WithOne(ac => ac.TestAggregate)
            .HasForeignKey(ac => ac.TestAggregateId);
    }
}