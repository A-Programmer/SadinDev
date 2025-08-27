using KSFramework.Exceptions;
using KSFramework.KSDomain;
using KSFramework.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Runtime.Serialization;

namespace KSProject.Domain.Aggregates.Users;
public sealed class UserPermission : BaseEntity, ISerializable
{
	private UserPermission(Guid id,
		Guid userId,
		string name) : base(id)
	{
		UserId = userId;
		if (!name.HasValue())
			throw new KSArgumentNullException(nameof(name));
		Name = name;
	}
	public Guid? UserId { get; private set; }
	public User? User { get; private set; }
	public string Name { get; private set; } = string.Empty;

	public static UserPermission Create(
		Guid userId,
		string name)
	{
		UserPermission userPermission = new(Guid.NewGuid(), userId, name);
		return userPermission;
	}

	protected UserPermission()
	{

	}

	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue(nameof(Id), Id);
		info.AddValue(nameof(UserId), UserId);
		info.AddValue(nameof(User), User);
		info.AddValue(nameof(Name), Name);
	}
}

public sealed class UserPermissionsConfiguration : IEntityTypeConfiguration<UserPermission>
{
	public void Configure(EntityTypeBuilder<UserPermission> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.UserId).IsRequired(false);
	}
}