using KSFramework.GenericRepository;
using KSFramework.KSMessaging;
using KSFramework.KSMessaging.Abstraction;
using KSFramework.Pagination;
using KSProject.Domain.Contracts;

namespace KSProject.Application.TestAggregate.GetPagedTestAggregates;

public sealed class GetPagedTestAggregateQueryHandler : IQueryHandler<GetPagedTestAggregateQuery, PaginatedList<GetPagedTestAggregateResponse>>
{
    private readonly IKSProjectUnitOfWork _unitOfWork;
    public GetPagedTestAggregateQueryHandler(IKSProjectUnitOfWork unitOfWork)
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
