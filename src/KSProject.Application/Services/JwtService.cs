using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KSProject.Domain;
using KSProject.Domain.Aggregates.Users;
using KSProject.Domain.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace KSProject.Application.Services;

public sealed class JwtService : IJwtService
{
    private readonly PublicSettings _settings;

    public JwtService(IOptionsSnapshot<PublicSettings> settings)
    {
        _settings = settings.Value ??
                    throw new ArgumentNullException(nameof(settings));
    }

    public string GenerateToken(Domain.Aggregates.Users.User user, List<string> permissions)
    {
        var secretKey = Encoding.UTF8.GetBytes(_settings.JwtOptions.SecretKey);

        var signInCredentials =
            new SigningCredentials(new SymmetricSecurityKey(secretKey),
                                    SecurityAlgorithms.HmacSha256Signature);

        var claims = _getClaims(user, permissions);

        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = _settings.JwtOptions.Issuer,
            Audience = _settings.JwtOptions.Audience,
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow.AddMinutes(_settings.JwtOptions.NotBeforeInMinutes),
            Expires = DateTime.UtcNow.AddMinutes(_settings.JwtOptions.ExpirationInMinutes),
            SigningCredentials = signInCredentials,
            Subject = new ClaimsIdentity(claims),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateJwtSecurityToken(descriptor);

        return tokenHandler.WriteToken(token);
    }

    private IEnumerable<Claim> _getClaims(Domain.Aggregates.Users.User user, List<string> permissions)
    {
        string securityStamp = user.UserSecurityStamps?
            .FirstOrDefault(x => x.ExpirationDate > DateTime.UtcNow)?.SecurityStamp ?? "";
        
        string isInternal = "false";
        if (user.ApiKeys != null && user.ApiKeys.Any() && user.ApiKeys.FirstOrDefault(x => x.InternalStatus()) != null && user.ApiKeys.FirstOrDefault(x => x.InternalStatus()).IsValid())
        {
            isInternal = "true";
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("security_stamp", securityStamp),
            new Claim("is_SuperAdmin", user.IsSuperAdmin().ToString()),
            new Claim("is_Internal", isInternal ?? "false")
        };

        foreach (var role in user.Roles)
        {
            claims.Add(new Claim("role", role.Name));
        }

        foreach (var permission in permissions)
        {
            claims.Add(new Claim("permission", permission));
        }

        return claims;
    }

}
