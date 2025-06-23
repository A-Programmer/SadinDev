using KSFramework.Exceptions;
using KSFramework.GenericRepository;
using KSFramework.KSMessaging;
using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Aggregates.Roles;
using KSProject.Domain.Contracts;

namespace KSProject.Application.Roles.CreateRole;

public sealed class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand, CreateRoleResponse>
{
    private readonly IKSProjectUnitOfWork _unitOfWork;
    public CreateRoleCommandHandler(IKSProjectUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<CreateRoleResponse> Handle(CreateRoleCommand request,
        CancellationToken cancellationToken)
    {
        Role? existingRole = await _unitOfWork.Roles.GetByRoleName(request.Payload.Name,
            cancellationToken);

        if (existingRole is not null)
            throw new KSNotFoundException("The role with this name exist, please choose another name.");
        
        Role roleToCreate = Role.Create(Guid.NewGuid(), request.Payload.Name, request.Payload.Description);

        await _unitOfWork.Roles.AddAsync(roleToCreate, cancellationToken);

        await _unitOfWork.SaveChangesAsync();

        return new CreateRoleResponse
        {
            Id = roleToCreate.Id
        };
    }
}