using KSFramework.Exceptions;
using KSFramework.GenericRepository;
using KSFramework.KSMessaging;
using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Contracts;

namespace KSProject.Application.TestAggregate.DeleteTestAggregate;

public sealed class DeleteTestAggregateCommandHandler(IKSProjectUnitOfWork unitOfWork) : 
    ICommandHandler<DeleteTestAggregateCommand, DeleteTestAggregateResponse>
{
    private readonly IKSProjectUnitOfWork _unitOfWork = unitOfWork;

    public async Task<DeleteTestAggregateResponse> Handle(DeleteTestAggregateCommand request, CancellationToken cancellationToken)
    {
        Domain.Aggregates.Test.TestAggregate existingTestAggregate = await _unitOfWork
            .GetRepository<Domain.Aggregates.Test.TestAggregate>()
            .GetByIdAsync(request.Payload.id, cancellationToken) ?? throw new KSNotFoundException("The TestAggregate could not be found.");

        _unitOfWork.GetRepository<Domain.Aggregates.Test.TestAggregate>()
            .Remove(existingTestAggregate);
        
        await _unitOfWork.SaveChangesAsync();

        return new DeleteTestAggregateResponse
        {
            Id = request.Payload.id
        };
    }
}
