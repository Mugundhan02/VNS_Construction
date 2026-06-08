using System.Security.Claims;
using BuildManager.DTOs;
using BuildManager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService     _authService;
        private readonly IAuditLogService _auditLog;

        public AuthController(IAuthService authService, IAuditLogService auditLog)
        {
            _authService = authService;
            _auditLog    = auditLog;
        }

        private string? GetIp()   => HttpContext.Connection.RemoteIpAddress?.ToString();
        private string  GetUser() => User.Identity?.Name ?? "unknown";

        /// <summary>
        /// Register a new company and its first Owner account.
        /// </summary>
        [HttpPost("register")]
        [ProducesResponseType(typeof(RegisterResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<RegisterResponseDto>> Register([FromBody] RegisterRequestDto dto)
        {
            var result = await _authService.Register(dto);
            await _auditLog.LogAsync(result.UserName, "REGISTER", "CompanyUser", result.CompanyUserId.ToString(), "New registration", GetIp());
            return StatusCode(StatusCodes.Status201Created, result);
        }

        /// <summary>
        /// Login with username and password.
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto dto)
        {
            var result = await _authService.Login(dto);
            return Ok(result);
        }

        /// <summary>
        /// Issue a new JWT using a valid refresh token.
        /// </summary>
        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LoginResponseDto>> RefreshToken([FromBody] RefreshTokenRequestDto dto)
        {
            var result = await _authService.RefreshToken(dto);
            if (result is null)
                return Unauthorized(new { message = "Invalid or expired refresh token." });

            return Ok(result);
        }

        /// <summary>
        /// Logout — revoke the refresh token.
        /// </summary>
        [HttpPost("logout")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Logout([FromBody] RefreshTokenRequestDto dto)
        {
            var revoked = await _authService.RevokeToken(dto.RefreshToken);
            if (!revoked)
                return BadRequest(new { message = "Token not found or already revoked." });

            return NoContent();
        }

        /// <summary>
        /// Change password for the currently authenticated user.
        /// </summary>
        [HttpPost("change-password")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordRequestDto dto)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim is null || !int.TryParse(userIdClaim, out int userId))
                return Unauthorized(new { message = "Invalid token." });

            await _authService.ChangePassword(userId, dto);
            await _auditLog.LogAsync(GetUser(), "CHANGE_PASSWORD", "CompanyUser", userId.ToString(), "Password changed", GetIp());
            return NoContent();
        }
    }
}
