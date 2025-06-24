using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Application;
using User.Domain;
using User.Domain.Exceptions;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                var token = _loginService.Login(request.Username, request.Password);
                return Ok(new { Token = token });
            }
            catch (InvalidCredentialsException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }

        [HttpGet]
        [Authorize]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult AdminPage()
        {
            return Ok("Admin only data");
        }
    }

}