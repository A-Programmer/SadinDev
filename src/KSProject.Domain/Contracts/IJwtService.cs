using KSProject.Domain.Aggregates.Users;

namespace KSProject.Domain.Contracts;

public interface IJwtService
{
	string GenerateToken(User user, List<string> permissions);
}