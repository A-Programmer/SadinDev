using KSFramework.Exceptions;

namespace KSProject.Common.Exceptions;

public sealed class KsInvalidPhoneNumberException : KSException
{
    public KsInvalidPhoneNumberException(string? message = null)
        : base(message ?? "Invalid phone number")
    {
        
    }
}