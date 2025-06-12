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
        Domain.Aggregates.Tes.TestAggregate testAggregate = Domain.Aggregates.Tes.TestAggregate.Create(request.Title, request.Content);
        await _unitOfWork.GetRepository<Domain.Aggregates.Tes.TestAggregate>()
            .AddAsync(testAggregate);

        await _unitOfWork.SaveChangesAsync();

        return new CreateTestAggregateResponse(testAggregate.Id);
    }
}