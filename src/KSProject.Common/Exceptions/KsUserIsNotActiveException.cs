using KSFramework.Exceptions;

namespace KSProject.Common.Exceptions;
public sealed class KsUserIsNotActiveException : KSException
{
	public KsUserIsNotActiveException(string message)
	: base(message ?? "The user is not active, please contact your admi.")
	{

	}
}
