using User.Domain;

namespace User.Application;

public interface IAccountUpdateService
{
    Task UpdateAccountAsync(int userId, UpdateAccountRequest request, bool isAdmin);
}

