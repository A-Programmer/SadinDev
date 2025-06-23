using KSFramework.Exceptions;
using KSFramework.GenericRepository;
using KSFramework.KSMessaging.Abstraction;
using KSProject.Common.Exceptions;
using KSProject.Domain.Aggregates.Roles;
using KSProject.Domain.Contracts;

namespace KSProject.Application.Roles.DeleteRole;

public sealed class DeleteRoleCommandHandler : ICommandHandler<DeleteRoleCommand, DeleteRoleResponse>
{
    private readonly IKSProjectUnitOfWork _unitOfWork;
    public DeleteRoleCommandHandler(IKSProjectUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<DeleteRoleResponse> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        Role? role = await _unitOfWork
            .Roles
            .GetRoleByIdIncludingUsersAsync(request.Payload.id,
                cancellationToken);

        if (role is null)
            throw new KSNotFoundException("The record could not be fount");

        if (role.Users.Count > 0)
            throw new KsParentHasChildrenException("This role is assigned to some users and you can not delete it, please remove users and then try again.");
        
        _unitOfWork.Roles.Remove(role);

        await _unitOfWork.SaveChangesAsync();

        return new DeleteRoleResponse
        {
            Id = request.Payload.id
        };
    }
}