using KSFramework.Exceptions;

namespace KSProject.Common.Exceptions;

public sealed class KSDomainNotEnoughCredit : KSException
{
    public KSDomainNotEnoughCredit(string? message = null)
        : base(message ?? "Not enough credit")
    {

    }
}
