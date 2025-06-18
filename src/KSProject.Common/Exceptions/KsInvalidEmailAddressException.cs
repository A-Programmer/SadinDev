using KSFramework.Exceptions;

namespace KSProject.Common.Exceptions;

public sealed class KsInvalidEmailAddressException : KSException
{
    public KsInvalidEmailAddressException(string? message = null)
        : base(message ?? "Invalid email address")
    {
        
    }
}