using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Aggregates.Wallets;
using KSProject.Domain.Contracts;
using KSFramework.Exceptions;
using KSProject.Common.Constants.Enums;
using KSProject.Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;

namespace KSProject.Application.Wallets.ChargeWallet;

public sealed class ChargeWalletCommandHandler :
    ICommandHandler<ChargeWalletCommand, ChargeWalletCommandResponse>
{
    private readonly IKSProjectUnitOfWork _uow;

    public ChargeWalletCommandHandler(IKSProjectUnitOfWork uow)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
    }

    public async Task<ChargeWalletCommandResponse> Handle(ChargeWalletCommand request,
        CancellationToken cancellationToken)
    {
        Wallet? wallet = await _uow.Wallets.GetByUserIdAsync(request.UserId, cancellationToken);

        if (wallet is null)
        {
            throw new KSNotFoundException("Wallet not found for the specified user.");
        }

        Transaction transaction = Transaction.Create(Guid.NewGuid(), wallet.Id, request.Payload.Amount,
            TransactionTypes.Charge, null, null, 0);

        wallet.UpdateBalance(transaction);
        
        _uow.ChangeEntityState(transaction, EntityState.Added);

        await _uow.SaveChangesAsync(cancellationToken);

        return new ChargeWalletCommandResponse(wallet.Balance);
    }
}
