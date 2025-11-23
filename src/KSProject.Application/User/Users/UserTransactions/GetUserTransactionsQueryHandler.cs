using KSFramework.KSMessaging.Abstraction;
using KSFramework.Pagination;
using KSProject.Domain.Aggregates.Wallets;
using KSProject.Domain.Contracts;

namespace KSProject.Application.User.Users.UserTransactions;

    public sealed class GetUserTransactionsQueryHandler
        : IQueryHandler<GetUserTransactionsQuery,
            PaginatedList<UserTransactionListItemResponse>>
    {
        private readonly IKSProjectUnitOfWork _uow;

        public GetUserTransactionsQueryHandler(IKSProjectUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<PaginatedList<UserTransactionListItemResponse>> Handle(
            GetUserTransactionsQuery request,
            CancellationToken cancellationToken)
        {
            PaginatedList<Transaction> transactions = _uow.Wallets.GetTransactionsByUserIdAsync(request.UserId,
                request.PageIndex,
                request.PageSize,
                request.Where,
                request.OrderByPropertyName,
                request.Desc);

            return new PaginatedList<UserTransactionListItemResponse>(transactions
                    .Select(u => new UserTransactionListItemResponse(u.Id,
                        u.Amount,
                        u.ServiceType,
                        u.MetricType,
                        u.MetricValue,
                        u.TransactionDateTime,
                        u.MetricDetails,
                        u.TransactionStatus.ToString(),
                        u.WalletId))
                    .ToList(),
                transactions.Count,
                transactions.PageIndex,
                request.PageSize);
        }
    }
