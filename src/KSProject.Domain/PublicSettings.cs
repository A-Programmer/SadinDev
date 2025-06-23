namespace KSProject.Domain;

public class PublicSettings
{
    public PublicOptions PublicOptions { get; set; }
    public JwtOptions JwtOptions { get; set; }
    public CustomIdentityOptions CustomIdentityOptions { get; set; }
}

public class PublicOptions
{
    public bool EmailActivation { get; set; }
    public bool PhoneActivation { get; set; }
    public string BaseUrl { get; set; }
}

public class CustomIdentityOptions
{
    public PasswordOptions PasswordOptions { get; set; }
    public SigninOptions SigninOptions { get; set; }
    public LockoutOptions LockoutOptions { get; set; }
    public UserOptions UserOptions { get; set; }
}

public class JwtOptions
{
    public string SecretKey { get; set; }
    public string SecretKey2 { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int NotBeforeInMinutes { get; set; }
    public int ExpirationInMinutes { get; set; }
    public string BaseUrl { get; set; }
}

public class PasswordOptions
{
    public bool RequireDigit { get; set; }
    public int RequiredLength { get; set; }
    public bool RequireNonAlphanumeric { get; set; }
    public bool RequireUppercase { get; set; }
    public bool RequireLowercase { get; set; }
    public int RequiredUniqueChars { get; set; }
}
public class SigninOptions
{
    public bool RequireConfirmedAccount { get; set; }
    public bool RequireConfirmedPhoneNumber { get; set; }
    public bool RequireConfirmedEmail { get; set; }
}
public class LockoutOptions
{
    public bool AllowedForNewUsers { get; set; }
    public int DefaultLockoutMinutes { get; set; }
    public int MaxFailedAccessAttempts { get; set; }
}
public class UserOptions
{
    public bool RequireUniqueEmail { get; set; }
    public string AllowedUserNameCharacters { get; set; }
}