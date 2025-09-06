using KSFramework.Exceptions;
using KSFramework.KSDomain.AggregatesHelper;
using KSFramework.Utilities;
using System.Runtime.Serialization;

namespace KSProject.Domain.Aggregates.Users.ValueObjects;

[Serializable]
public sealed class UserPermission : ValueObject, ISerializable
{
	public UserPermission(
		string name)
	{
		if (!name.HasValue())
			throw new KSArgumentNullException(nameof(name));
		Name = name;
	}
	public Guid UserId { get; private set; }
	public string Name { get; private set; } = string.Empty;


	protected UserPermission()
	{

	}

	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue(nameof(UserId), UserId);
		info.AddValue(nameof(Name), Name);
	}

	protected override IEnumerable<object> GetEqualityComponents()
	{
		throw new NotImplementedException();
	}
}