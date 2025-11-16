using System.Security.Claims;
using KSProject.Domain.Contracts;
using Microsoft.AspNetCore.Http;

namespace KSProject.Infrastructure.Helpers;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId => Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);
    public bool IsInternal => _httpContextAccessor.HttpContext?.User.Claims.Any(c => c.Type == "is_Internal" && c.Value == "true") ?? false;
}
