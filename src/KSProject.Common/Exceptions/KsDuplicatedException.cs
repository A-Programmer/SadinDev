using KSFramework.Exceptions;

namespace KSProject.Common.Exceptions;

public sealed class KsDuplicatedException : KSException
{
    public KsDuplicatedException(string? message = null)
        : base(message ?? "There is same record in database")
    {
        
    }
}