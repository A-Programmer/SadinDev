namespace KSProject.Application.Admin.Roles.GetAllRoles;

public sealed class GetAllRolesResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string Description { get; init; }
}
