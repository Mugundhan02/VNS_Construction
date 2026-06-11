using BuildManager.DTOs;
using BuildManager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IAuditLogService _auditLog;

        public CompanyController(ICompanyService companyService, IAuditLogService auditLog)
        {
            _companyService = companyService;
            _auditLog = auditLog;
        }

        private string GetUsername() => User.Identity?.Name ?? "unknown";
        private string? GetIp() => HttpContext.Connection.RemoteIpAddress?.ToString();

        [HttpGet]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<IEnumerable<CompanyResponseDto>>> GetAll()
        {
            var result = await _companyService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<CompanyResponseDto>> GetById(int id)
        {
            var result = await _companyService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult<CompanyResponseDto>> Create([FromBody] CompanyRequestDto dto)
        {
            var username = GetUsername();
            var result = await _companyService.Create(dto);
            await _auditLog.LogAsync(username, "CREATE", "Company", result.CompanyId.ToString(),
                $"Created corporate entity profile: {dto.CompanyName}", GetIp());
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<CompanyResponseDto>> Update(int id, [FromBody] CompanyRequestDto dto)
        {
            var username = GetUsername();
            var result = await _companyService.Update(id, dto);
            await _auditLog.LogAsync(username, "UPDATE", "Company", id.ToString(),
                $"Modified commercial configurations for company ID {id}", GetIp());
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> Delete(int id)
        {
            var username = GetUsername();
            await _companyService.Delete(id);
            await _auditLog.LogAsync(username, "DELETE", "Company", id.ToString(),
                $"Removed company record track from structural directory ID {id}", GetIp());
            return NoContent();
        }
    }
}