using KSFramework.KSDomain;
using KSFramework.KSDomain.AggregatesHelper;
using KSFramework.Utilities;
using KSProject.Domain.Aggregates.Roles;
using KSProject.Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Runtime.Serialization;

namespace KSProject.Domain.Aggregates.Permissions;

public sealed class Permission : BaseEntity, IAggregateRoot, ISerializable
{
	private Permission(Guid id,
		string name,
		string title) : base(id)
	{
		Name = !name.HasValue() ? throw new ArgumentNullException(nameof(name)) : name;
		Title = !title.HasValue() ? throw new ArgumentNullException(nameof(title)) : title;
	}

	public string Name { get; private set; }
	public string Title { get; private set; }


	private List<Role> _roles = new();
	public IReadOnlyCollection<Role> Roles => _roles;


	private List<User> _users = new();
	public IReadOnlyCollection<User> Users => _users;

	public void Update(
		string name,
		string title)
	{
		Name = !name.HasValue() ? throw new ArgumentNullException(nameof(name)) : name;
		Title = !title.HasValue() ? throw new ArgumentNullException(nameof(title)) : title;
	}

	public static Permission Create(
		Guid id,
		string name,
		string title)
	{
		Permission permission = new(id, name, title);

		return permission;
	}

	public void AssignPermissionTRole(Role role)
	{
		if (role == null)
			throw new ArgumentNullException(nameof(role), "Role cannot be null.");
		if (_roles.Contains(role))
			return;
		_roles.Add(role);
	}

	public void UnAssignPermissionFromRole(Role role)
	{
		if (role == null)
			throw new ArgumentNullException(nameof(role), "Role cannot be null.");
		if (!_roles.Contains(role))
			return;
		_roles.Remove(role);
	}

	public void ClearRoles()
	{
		_roles.Clear();
	}

	public void AssignPermissionToUser(User user)
	{
		if (user == null)
			throw new ArgumentNullException(nameof(user), "User cannot be null.");
		if (_users.Contains(user))
			return;
		_users.Add(user);
	}

	public void UnAssignPermissionFromUser(User user)
	{
		if (user == null)
			throw new ArgumentNullException(nameof(user), "User cannot be null.");
		if (!_users.Contains(user))
			return;
		_users.Remove(user);
	}

	public void ClearUsers()
	{
		_users.Clear();
	}

	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue(nameof(Id), Id);
		info.AddValue(nameof(Name), Name);
		info.AddValue(nameof(Title), Title);
	}
}

public sealed class PermissionConfiguration
	: IEntityTypeConfiguration<Permission>
{
	public void Configure(EntityTypeBuilder<Permission> builder)
	{
		builder.HasKey(p => p.Id);
	}
}
