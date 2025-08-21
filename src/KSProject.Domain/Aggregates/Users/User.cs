using KSFramework.KSDomain;
using KSFramework.KSDomain.AggregatesHelper;
using KSFramework.Utilities;
using KSProject.Common.Exceptions;
using KSProject.Domain.Aggregates.Roles;
using KSProject.Domain.Aggregates.Users.Events;
// using KSProject.Domain.Aggregates.Users.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Runtime.Serialization;

namespace KSProject.Domain.Aggregates.Users;

// TODO: Add tokens table and relation (Take a look at the BaseProjectWithDetails)
public sealed class User : BaseEntity, IAggregateRoot, ISerializable
{
	private User(Guid id,
		string userName,
		string hashedPassword,
		string email,
		string phoneNumber) : base(id)
	{
		if (!userName.HasValue())
			throw new ArgumentNullException(nameof(userName));
		UserName = userName;

		if (!hashedPassword.HasValue())
			throw new ArgumentNullException(nameof(hashedPassword));
		HashedPassword = hashedPassword;

		if (!email.HasValue())
			throw new ArgumentNullException(nameof(email));
		if (!email.IsValidEmail())
			throw new KsInvalidEmailAddressException();
		Email = email;

		if (!phoneNumber.HasValue())
			throw new ArgumentNullException(nameof(phoneNumber));
		if (!phoneNumber.IsValidMobile())
			throw new KsInvalidPhoneNumberException();
		PhoneNumber = phoneNumber;
	}

	public string UserName { get; private set; }
	public string HashedPassword { get; private set; }
	public string Email { get; private set; }
	public string PhoneNumber { get; private set; }

	private List<Role> _roles = new();
	public IReadOnlyCollection<Role> Roles => _roles;


	public void Update(string userName,
		string email,
		string phoneNumber)
	{
		if (userName.HasValue())
			UserName = userName;

		if (email.HasValue())
			Email = email;

		if (!phoneNumber.HasValue())
			PhoneNumber = phoneNumber;

		// AddDomainEvent(new UserUpdatedDomainEvent
		// {
		//     Id = Id,
		//     Email = email
		// });
	}

	public void UpdatePassword(string hashedPassword)
	{
		if (!hashedPassword.HasValue())
			throw new ArgumentNullException(nameof(hashedPassword));
		HashedPassword = hashedPassword;
	}

	public void AssignRoles(IEnumerable<Role> roles)
	{
		foreach (Role role in roles)
		{
			if (!_roles.Contains(role))
				_roles.Add(role);
		}
	}

	public void UnAssignRoles(IEnumerable<Role> roles) =>
		_roles.RemoveAll(role => _roles.Contains(role));

	public void ClearRoles() => _roles.Clear();

	public static User Create(Guid id,
		string userName,
		string hashedPassword,
		string email,
		string phoneNumber)
	{
		User user = new(id, userName, hashedPassword, email, phoneNumber);

		user.AddDomainEvent(new UserCreatedDomainEvent
		{
			Id = id,
			UserName = userName,
			Email = email,
			OccurredOn = DateTime.UtcNow
		});

		var domainEvents = user.DomainEvents;

		return user;
	}

	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue(nameof(Id), Id);
		info.AddValue(nameof(UserName), UserName);
		info.AddValue(nameof(HashedPassword), HashedPassword);
		info.AddValue(nameof(Email), Email);
		info.AddValue(nameof(PhoneNumber), PhoneNumber);
	}
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasKey(u => u.Id);

		builder.HasMany(u => u.Roles)
			.WithMany(r => r.Users);
	}
}