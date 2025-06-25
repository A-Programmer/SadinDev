using FluentValidation;

namespace KSProject.Application.Users.GetPaginatedUsers;

public sealed class GetPaginatedUsersQueryValidator : AbstractValidator<GetPaginatedUsersQuery>
{
    public GetPaginatedUsersQueryValidator()
    {
        
    }
}