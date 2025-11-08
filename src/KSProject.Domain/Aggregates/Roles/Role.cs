using KSFramework.KSDomain;
using KSFramework.KSDomain.AggregatesHelper;
using KSFramework.Utilities;
using KSProject.Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Runtime.Serialization;

namespace KSProject.Domain.Aggregates.Roles;

/// <summary>
/// Represents a role within the system, containing permissions and users.
/// </summary>
public sealed class Role : BaseEntity, IAggregateRoot, ISerializable, ISoftDeletable
{
	/// <summary>
	/// Initializes a new instance of the <see cref="Role"/> class with the specified id, name, and description.
	/// </summary>
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

	/// <summary>
	/// Gets the name of the role.
	/// </summary>
	public string Name { get; private set; }

    public bool IsDeleted { get; set; }
    public DateTime? DeletedOnUtc { get; set; }

	/// <summary>
	/// Gets the description of the role.
	/// </summary>
	public string Description { get; private set; } = string.Empty;

	private List<User> _users = new();

	/// <summary>
	/// Gets the users assigned to this role.
	/// </summary>
	public IReadOnlyCollection<User> Users => _users;

	private List<RolePermission> _permissions = new();

	/// <summary>
	/// Gets the permissions assigned to this role.
	/// </summary>
	public IReadOnlyCollection<RolePermission> Permissions => _permissions;

	/// <summary>
	/// Updates the name and description of the role.
	/// </summary>
	/// <param name="name">The new name for the role.</param>
	/// <param name="description">The new description for the role.</param>
	public void Update(string name,
		string description)
	{
		if (!name.HasValue())
			throw new ArgumentNullException(nameof(name));
		Name = name;

		if (!description.HasValue())
			Description = description;
	}

	/// <summary>
	/// Creates a new <see cref="Role"/> instance with the specified id, name, and description.
	/// </summary>
	/// <param name="id">The unique identifier for the role.</param>
	/// <param name="name">The name of the role.</param>
	/// <param name="description">The description of the role.</param>
	/// <returns>A new <see cref="Role"/> object.</returns>
	public static Role Create(Guid id,
		string name,
		string description)
	{
		Role role = new(id, name, description);

		return role;
	}

	/// <summary>
	/// Adds a permission to the role.
	/// </summary>
	/// <param name="permission">The permission to add.</param>
	/// <exception cref="ArgumentNullException">Thrown when the permission is null.</exception>
	public void AddPermission(RolePermission permission)
	{
		if (permission == null)
			throw new ArgumentNullException(nameof(permission));
		if (_permissions.Contains(permission))
			return;
		_permissions.Add(permission);
	}

	/// <summary>
	/// Removes a permission from the role.
	/// </summary>
	/// <param name="permission">The permission to remove.</param>
	/// <exception cref="ArgumentNullException">Thrown when the permission is null.</exception>
	public void RemovePermission(RolePermission permission)
	{
		if (permission == null)
			throw new ArgumentNullException(nameof(permission));
		if (!_permissions.Contains(permission))
			return;
		_permissions.Remove(permission);
	}

	/// <summary>
	/// Removes all permissions from the role.
	/// </summary>
	public void ClearPermissions()
	{
		_permissions.Clear();
	}

	/// <summary>
	/// Adds a user to the role.
	/// </summary>
	/// <param name="user">The user to add.</param>
	/// <exception cref="ArgumentNullException">Thrown when the user is null.</exception>
	public void AddUser(User user)
	{
		if (user == null)
			throw new ArgumentNullException(nameof(user));
		if (_users.Contains(user))
			return;
		_users.Add(user);
	}

	/// <summary>
	/// Removes a user from the role.
	/// </summary>
	/// <param name="user">The user to remove.</param>
	/// <exception cref="ArgumentNullException">Thrown when the user is null.</exception>
	public void RemoveUser(User user)
	{
		if (user == null)
			throw new ArgumentNullException(nameof(user));
		if (!_users.Contains(user))
			return;
		_users.Remove(user);
	}

	/// <summary>
	/// Removes all users from the role.
	/// </summary>
	public void ClearUsers()
	{
		_users.Clear();
	}

	/// <summary>
	/// Creates a new <see cref="Role"/> instance with a generated id, name, and optional description.
	/// </summary>
	/// <param name="name">The name of the role.</param>
	/// <param name="description">The description of the role. Defaults to an empty string.</param>
	/// <returns>A new <see cref="Role"/> object.</returns>
	public static Role Create(string name,
		string description = "")
	{
		return Create(Guid.NewGuid(), name, description);
	}

	/// <summary>
	/// Populates a <see cref="SerializationInfo"/> with the data needed to serialize the role.
	/// </summary>
	/// <param name="info">The serialization information to populate.</param>
	/// <param name="context">The destination for this serialization.</param>
	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue(nameof(Id), Id);
		info.AddValue(nameof(Name), Name);
		info.AddValue(nameof(Description), Description);
	}
}

/// <summary>
/// Entity Framework configuration for the <see cref="Role"/> entity.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<Role>
{
	/// <summary>
	/// Configures the <see cref="Role"/> entity type.
	/// </summary>
	/// <param name="builder">The builder to be used to configure the entity type.</param>
	public void Configure(EntityTypeBuilder<Role> builder)
	{
		builder.HasKey(r => r.Id);

		builder
			.HasMany(r => r.Permissions)
			.WithOne(p => p.Role)
			.HasForeignKey(x => x.RoleId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
