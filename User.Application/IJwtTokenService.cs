using User.Domain;

namespace User.Application;

public interface IJwtTokenService
{
    string GenerateToken(int userId, List<string> roles);
}