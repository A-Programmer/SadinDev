using KSFramework.GenericRepository;
using KSProject.Domain.Aggregates.Roles;

namespace KSProject.Domain.Contracts;

public interface IKSProjectUnitOfWork : IUnitOfWork
{
    public IRolesRepository Roles { get; }
}