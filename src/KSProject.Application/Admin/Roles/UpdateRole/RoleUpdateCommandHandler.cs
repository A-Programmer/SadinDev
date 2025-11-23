using KSFramework.Exceptions;
using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Aggregates.Roles;
using KSProject.Domain.Contracts;

namespace KSProject.Application.Admin.Roles.UpdateRole;

public sealed class RoleUpdateCommandHandler : ICommandHandler<RoleUpdateCommand, RoleUpdateResponse>
{
    private readonly IKSProjectUnitOfWork _unitOfWork;
    public RoleUpdateCommandHandler(IKSProjectUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<RoleUpdateResponse> Handle(RoleUpdateCommand request, CancellationToken cancellationToken)
    {
        Role? existingRole = await _unitOfWork.Roles.GetByIdAsync(
            request.Payload.Id,
            cancellationToken);

        if (existingRole is null)
            throw new KSNotFoundException("The record could not be found.");
        
        existingRole.Update(request.Payload.Name, request.Payload.Description);

        await _unitOfWork.SaveChangesAsync();

        return new RoleUpdateResponse
        {
            Id = request.Payload.Id
        };
    }
}
