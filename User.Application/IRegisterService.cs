using User.Domain.Requests;

namespace User.Application;

public interface IRegisterService
{
    Task RegisterAsync(RegisterRequest request);
}

