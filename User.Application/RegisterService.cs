using User.Domain.Models;
using User.Domain.Repositories;
using System.Security.Cryptography;
using System.Text;
using User.Domain.Requests;

namespace User.Application;

public class RegisterService : IRegisterService
{
    private readonly IUserRepository _userRepository;
    private readonly UserDataContext _context;

    public RegisterService(IUserRepository userRepository, UserDataContext context)
    {
        _userRepository = userRepository;
        _context = context;
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        var existingUser = await _userRepository.GetByUsernameAsync(request.Username);
        if (existingUser != null)
            throw new Exception("User already exists");

        var hashedPassword = HashPassword(request.Password);

        var user = new UserModel
        {
            Username = request.Username,
            Password = hashedPassword,
            Role = request.Role
        };

        _context.Users.Add(user);
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

