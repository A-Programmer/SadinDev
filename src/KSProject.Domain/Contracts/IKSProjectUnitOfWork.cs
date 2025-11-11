using KSFramework.GenericRepository;
using KSProject.Domain.Aggregates.Billings;
using KSProject.Domain.Aggregates.Roles;
using KSProject.Domain.Aggregates.Users;
using KSProject.Domain.Aggregates.Wallets;

namespace KSProject.Domain.Contracts;

public interface IKSProjectUnitOfWork : IUnitOfWork
{
	public IRolesRepository Roles { get; }
	public IUsersRepository Users { get; }
    public IWalletsRepository Wallets { get; }
    public IServiceRatesRepository ServiceRates { get; }
}
