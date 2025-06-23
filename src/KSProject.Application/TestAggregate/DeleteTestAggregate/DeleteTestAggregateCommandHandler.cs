using KSFramework.Exceptions;
using KSFramework.GenericRepository;
using KSFramework.KSMessaging;
using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.TestAggregate.DeleteTestAggregate;

public sealed class DeleteTestAggregateCommandHandler(IUnitOfWork unitOfWork) : CqrsBase(unitOfWork),
    ICommandHandler<DeleteTestAggregateCommand, DeleteTestAggregateResponse>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<DeleteTestAggregateResponse> Handle(DeleteTestAggregateCommand request, CancellationToken cancellationToken)
    {
        Domain.Aggregates.Tes.TestAggregate existingTestAggregate = await _unitOfWork
            .GetRepository<Domain.Aggregates.Tes.TestAggregate>()
            .GetByIdAsync(request.Payload.id, cancellationToken) ?? throw new KSNotFoundException("The TestAggregate could not be found.");

        _unitOfWork.GetRepository<Domain.Aggregates.Tes.TestAggregate>()
            .Remove(existingTestAggregate);
        
        await _unitOfWork.SaveChangesAsync();

        return new DeleteTestAggregateResponse
        {
            Id = request.Payload.id
        };
    }
}