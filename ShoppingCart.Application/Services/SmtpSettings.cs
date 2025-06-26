namespace ShoppingCart.Application.Services;

public class SmtpSettings
{
    public string Host { get; set; } = "";
    public int Port { get; set; }
    public string SenderEmail { get; set; } = "";
    public string Password { get; set; } = "";
}
