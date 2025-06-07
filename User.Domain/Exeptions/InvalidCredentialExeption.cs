namespace User.Domain.Exeptions;

public class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException() : base("Incorect password or login") { }
}
