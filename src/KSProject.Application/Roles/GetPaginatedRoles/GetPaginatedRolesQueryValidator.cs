using FluentValidation;

namespace KSProject.Application.Roles.GetPaginatedRoles;

public sealed class GetPaginatedRolesQueryValidator : AbstractValidator<GetPaginatedRolesQuery>
{
    public GetPaginatedRolesQueryValidator()
    {
    }
}