namespace KSProject.Domain.Contracts;
public interface ICurrentUserService
{
    Guid UserId { get; }
    bool IsInternal { get; }
}
