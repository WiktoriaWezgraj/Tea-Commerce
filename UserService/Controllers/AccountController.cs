using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using User.Application;
using User.Domain;

namespace UserService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IRegisterService _registerService;
    private readonly IResetPasswordService _resetPasswordService;
    private readonly IAccountUpdateService _accountUpdateService;

    public AccountController(
        IRegisterService registerService,
        IResetPasswordService resetPasswordService,
        IAccountUpdateService accountUpdateService)
    {
        _registerService = registerService;
        _resetPasswordService = resetPasswordService;
        _accountUpdateService = accountUpdateService;
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

    [HttpPut("update")]
    [Authorize]
    public async Task<IActionResult> UpdateAccount([FromBody] UpdateAccountRequest request)
    {
        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var isAdmin = User.IsInRole("Administrator");

            await _accountUpdateService.UpdateAccountAsync(userId, request, isAdmin);
            return Ok(new { Message = "Account updated successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

}

