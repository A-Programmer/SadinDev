using KSFramework.KSDomain.AggregatesHelper;
using System.Runtime.Serialization;

namespace KSProject.Domain.Aggregates.Users.ValueObjects;

[Serializable]
public class UserSecurityStamp : ValueObject, ISerializable
{
	public UserSecurityStamp(
		string securityStamp,
		DateTimeOffset createdDate,
		DateTimeOffset expirationDate)
	{
		SecurityStamp = securityStamp;
		CreatedAt = createdDate;
		ExpirationDate = expirationDate;
	}

	public string SecurityStamp { get; private set; }

	public DateTimeOffset CreatedAt { get; private set; }

	public DateTimeOffset ExpirationDate { get; private set; }


	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue(nameof(SecurityStamp), SecurityStamp);
		info.AddValue(nameof(CreatedAt), CreatedAt);
		info.AddValue(nameof(ExpirationDate), ExpirationDate);
	}

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return SecurityStamp;
		yield return CreatedAt;
		yield return ExpirationDate;
	}

	private UserSecurityStamp()
	{
	}
}