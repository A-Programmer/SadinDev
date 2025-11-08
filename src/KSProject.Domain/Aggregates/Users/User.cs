using System.Runtime.Serialization;
using KSFramework.KSDomain;
using KSFramework.KSDomain.AggregatesHelper;
using KSFramework.Utilities;
using KSProject.Common.Constants.Enums;
using KSProject.Common.Exceptions;
using KSProject.Domain.Aggregates.Roles;
using KSProject.Domain.Aggregates.Users.Events;
using KSProject.Domain.Aggregates.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KSProject.Domain.Aggregates.Users;

public sealed class User : BaseEntity, IAggregateRoot, ISerializable, ISoftDeletable
{
    private User(Guid id,
        string userName,
        string hashedPassword,
        string email,
        string phoneNumber,
        bool active = true,
        bool superAdmin = false) : base(id)
    {
        if (!userName.HasValue())
            throw new ArgumentNullException(nameof(userName));
        UserName = userName;

        if (!hashedPassword.HasValue())
            throw new ArgumentNullException(nameof(hashedPassword));
        HashedPassword = hashedPassword;

        if (!email.HasValue())
            throw new ArgumentNullException(nameof(email));
        if (!email.IsValidEmail())
            throw new KsInvalidEmailAddressException();
        Email = email;

        if (!phoneNumber.HasValue())
            throw new ArgumentNullException(nameof(phoneNumber));
        if (!phoneNumber.IsValidMobile())
            throw new KsInvalidPhoneNumberException();

        PhoneNumber = phoneNumber;

        SuperAdmin = superAdmin;
        Active = active;
    }
    public string UserName { get; private set; }
    public string HashedPassword { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public bool SuperAdmin { get; private set; }
    public bool Active { get; private set; }

    public bool IsDeleted { get; set; }
    public DateTime? DeletedOnUtc { get; set; }

    public Guid? UserProfileId { get; private set; }
    public UserProfile? Profile { get; private set; }

    private List<UserPermission> _permissions = new();
    public IReadOnlyCollection<UserPermission> Permissions => _permissions;


    private List<Role> _roles = new();
    public IReadOnlyCollection<Role> Roles => _roles; private List<UserToken> _userTokens = new();
    public IReadOnlyCollection<UserToken> UserTokens => _userTokens;

    private List<UserLoginDate> _loginDates = new();
    public IReadOnlyCollection<UserLoginDate> UserLoginDates => _loginDates;

    private List<UserSecurityStamp> _securityStamps = new();
    public IReadOnlyCollection<UserSecurityStamp> UserSecurityStamps => _securityStamps;


    public void Update(string userName,
        string email,
        string phoneNumber,
        bool active,
        UserSecurityStamp? securityStamp = null)
    {
        if (userName.HasValue())
            UserName = userName;

        if (email.HasValue())
            Email = email;

        if (phoneNumber.HasValue())
            PhoneNumber = phoneNumber;

        if (securityStamp != null)
        {
            AddSecurityStamp(securityStamp);
        }
        Active = active;

        AddDomainEvent(new UserUpdatedDomainEvent
        {
            Id = Id,
            Email = email
        });
    }
    
    public void UpdateUsername(string userName,
        UserSecurityStamp? securityStamp = null)
    {
        if (!userName.HasValue())
            throw new ArgumentNullException(nameof(userName));
        UserName = userName;
        
        ClearTokens();
        
        if (securityStamp != null)
        {
            AddSecurityStamp(securityStamp);
        }
    }

    public void UpdatePassword(string hashedPassword,
        UserSecurityStamp? securityStamp = null)
    {
        if (!hashedPassword.HasValue())
            throw new ArgumentNullException(nameof(hashedPassword));
        HashedPassword = hashedPassword;

        ClearTokens();

        if (securityStamp != null)
        {
            AddSecurityStamp(securityStamp);
        }
    }

    public void ChangeSuperAdminMode(UserSecurityStamp? securityStamp = null)
    {
        SuperAdmin = !SuperAdmin;

        ClearTokens();

        if (securityStamp != null && securityStamp is not null)
        {
            AddSecurityStamp(securityStamp);
        }
    }

    public void ChangeActiveMode(UserSecurityStamp? securityStamp = null)
    {
        Active = !Active;

        ClearTokens();

        if (securityStamp is not null && securityStamp != null)
        {
            AddSecurityStamp(securityStamp);
        }
    }

    public bool IsActive()
    {
        return Active;
    }

    public bool IsSuperAdmin()
    {
        return SuperAdmin;
    }

    public void AssignRoles(IEnumerable<Role> roles,
        UserSecurityStamp? securityStamp = null)
    {
        foreach (Role role in roles)
        {
            if (!_roles.Contains(role))
                _roles.Add(role);
        }

        ClearTokens();

        if (securityStamp is not null)
        {
            AddSecurityStamp(securityStamp);
        }
    }

    public void UnAssignRoles(IEnumerable<Role> roles,
        UserSecurityStamp? securityStamp = null)
    {
        if (roles?.Count() > 0)
            _roles.RemoveAll(role => roles.Contains(role));

        ClearTokens();

        if (securityStamp != null)
        {
            AddSecurityStamp(securityStamp);
        }
    }

    public void ClearRoles(UserSecurityStamp? securityStamp = null)
    {
        _roles.Clear();

        ClearTokens();

        if (securityStamp != null)
        {
            AddSecurityStamp(securityStamp);
        }
    }

    public static User Create(Guid id,
        string userName,
        string hashedPassword,
        string email,
        string phoneNumber,
        bool active = true,
        bool superAdmin = false)
    {
        User user = new(id, userName, hashedPassword, email, phoneNumber, active, superAdmin);

        user.AddDomainEvent(new UserCreatedDomainEvent
        {
            Id = id,
            UserName = userName,
            Email = email,
            OccurredOn = DateTime.UtcNow
        });

        return user;
    }

    public static User Register(Guid id,
        string userName,
        string hashedPassword,
        string email,
        string phoneNumber,
        bool active = true)
    {
        User user = new(id, userName, hashedPassword, email, phoneNumber, active, false);

        user.AddDomainEvent(new UserRegisteredDomainEvent
        {
            Id = id,
            UserName = userName,
            Email = email,
            OccurredOn = DateTime.UtcNow
        });

        return user;
    }

    public static User OtpRegistration(string mobileOrEmail)
    {
        if (!mobileOrEmail.HasValue())
        {
            throw new ArgumentNullException(nameof(mobileOrEmail));
        }
        if (mobileOrEmail.IsValidEmail())
        {
            return new User(Guid.NewGuid(),
                mobileOrEmail,
                string.Empty,
                mobileOrEmail,
                string.Empty,
                false,
                false);
        }
        else if (mobileOrEmail.IsValidMobile())
        {
            return new User(Guid.NewGuid(),
                mobileOrEmail,
                string.Empty,
                string.Empty,
                mobileOrEmail,
                false,
                false);
        }
        else
        {
            throw new KsInvalidMobileOrEmailException();
        }
    }

    #region Permissions
    public void AddPermission(UserPermission permission,
        UserSecurityStamp? securityStamp = null)
    {
        if (!_permissions.Any(x => x.Name.ToLower() == permission.Name.ToLower()))
        {
            _permissions.Add(permission);
            if (securityStamp != null)
            {
                AddSecurityStamp(securityStamp);
            }
        }
    }
    public void AddPermissions(List<UserPermission> permissions,
        UserSecurityStamp? securityStamp = null)
    {
        ClearPermissions();
        foreach (var permission in permissions)
        {
            if (!_permissions.Any(x => x.Name.ToLower() == permission.Name.ToLower()))
            {
                _permissions.Add(permission);
            }
        }
    }
    public void AddPermission(string permission,
        UserSecurityStamp? securityStamp = null)
    {
        UserPermission userPermission = new(permission);
        if (!_permissions.Any(x => x.Name.ToLower() == permission.ToLower()))
        {
            _permissions.Add(userPermission);
            if (securityStamp != null)
            {
                AddSecurityStamp(securityStamp);
            }
        }
    }

    public void RemovePermission(UserPermission permission,
        UserSecurityStamp? securityStamp = null)
    {
        if (_permissions.Any(x => x.Name.ToLower() == permission.Name.ToLower()))
        {
            _permissions.Remove(permission);
            if (securityStamp != null)
            {
                AddSecurityStamp(securityStamp);
            }
        }
    }

    public void RemovePermission(string permission,
        UserSecurityStamp? securityStamp = null)
    {
        UserPermission? userPermission = _permissions.FirstOrDefault(x => x.Name.ToLower() == permission.ToLower());
        if (userPermission != null)
        {
            _permissions.Remove(userPermission);
            if (securityStamp != null)
            {
                AddSecurityStamp(securityStamp);
            }
        }
    }

    public void ClearPermissions(UserSecurityStamp? securityStamp = null)
    {
        _permissions.Clear();
        if (securityStamp != null)
        {
            AddSecurityStamp(securityStamp);
        }
    }

    public void UpdatePermissions(List<UserPermission> permissions,
        UserSecurityStamp? securityStamp = null)
    {
        _permissions.Clear();
        _permissions.AddRange(permissions);

        if (securityStamp != null)
        {
            AddSecurityStamp(securityStamp);
        }
    }

    public void UpdatePermissions(Guid userId, List<string> permissions,
        UserSecurityStamp? securityStamp = null)
    {
        _permissions.Clear();
        foreach (var permission in permissions)
        {
            _permissions.Add(new UserPermission(permission));
        }

        if (securityStamp != null)
        {
            AddSecurityStamp(securityStamp);
        }
    }
    #endregion

    #region Secrity Stamps and Tokens

    public void AddSecurityStamp(UserSecurityStamp securityStamp)
    {
        _securityStamps.Add(securityStamp);
    }

    public void RemoveSecurityStamp(UserSecurityStamp securityStamp)
    {
        _securityStamps.Remove(securityStamp);
    }
    public void ClearSecurityStamps()
    {
        _securityStamps.Clear();
    }
    public void RemoveToken(UserToken token)
    {
        _userTokens.Remove(token);
    }

    public void AddToken(UserToken token)
    {
        _userTokens.Add(token);
    }
    public void ClearTokens()
    {
        _userTokens.Clear();
    }

    public void ClearTokensByType(TokenTypes tokenType)
    {
        var tokensToRemove = _userTokens.Where(t => t.Type == tokenType).ToList();
        foreach (var token in tokensToRemove)
        {
            _userTokens.Remove(token);
        }
    }
    #endregion


    #region Profile
    public void AddProfile(UserProfile profile,
        UserSecurityStamp? securityStamp = null)
    {
        Profile = profile;
        UserProfileId = profile.Id;

        if (securityStamp != null)
        {
            AddSecurityStamp(securityStamp);
        }
    }

    public void UpdateProfile(UserProfile profile,
        UserSecurityStamp? securityStamp = null)
    {
        ArgumentNullException.ThrowIfNull(profile);

        Profile?.Update(profile.FirstName,
            profile.LastName,
            profile.ProfileImageUrl,
            profile.AboutMe,
            profile.BirthDate);

        Profile?.SetUserId(profile.UserId);

        if (securityStamp != null)
        {
            AddSecurityStamp(securityStamp);
        }
    }
    #endregion

    #region LoginDates

    public void AddLoginDate(UserLoginDate loginDate)
    {
        _loginDates.Add(loginDate);
    }
    public void ClearLoginDates()
    {
        _loginDates.Clear();
    }

    #endregion

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(Id), Id);
        info.AddValue(nameof(UserName), UserName);
        info.AddValue(nameof(HashedPassword), HashedPassword);
        info.AddValue(nameof(Email), Email);
        info.AddValue(nameof(PhoneNumber), PhoneNumber);
        info.AddValue(nameof(SuperAdmin), SuperAdmin);
        info.AddValue(nameof(Active), Active);
        info.AddValue(nameof(UserProfileId), UserProfileId);
        info.AddValue(nameof(Profile), Profile);
        info.AddValue(nameof(Permissions), Permissions);
        info.AddValue(nameof(UserTokens), UserTokens);
        info.AddValue(nameof(UserSecurityStamps), UserSecurityStamps);
        info.AddValue(nameof(UserLoginDates), UserLoginDates);
    }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.IsDeleted).HasDefaultValue(false);

        builder.Property(x => x.Active)
            .HasDefaultValue(true);

        builder.Property(x => x.SuperAdmin)
            .HasDefaultValue(false);

        builder.HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity("UsersRoles");

        builder.OwnsMany(u => u.Permissions, c =>
        {
            c.ToTable("UserPermissions");
            c.WithOwner()
            .HasForeignKey(x => x.UserId);
            c.Property<Guid>("Id");
            c.HasKey("Id");
        });

        builder.HasOne(u => u.Profile)
            .WithOne(up => up.User)
            .HasForeignKey<UserProfile>(up => up.UserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsMany(u => u.UserTokens, c =>
        {
            c.ToTable("UserTokens");
            c.WithOwner()
                .HasForeignKey("UserId");
            c.Property<Guid>("Id");
            c.HasKey("Id");
        });

        builder.OwnsMany(u => u.UserSecurityStamps, c =>
        {
            c.ToTable("UserSecurityStamps");
            c.WithOwner()
                .HasForeignKey("UserId");
            c.Property<Guid>("Id");
            c.HasKey("Id");
        });

        builder.OwnsMany(u => u.UserLoginDates, c =>
        {
            c.ToTable("UserLoginDates");
            c.WithOwner()
                .HasForeignKey("UserId");
            c.Property<Guid>("Id");
            c.HasKey("Id");
        });

    }
}
