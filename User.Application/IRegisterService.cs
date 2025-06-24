using User.Domain;

namespace User.Application;

public interface IRegisterService
{
    Task RegisterAsync(RegisterRequest request);
}

