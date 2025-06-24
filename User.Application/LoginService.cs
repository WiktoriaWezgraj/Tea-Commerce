using User.Domain.Repositories;
using User.Domain.Exceptions;
using System.Security.Cryptography;
using System.Text;

namespace User.Application;

public class LoginService : ILoginService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginService(IUserRepository userRepository, IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
    }

    public string Login(string username, string password)
    {
        var user = _userRepository.GetByUsernameAsync(username).Result;

        if (user == null || user.Password != HashPassword(password))
        {
            throw new InvalidCredentialsException();
        }

        var roles = new List<string> { user.Role };

        return _jwtTokenService.GenerateToken(user.Id, roles);
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
