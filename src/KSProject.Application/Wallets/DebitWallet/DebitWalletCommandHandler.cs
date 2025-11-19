using KSFramework.Exceptions;
using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Aggregates.Wallets;
using KSProject.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace KSProject.Application.Wallets.DebitWallet;

public sealed class DebitWalletCommandHandler :
    ICommandHandler<DebitWalletCommand, DebitWalletCommandResponse>
{
    private readonly IKSProjectUnitOfWork _uow;

    public DebitWalletCommandHandler(IKSProjectUnitOfWork uow)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
    }

    public async Task<DebitWalletCommandResponse> Handle(DebitWalletCommand request,
        CancellationToken cancellationToken)
    {
        Wallet? wallet = await _uow.Wallets.GetByUserIdAsync(request.Payload.UserId, cancellationToken);

        if (wallet is null)
        {
            throw new KSNotFoundException("Wallet not found for the specified user.");
        }

        var transaction = Transaction.Create(Guid.NewGuid(), wallet.Id, request.Payload.Amount,
            request.Payload.TransactionType, request.Payload.ServiceType, request.Payload.MetricType,
            request.Payload.MetricValue);

        wallet.UpdateBalance(transaction);
        
        _uow.ChangeEntityState(transaction, EntityState.Added);

        await _uow.SaveChangesAsync(cancellationToken);

        return new DebitWalletCommandResponse(wallet.Balance);
    }
}
