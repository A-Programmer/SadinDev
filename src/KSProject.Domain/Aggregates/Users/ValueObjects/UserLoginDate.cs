using KSFramework.KSDomain.AggregatesHelper;
using System.Runtime.Serialization;

namespace KSProject.Domain.Aggregates.Users.ValueObjects;

[Serializable]
public sealed class UserLoginDate : ValueObject, ISerializable
{
	public UserLoginDate(
        DateTime loginDate,
		string ipAddress = "")
	{
		LoginDate = loginDate;
		IpAddress = ipAddress;
	}

	public DateTime LoginDate { get; private init; }
	public string IpAddress { get; private init; }
	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return LoginDate;
		yield return IpAddress;
	}

	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue(nameof(LoginDate), LoginDate);
		info.AddValue(nameof(IpAddress), IpAddress);
	}
}
