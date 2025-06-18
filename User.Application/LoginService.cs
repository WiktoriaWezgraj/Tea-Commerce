using Microsoft.Extensions.Options;
using System.Data;
using System;
using System.Security.Authentication;
using User.Domain.Exeptions;
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
            var roles = new List<string> { "Client", "Employee", "Administrator" };
            var token = _jwtTokenService.GenerateToken(123, roles);
            return token;
        }
        else
        {
            throw new InvalidCredentialsException();
        }

    }
}