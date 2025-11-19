using KSProject.Common.Constants.Enums;

namespace KSProject.Domain.Contracts;
public interface ICurrentUserService
{
    Guid UserId { get; }
    bool IsInternal { get; }
    bool IsAuthenticated { get; }
}
