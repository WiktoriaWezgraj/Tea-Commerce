using User.Domain.Models;

namespace User.Domain.Repositories;

public interface IUserRepository
{
    Task<UserModel> GetByUsernameAsync(string username);
}
