using System.Runtime.Serialization;
using KSFramework.KSDomain;
using KSFramework.KSDomain.AggregatesHelper;
using KSFramework.Utilities;
using KSProject.Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KSProject.Domain.Aggregates.Roles;

public sealed class Role : BaseEntity, IAggregateRoot, ISerializable
{
    private Role(Guid id,
        string name,
        string description) : base(id)
    {
        if (!name.HasValue())
            throw new ArgumentNullException(nameof(name));
        Name = name;

        if (description.HasValue())
            Description = description;
    }
    
    public string Name { get; private set; }
    public string Description { get; private set; } = string.Empty;

    private List<User> _users = new();
    public IReadOnlyCollection<User> Users => _users;

    public void Update(string name,
        string description)
    {
        if (!name.HasValue())
            throw new ArgumentNullException(nameof(name));
        Name = name;

        if (!description.HasValue())
            Description = description;
    }

    public static Role Create(Guid id,
        string name,
        string description)
    {
        Role role = new(id, name, description);

        return role;
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(Id), Id);
        info.AddValue(nameof(Name), Name);
        info.AddValue(nameof(Description), Description);
    }
}

public class UserConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);
    }
}