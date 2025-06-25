namespace User.Domain;

public class SmtpSettings
{
    public string Host { get; set; } = default!;
    public int Port { get; set; }
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}
