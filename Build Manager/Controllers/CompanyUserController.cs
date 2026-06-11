using BuildManager.DTOs;
using BuildManager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyUserController : ControllerBase
    {
        private readonly ICompanyUserService _companyUserService;
        private readonly IAuditLogService _auditLog;

        public CompanyUserController(ICompanyUserService companyUserService, IAuditLogService auditLog)
        {
            _companyUserService = companyUserService;
            _auditLog = auditLog;
        }

        private string GetUsername() => User.Identity?.Name ?? "unknown";
        private string? GetIp() => HttpContext.Connection.RemoteIpAddress?.ToString();

        [HttpGet]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<IEnumerable<CompanyUserResponseDto>>> GetAll()
        {
            var result = await _companyUserService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<CompanyUserResponseDto>> GetById(int id)
        {
            var result = await _companyUserService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult<CompanyUserResponseDto>> Create([FromBody] CompanyUserRequestDto dto)
        {
            var username = GetUsername();
            var result = await _companyUserService.Create(dto);

            await _auditLog.LogAsync(username, "CREATE", "CompanyUser", result.CompanyUserId.ToString(),
                "Created administrative credential link", GetIp());

            return Ok(result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult<CompanyUserResponseDto>> Update(int id, [FromBody] CompanyUserRequestDto dto)
        {
            var username = GetUsername();
            var result = await _companyUserService.Update(id, dto);

            await _auditLog.LogAsync(username, "UPDATE", "CompanyUser", id.ToString(),
                $"Updated functional authorizations for employee entity ID {id}", GetIp());

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> Delete(int id)
        {
            var username = GetUsername();
            await _companyUserService.Delete(id);

            await _auditLog.LogAsync(username, "DELETE", "CompanyUser", id.ToString(),
                $"Revoked user access clearance track for entry ID {id}", GetIp());

            return NoContent();
        }
    }
}