using KSFramework.Exceptions;

namespace KSProject.Common.Exceptions;

public sealed class KSPaymentFailedException : KSException
{
    public KSPaymentFailedException(string message)
        : base(message)
    {
        
    }
}
