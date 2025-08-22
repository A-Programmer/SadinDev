using KSFramework.KSDomain.AggregatesHelper;
using KSFramework.Utilities;
using KSProject.Common.Constants.Enums;
using System.Runtime.Serialization;

namespace KSProject.Domain.Aggregates.Users.ValueObjects;

[Serializable]
public class UserToken : ValueObject, ISerializable
{
	public UserToken(
		TokenTypes type,
		string token,
		DateTimeOffset expirationDateTimeOffset)
	{
		Type = type;

		if (string.IsNullOrEmpty(token))
		{
			throw new ArgumentNullException(nameof(token));
		}
		Token = token;
		ExpirationDateTime = expirationDateTimeOffset;
	}

	public TokenTypes Type { get; private init; }
	public string Token { get; private init; }
	public DateTimeOffset ExpirationDateTime { get; private init; }

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Type;
		yield return Token;
		yield return ExpirationDateTime;
	}

	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue(nameof(Type), Type);
		info.AddValue(nameof(Token), Token);
		info.AddValue(nameof(ExpirationDateTime), ExpirationDateTime);
	}

	public override string ToString()
	{
		return $"{Type.ToDisplay()} : {Token} {Environment.NewLine}Expiration DateTime: {ExpirationDateTime}";
	}

	private UserToken()
	{

	}
}
