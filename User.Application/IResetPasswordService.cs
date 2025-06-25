using User.Domain.Requests;

namespace User.Application;

public interface IResetPasswordService
{
    Task ResetPasswordAsync(ResetPasswordRequest request);
}

