using KSFramework.Exceptions;
using KSFramework.GenericRepository;
using KSFramework.KSMessaging;
using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Contracts;

namespace KSProject.Application.TestAggregate.GetTestAggregateById;

public sealed class GetTestAggregateByIdQueryHandler : IQueryHandler<GetTestAggregateByIdQuery, GetTestAggregateByIdResponse>
{
    private readonly IKSProjectUnitOfWork _unitOfWork;
    public GetTestAggregateByIdQueryHandler(IKSProjectUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetTestAggregateByIdResponse> Handle(GetTestAggregateByIdQuery request, CancellationToken cancellationToken)
    {
        Domain.Aggregates.Test.TestAggregate? testAggregate = await _unitOfWork.GetRepository<Domain.Aggregates.Test.TestAggregate>()
            .GetByIdAsync(request.Payload.id);

        if (testAggregate == null)
            throw new KSNotFoundException("The record could not be found");

        return new GetTestAggregateByIdResponse
        {
            Id = testAggregate.Id,
            Title = testAggregate.Title,
            Content = testAggregate.Content
        };
    }
}
