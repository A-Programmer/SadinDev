using KSFramework.GenericRepository;
using KSFramework.KSMessaging;
using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Contracts;

namespace KSProject.Application.TestAggregate.GetAllTestAggregates;

public sealed class GetAllTestAggregatesQueryHandler : IQueryHandler<GetAllTestAggregatesQuery, List<GetAllTestAggregatesResponse>>
{
    private readonly IKSProjectUnitOfWork _unitOfWork;
    
    public GetAllTestAggregatesQueryHandler(IKSProjectUnitOfWork unitOfWork)
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
