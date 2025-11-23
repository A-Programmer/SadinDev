using System.Linq.Expressions;
using KSFramework.KSMessaging.Abstraction;
using KSFramework.Pagination;
using KSFramework.Utilities;
using KSProject.Domain.Aggregates.Wallets;

namespace KSProject.Application.User.Users.UserTransactions;

public record GetUserTransactionsQuery
    : IQuery<PaginatedList<UserTransactionListItemResponse>>
{
    private readonly GetUserTransactionsQueryRequest _payload;
    public GetUserTransactionsQuery(Guid userId,
        GetUserTransactionsQueryRequest payload)
    {
        if(payload.SearchTerm.HasValue())
        {
            Where = x =>
                x.Amount.ToString("######") == payload.SearchTerm ||
                x.Type.ToString().ToLower() == payload.SearchTerm.ToLower() ||
                x.ServiceType.ToString().ToLower() == payload.SearchTerm.ToLower() ||
                x.MetricType.ToString().ToLower() == payload.SearchTerm.ToLower() ||
                x.MetricValue.ToString("######") == payload.SearchTerm ||
                x.MetricDetails.ToString().ToLower() == payload.SearchTerm.ToLower() ||
                x.TransactionStatus.ToString().ToLower() == payload.SearchTerm.ToLower() ||
                x.WalletId.ToString().ToLower() == payload.SearchTerm.ToLower();
        }
        OrderByPropertyName = payload.OrderByPropertyName.HasValue() ?  payload.OrderByPropertyName : "Id";
        Desc = payload.Desc;
        PageIndex = payload.PageIndex;
        PageSize = payload.PageSize;
        UserId = userId;
        _payload = payload;
    }

    public Guid UserId { get; set; }
    public int PageIndex { get; private set; }
    public int PageSize { get; private set; }
    public Expression<Func<Transaction, bool>>? Where { get; private set; }
    public string OrderByPropertyName { get; private set; }
    public bool Desc { get; private set; }
}
