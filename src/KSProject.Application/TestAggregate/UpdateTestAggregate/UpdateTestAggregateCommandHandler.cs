using KSFramework.Exceptions;
using KSFramework.GenericRepository;
using KSFramework.KSMessaging;
using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.TestAggregate.UpdateTestAggregate;

public sealed class UpdateTestAggregateCommandHandler : CqrsBase, ICommandHandler<UpdateTestAggregateCommand, UpdateTestAggregateResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    public UpdateTestAggregateCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateTestAggregateResponse> Handle(UpdateTestAggregateCommand request, CancellationToken cancellationToken)
    {
        Domain.Aggregates.Tes.TestAggregate? testAggregate = await _unitOfWork
            .GetRepository<Domain.Aggregates.Tes.TestAggregate>()
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