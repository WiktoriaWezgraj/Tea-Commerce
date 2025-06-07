using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace User.Domain.Models;

public class UserModel
{
    public int Id { get; set; } = default(int);
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Role { get; set; } = default!;
}
