using KSFramework.Exceptions;
using KSFramework.GenericRepository;
using KSFramework.KSMessaging;
using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Contracts;

namespace KSProject.Application.TestAggregate.UpdateTestAggregate;

public sealed class UpdateTestAggregateCommandHandler : ICommandHandler<UpdateTestAggregateCommand, UpdateTestAggregateResponse>
{
    private readonly IKSProjectUnitOfWork _unitOfWork;
    public UpdateTestAggregateCommandHandler(IKSProjectUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateTestAggregateResponse> Handle(UpdateTestAggregateCommand request, CancellationToken cancellationToken)
    {
        Domain.Aggregates.Test.TestAggregate? testAggregate = await _unitOfWork
            .GetRepository<Domain.Aggregates.Test.TestAggregate>()
            .GetByIdAsync(request.Payload.Id);

        if (testAggregate == null)
            throw new KSNotFoundException("The record could not be found.");
        
        testAggregate.Update(request.Payload.Title, request.Payload.Content);

        await _unitOfWork.SaveChangesAsync();

        return new UpdateTestAggregateResponse
        {
            Id = request.Payload.Id
        };
    }
}
