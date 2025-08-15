using KSFramework.GenericRepository;
using KSFramework.KSMessaging;
using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.TestAggregate.GetAllTestAggregates;

public sealed class GetAllTestAggregatesQueryHandler : CqrsBase, IQueryHandler<GetAllTestAggregatesQuery, List<GetAllTestAggregatesResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public GetAllTestAggregatesQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetAllTestAggregatesResponse>> Handle(GetAllTestAggregatesQuery request, CancellationToken cancellationToken)
    {
        var testAggregates = await _unitOfWork.GetRepository<Domain.Aggregates.Test.TestAggregate>()
            .GetAllAsync();
        
        return testAggregates.Select(x => new GetAllTestAggregatesResponse
            {
                Id = x.Id,
                Title = x.Title
            })
            .ToList();
    }
}