using KSFramework.Exceptions;

namespace KSProject.Common.Exceptions;

public sealed class KsDuplicatedUserException : KSException
{
    public KsDuplicatedUserException(string? message = null)
        : base(message ?? "Invalid email address")
    {
        
    }
}