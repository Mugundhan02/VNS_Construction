using System.Security.Claims;
using BuildManager.DTOs;
using BuildManager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        private string GetUser() => User.Identity?.Name ?? "unknown";

        [HttpPost("register")]
        [ProducesResponseType(typeof(RegisterResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<RegisterResponseDto>> Register([FromBody] RegisterRequestDto dto)
        {
            var result = await _authService.Register(dto);
            await _auditLog.LogAsync(result.UserName, "REGISTER", "CompanyUser", result.CompanyUserId.ToString(), "Registered new user for VNS Construction", GetIp());
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto dto)
        {
            var result = await _authService.Login(dto);
            return Ok(result);
        }

        [HttpPost("change-password")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordRequestDto dto)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim is null || !int.TryParse(userIdClaim, out int userId))
                return Unauthorized(new { message = "Invalid token authorization status." });

            await _authService.ChangePassword(userId, dto);
            await _auditLog.LogAsync(GetUser(), "CHANGE_PASSWORD", "CompanyUser", userId.ToString(), "Updated user security password profiles", GetIp());
            return NoContent();
        }
    }
}