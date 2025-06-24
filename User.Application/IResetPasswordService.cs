using User.Domain;

namespace User.Application;

public interface IResetPasswordService
{
    Task ResetPasswordAsync(ResetPasswordRequest request);
}

