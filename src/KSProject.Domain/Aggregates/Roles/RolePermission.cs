using KSFramework.Exceptions;
using KSFramework.KSDomain;
using KSFramework.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Runtime.Serialization;

namespace KSProject.Domain.Aggregates.Roles;
public sealed class RolePermission : BaseEntity, ISerializable
{
	private RolePermission(Guid id,
		Guid roleId,
		string name) : base(id)
	{
		RoleId = roleId;
		if (!name.HasValue())
			throw new KSArgumentNullException(nameof(name));
		Name = name;
	}
	public Guid RoleId { get; private set; }
	public Role Role { get; private set; }
	public string Name { get; private set; } = string.Empty;

	public static RolePermission Create(Guid id,
		Guid roleId,
		string name)
	{
		RolePermission rolePermission = new(id, roleId, name);
		return rolePermission;
	}

	protected RolePermission()
	{

	}

	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue(nameof(Id), Id);
		info.AddValue(nameof(RoleId), RoleId);
		info.AddValue(nameof(Role), Role);
		info.AddValue(nameof(Name), Name);
	}
}

public sealed class RolePermissionsConfiguration : IEntityTypeConfiguration<RolePermission>
{
	public void Configure(EntityTypeBuilder<RolePermission> builder)
	{
		builder.HasKey(x => x.Id);
	}
}