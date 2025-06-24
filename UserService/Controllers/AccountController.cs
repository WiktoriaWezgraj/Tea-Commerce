using Microsoft.AspNetCore.Mvc;
using User.Application;
using User.Domain;

namespace UserService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IRegisterService _registerService;
    private readonly IResetPasswordService _resetPasswordService;

    public AccountController(IRegisterService registerService, IResetPasswordService resetPasswordService)
    {
        _registerService = registerService;
        _resetPasswordService = resetPasswordService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            await _registerService.RegisterAsync(request);
            return Ok(new { Message = "User registered successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        try
        {
            await _resetPasswordService.ResetPasswordAsync(request);
            return Ok(new { Message = "Password changed successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}

