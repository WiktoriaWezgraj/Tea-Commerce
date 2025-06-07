using Microsoft.AspNetCore.Mvc;
using User.Domain;

namespace User.Application;

public interface ILoginService
{
    string Login(string username, string password);
}