using KSFramework.GenericRepository;
using KSProject.Domain.Aggregates.Billings;
using KSProject.Domain.Aggregates.Roles;
using KSProject.Domain.Aggregates.Users;
using KSProject.Domain.Aggregates.Wallets;
using Microsoft.EntityFrameworkCore;

namespace KSProject.Domain.Contracts;

public interface IKSProjectUnitOfWork
{
    void ChangeEntityState<TEntity>(TEntity entity, EntityState entityState)  where TEntity : class;
	public IRolesRepository Roles { get; }
	public IUsersRepository Users { get; }
    public IWalletsRepository Wallets { get; }
    public IServiceRatesRepository ServiceRates { get; }
    
    new Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    /// <inheritdoc cref="SaveChangesAsync(System.Threading.CancellationToken)"/>
    new Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default);
    
    /// <inheritdoc cref="SaveChanges(bool)"/>
    new int SaveChanges(bool acceptAllChangesOnSuccess);
    
    /// <inheritdoc cref="SaveChanges()"/>
    new int SaveChanges();
    
    /// <summary>
    /// Gets a generic repository for the specified entity type.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <returns>An instance of <see cref="IGenericRepository{TEntity}"/> for the specified entity type.</returns>
    IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
}
