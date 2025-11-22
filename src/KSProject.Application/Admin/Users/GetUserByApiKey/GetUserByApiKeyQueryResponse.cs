namespace KSProject.Application.Admin.Users.GetUserByApiKey;

public record GetUserByApiKeyQueryResponse(
    Guid Id,
    string UserName,
    string Email,
    bool IsUserActive,
    bool IsSuperAdmin,
    bool IsInternal,
    string Variant,
    string Scopes,
    bool IsApiKeyActive,
    string Domain
    );
