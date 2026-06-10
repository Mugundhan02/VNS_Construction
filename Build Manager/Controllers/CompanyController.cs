using BuildManager.DTOs;
using BuildManager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Owner,Admin")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IAuditLogService _auditLog;

        public CompanyController(ICompanyService companyService, IAuditLogService auditLog)
        {
            _companyService = companyService;
            _auditLog = auditLog;
        }

        private string? GetIp() => HttpContext.Connection.RemoteIpAddress?.ToString();
        private string GetUser() => User.Identity?.Name ?? "unknown";

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyResponseDto>>> GetAll()
            => Ok(await _companyService.GetAll());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CompanyResponseDto>> GetById(int id)
            => Ok(await _companyService.GetById(id));

        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult<CompanyResponseDto>> Create([FromBody] CompanyRequestDto dto)
        {
            var result = await _companyService.Create(dto);
            await _auditLog.LogAsync(GetUser(), "CREATE", "Company", result.CompanyId.ToString(), $"Created corporate entity profile: {dto.CompanyName}", GetIp());
            return CreatedAtAction(nameof(GetById), new { id = result.CompanyId }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CompanyResponseDto>> Update(int id, [FromBody] CompanyRequestDto dto)
        {
            var result = await _companyService.Update(id, dto);
            await _auditLog.LogAsync(GetUser(), "UPDATE", "Company", id.ToString(), $"Modified commercial configurations for company ID {id}", GetIp());
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> Delete(int id)
        {
            await _companyService.Delete(id);
            await _auditLog.LogAsync(GetUser(), "DELETE", "Company", id.ToString(), $"Removed company record track from structural directory ID {id}", GetIp());
            return NoContent();
        }
    }
}