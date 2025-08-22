using KSFramework.Exceptions;

namespace KSProject.Common.Exceptions;

public sealed class KsAuthenticationException : KSException
{
	public KsAuthenticationException(string message)
		: base(message ?? "Authentication Exception")
	{

	}
}
