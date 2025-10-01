using KSFramework.Exceptions;

namespace KSProject.Common.Exceptions;
public class KsInvalidMobileOrEmailException : KSException
{
    public KsInvalidMobileOrEmailException(string? message = null)
        : base(message ?? "Invalid email address or mobile number")
    {
        
    }
}
