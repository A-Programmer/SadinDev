using KSFramework.GenericRepository;
using KSProject.Domain.Aggregates.Roles;
using KSProject.Domain.Aggregates.Users;

namespace KSProject.Domain.Contracts;

public interface IKSProjectUnitOfWork : IUnitOfWork
{
	public IRolesRepository Roles { get; }
	public IUsersRepository Users { get; }
}