using KSFramework.KSDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KSProject.Domain.Aggregates.Users;

public sealed class UserProfile : BaseEntity
{
	private UserProfile(Guid id,
		string firstName,
		string lastName,
		string profileImageUrl,
		string aboutMe,
		DateTimeOffset? birthDate)
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
		DateTimeOffset? birthDate)
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
	public DateTimeOffset? BirthDate { get; private set; }

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
		string firstName,
		string lastName,
		string profileImageUrl,
		string aboutMe,
		DateTimeOffset? birthDate)
	{
		UserProfile profile = new(id,
			firstName,
			lastName,
			profileImageUrl,
			aboutMe,
			birthDate);

		return profile;
	}


	protected UserProfile()
	{
	}
}



public class UserProfileConfigurations : IEntityTypeConfiguration<UserProfile>
{
	public void Configure(EntityTypeBuilder<UserProfile> builder)
	{
		builder.ToTable("UsersProfile");

		builder.HasKey(x => x.Id);
	}
}
