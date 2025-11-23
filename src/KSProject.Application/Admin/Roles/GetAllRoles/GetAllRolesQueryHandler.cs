using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Contracts;

namespace KSProject.Application.Admin.Roles.GetAllRoles;

public sealed class GetAllRolesQueryHandler : IQueryHandler<GetAllRolesQuery, List<GetAllRolesResponse>>
{
    private readonly IKSProjectUnitOfWork _unitOfWork;

    public GetAllRolesQueryHandler(IKSProjectUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<List<GetAllRolesResponse>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _unitOfWork.Roles.GetAllAsync(true, cancellationToken);

        return roles.Select(x => new GetAllRolesResponse()
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description
        }).ToList();
    }
}
