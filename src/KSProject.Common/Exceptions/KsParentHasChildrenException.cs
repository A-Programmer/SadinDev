using KSFramework.Exceptions;

namespace KSProject.Common.Exceptions;

public sealed class KsParentHasChildrenException : KSException
{
    public KsParentHasChildrenException(string? message = null)
        : base(message ?? "The record has children and you should remove its children first.")
    {
        
    }
}