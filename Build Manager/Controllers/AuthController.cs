using BuildManager.DTOs;
using BuildManager.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BuildManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IAuditLogService _auditLog;

        public AuthController(IAuthService authService, IAuditLogService auditLog)
        {
            _authService = authService;
            _auditLog = auditLog;
        }

        private string? GetIp() => HttpContext.Connection.RemoteIpAddress?.ToString();

        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponseDto>> Register([FromBody] RegisterRequestDto request)
        {
            var result = await _authService.Register(request);

            await _auditLog.LogAsync(request.UserName, "REGISTER", "CompanyUser", result.CompanyUserId.ToString(),
                "New user registered for VNS Construction", GetIp());

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto request)
        {
            var result = await _authService.Login(request);

            await _auditLog.LogAsync(request.UserName, "LOGIN", "CompanyUser", null,
                "User logged in", GetIp());

            return Ok(result);
        }

        [HttpPut("forgot-password")]
        public async Task<ActionResult<string>> ForgotPassword([FromBody] ForgotPasswordDto request)
        {
            var result = await _authService.ForgotPassword(request);

            await _auditLog.LogAsync(request.EmailId, "PASSWORD_RESET", "CompanyUser", null,
                "Password reset requested via email validation match", GetIp());

            return Ok(new { message = result });
        }
    }
}