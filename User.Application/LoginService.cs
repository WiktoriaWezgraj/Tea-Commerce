using Microsoft.Extensions.Options;
using System.Security.Authentication;
using User.Domain.Exceptions;
using User.Domain.Models;

namespace User.Application;

public class LoginService : ILoginService
{
    private readonly IJwtTokenService _jwtTokenService;

    public LoginService(IJwtTokenService jwtTokenService)
    {
        _jwtTokenService = jwtTokenService;
    }

    public string Login(string username, string password)
    {
        // Tymczasowa weryfikacja użytkownika (na sztywno)
        if (username != "admin" || password != "password")
        {
            throw new InvalidCredentialsException();
        }

        // Przykładowe dane użytkownika
        var userId = 1;
        var roles = new List<string> { "Administrator" };

        return _jwtTokenService.GenerateToken(userId, roles);
    }
}