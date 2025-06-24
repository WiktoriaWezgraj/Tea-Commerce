using System.Security.Cryptography;
using System.Text;
using User.Domain;
using User.Domain.Repositories;

namespace User.Application;

public class AccountUpdateService : IAccountUpdateService
{
    private readonly IUserRepository _userRepository;
    private readonly UserDataContext _context;

    public AccountUpdateService(IUserRepository userRepository, UserDataContext context)
    {
        _userRepository = userRepository;
        _context = context;
    }

    public async Task UpdateAccountAsync(int userId, UpdateAccountRequest request, bool isAdmin)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == userId);
        if (user == null)
            throw new Exception("User not found");

        if (!string.IsNullOrEmpty(request.NewUsername))
            user.Username = request.NewUsername;

        if (!string.IsNullOrEmpty(request.NewPassword))
            user.Password = HashPassword(request.NewPassword);

        if (!string.IsNullOrEmpty(request.NewRole) && isAdmin)
            user.Role = request.NewRole;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}

