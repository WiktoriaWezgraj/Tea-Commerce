namespace User.Domain.Requests;

public class UpdateAccountRequest
{
    public string? NewUsername { get; set; }
    public string? NewPassword { get; set; }
    public string? NewRole { get; set; } // admin only
}

