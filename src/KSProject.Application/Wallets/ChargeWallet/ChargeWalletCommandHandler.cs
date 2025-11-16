using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Aggregates.Wallets;
using KSProject.Domain.Contracts;
using KSFramework.Exceptions;
using KSProject.Common.Constants.Enums;
using KSProject.Common.Exceptions;
using KSProject.Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KSProject.Application.Wallets.ChargeWallet;

public sealed class ChargeWalletCommandHandler :
    ICommandHandler<ChargeWalletCommand, ChargeWalletCommandResponse>
{
    private readonly IKSProjectUnitOfWork _uow;
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;

    public ChargeWalletCommandHandler(IKSProjectUnitOfWork uow,
        IServiceProvider serviceProvider,
        IConfiguration configuration)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<ChargeWalletCommandResponse> Handle(ChargeWalletCommand request,
        CancellationToken cancellationToken)
    {
        var factory = _serviceProvider.GetRequiredService<IPaymentGatewayFactory>();
        var gateway = factory.GetGateway(request.Payload.PaymentGatewayType); // GatewayType مثل "ZarrinPal" در request اضافه کن

        string callBackUrl = _configuration[$"PaymentGateways:{request.Payload.PaymentGatewayType.ToString()}:callBackUrl"]; // از config بگیر
        var paymentResult = await gateway.ProcessPaymentAsync(request.Payload.Amount, request.UserId, callBackUrl);

        if (!paymentResult.Success)
            throw new KSPaymentFailedException(paymentResult.ErrorMessage);

        // TODO: Move the following codes to the callback url or another CommandHandler which calls by another Endpoint like PaymentsController/Callback
        // Redirect کاربر به paymentResult.RedirectUrl برای پرداخت
        // برای verify, callback endpoint جدا بساز
        
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
