using KSFramework.GenericRepository;
using KSFramework.KSMessaging;
using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.TestAggregate.CreateTestAggregate;

public class CreateTestAggregateCommandHandler(IUnitOfWork unitOfWork) : CqrsBase(unitOfWork),
    ICommandHandler<CreateTestAggregateCommand, CreateTestAggregateResponse>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<CreateTestAggregateResponse> Handle(CreateTestAggregateCommand request, CancellationToken cancellationToken)
    {
        Domain.Aggregates.Test.TestAggregate testAggregate = Domain.Aggregates.Test.TestAggregate.Create(request.Payload.Title, request.Payload.Content);
        await _unitOfWork.GetRepository<Domain.Aggregates.Test.TestAggregate>()
            .AddAsync(testAggregate);

        await _unitOfWork.SaveChangesAsync();

        return new CreateTestAggregateResponse
        {
            Id = testAggregate.Id
        };
    }
}