using KSFramework.KSDomain;

namespace KSProject.Domain.Aggregates.Users;

public sealed class UserProfile : BaseEntity, ISoftDeletable
{
	private UserProfile(Guid id,
		string firstName,
		string lastName,
		string profileImageUrl,
		string aboutMe,
        DateTime? birthDate)
	{
		Id = id;
		FirstName = firstName;
		LastName = lastName;
		ProfileImageUrl = profileImageUrl;
		AboutMe = aboutMe;
		BirthDate = birthDate;
	}


	public void Update(string firstName,
		string lastName,
		string profileImageUrl,
		string aboutMe,
        DateTime? birthDate)
	{
		FirstName = firstName;
		LastName = lastName;
		ProfileImageUrl = profileImageUrl;
		AboutMe = aboutMe;
		BirthDate = birthDate;

	}

	public void SetUserId(Guid userId)
	{
		UserId = userId;
	}

	public string FirstName { get; private set; }
	public string LastName { get; private set; }
	public string ProfileImageUrl { get; private set; }
	public string AboutMe { get; private set; }
	public DateTime? BirthDate { get; private set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOnUtc { get; set; }

	public string FullName
	{
		get
		{
			var fullName = "";

			if (!string.IsNullOrEmpty(this.FirstName))
				fullName += this.FirstName;

			if (!string.IsNullOrEmpty(this.LastName))
				fullName += " " + this.LastName;

			return fullName;
		}
	}

	public Guid UserId { get; private set; }
	public User User { get; private set; }

	public static UserProfile Create(Guid id,
        Guid userId,
		string firstName,
		string lastName,
		string profileImageUrl,
		string aboutMe,
        DateTime? birthDate)
	{
		UserProfile profile = new(id,
			firstName,
			lastName,
			profileImageUrl,
			aboutMe,
			birthDate)
        {
            UserId = userId
        };

		return profile;
	}


	protected UserProfile()
	{
	}
}
