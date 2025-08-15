using KSFramework.GenericRepository;
using KSFramework.KSMessaging;
using KSFramework.KSMessaging.Abstraction;
using KSFramework.Pagination;

namespace KSProject.Application.TestAggregate.GetPagedTestAggregates;

public sealed class GetPagedTestAggregateQueryHandler : CqrsBase, IQueryHandler<GetPagedTestAggregateQuery, PaginatedList<GetPagedTestAggregateResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetPagedTestAggregateQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedList<GetPagedTestAggregateResponse>> Handle(GetPagedTestAggregateQuery request, CancellationToken cancellationToken)
    {
        PaginatedList<Domain.Aggregates.Test.TestAggregate> res = await _unitOfWork.GetRepository<Domain.Aggregates.Test.TestAggregate>()
            .GetPagedAsync(request.PageIndex,
                request.PageSize,
                request.Where,
                request.OrderByPropertyName,
                request.Desc);

        var result = new PaginatedList<GetPagedTestAggregateResponse>(
            res.Select(x => new GetPagedTestAggregateResponse
            {
                Id = x.Id,
                Title = x.Title
            }).ToList(),
            res.TotalItems,
            res.PageIndex,
            res.TotalPages
        );

        return result;
    }
}