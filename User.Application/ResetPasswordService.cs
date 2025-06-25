using System.Security.Cryptography;
using System.Text;
using User.Domain.Models;
using User.Domain.Repositories;
using User.Domain.Requests;

namespace User.Application;

public class ResetPasswordService : IResetPasswordService
{
    private readonly IUserRepository _userRepository;
    private readonly UserDataContext _context;

    public ResetPasswordService(IUserRepository userRepository, UserDataContext context)
    {
        _userRepository = userRepository;
        _context = context;
    }

    public async Task ResetPasswordAsync(ResetPasswordRequest request)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username);

        if (user == null)
            throw new Exception("User not found");

        user.Password = HashPassword(request.NewPassword);
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

