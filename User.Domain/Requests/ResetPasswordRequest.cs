namespace User.Domain.Requests;

public class ResetPasswordRequest
{
    public string Username { get; set; } = default!;
    public string NewPassword { get; set; } = default!;
}

