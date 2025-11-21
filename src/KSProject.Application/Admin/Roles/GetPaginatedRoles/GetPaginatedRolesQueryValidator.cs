using FluentValidation;

namespace KSProject.Application.Admin.Roles.GetPaginatedRoles;

public sealed class GetPaginatedRolesQueryValidator : AbstractValidator<GetPaginatedRolesQuery>
{
    public GetPaginatedRolesQueryValidator()
    {
    }
}
