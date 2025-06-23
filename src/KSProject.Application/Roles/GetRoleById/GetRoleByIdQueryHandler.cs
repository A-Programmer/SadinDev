using KSFramework.Exceptions;
using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Aggregates.Roles;
using KSProject.Domain.Contracts;

namespace KSProject.Application.Roles.GetRoleById;

public sealed class GetRoleByIdQueryHandler : IQueryHandler<GetRoleByIdQuery, RoleItemResponse>
{
    private readonly IKSProjectUnitOfWork _unitOfWork;
    public GetRoleByIdQueryHandler(IKSProjectUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<RoleItemResponse> Handle(GetRoleByIdQuery request,
        CancellationToken cancellationToken)
    {
        Role? role = await _unitOfWork.Roles.GetByIdAsync(
            request.Payload.id,
            cancellationToken);

        if (role is null)
            throw new KSNotFoundException("The record could not be found.");

        return new RoleItemResponse
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description
        };
    }
}