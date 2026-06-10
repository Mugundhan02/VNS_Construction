using BuildManager.DTOs;
using BuildManager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Owner,Admin")]
    public class CompanyUserController : ControllerBase
    {
        private readonly ICompanyUserService _companyUserService;
        private readonly IAuditLogService _auditLog;

        public CompanyUserController(ICompanyUserService companyUserService, IAuditLogService auditLog)
        {
            _companyUserService = companyUserService;
            _auditLog = auditLog;
        }

        private string? GetIp() => HttpContext.Connection.RemoteIpAddress?.ToString();
        private string GetUser() => User.Identity?.Name ?? "unknown";

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyUserResponseDto>>> GetAll()
            => Ok(await _companyUserService.GetAll());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CompanyUserResponseDto>> GetById(int id)
            => Ok(await _companyUserService.GetById(id));

        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult<CompanyUserResponseDto>> Create([FromBody] CompanyUserRequestDto dto)
        {
            var result = await _companyUserService.Create(dto);
            // FIXED: Removed dto.Username to resolve compile error CS1061
            await _auditLog.LogAsync(GetUser(), "CREATE", "CompanyUser", result.CompanyUserId.ToString(), "Created administrative credential link", GetIp());
            return CreatedAtAction(nameof(GetById), new { id = result.CompanyUserId }, result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult<CompanyUserResponseDto>> Update(int id, [FromBody] CompanyUserRequestDto dto)
        {
            var result = await _companyUserService.Update(id, dto);
            await _auditLog.LogAsync(GetUser(), "UPDATE", "CompanyUser", id.ToString(), $"Updated functional authorizations for employee entity ID {id}", GetIp());
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> Delete(int id)
        {
            await _companyUserService.Delete(id);
            await _auditLog.LogAsync(GetUser(), "DELETE", "CompanyUser", id.ToString(), $"Revoked user access clearance track for entry ID {id}", GetIp());
            return NoContent();
        }
    }
}