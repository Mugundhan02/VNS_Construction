using BuildManager.DTOs;
using BuildManager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubContractorController : ControllerBase
    {
        private readonly ISubContractorService _subContractorService;
        private readonly IAuditLogService _auditLog;

        public SubContractorController(ISubContractorService subContractorService, IAuditLogService auditLog)
        {
            _subContractorService = subContractorService;
            _auditLog = auditLog;
        }

        private string GetUsername() => User.Identity?.Name ?? "unknown";
        private string? GetIp() => HttpContext.Connection.RemoteIpAddress?.ToString();

        [HttpGet]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<IEnumerable<SubContractorResponseDto>>> GetAll()
        {
            var result = await _subContractorService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<SubContractorResponseDto>> GetById(int id)
        {
            var result = await _subContractorService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<SubContractorResponseDto>> Create([FromBody] SubContractorRequestDto dto)
        {
            var username = GetUsername();
            var result = await _subContractorService.Create(dto);
            await _auditLog.LogAsync(username, "CREATE", "SubContractor", result.SubContractorId.ToString(),
                $"Linked specialized trade firm subcontractor: {dto.SubContractorName}", GetIp());
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<SubContractorResponseDto>> Update(int id, [FromBody] SubContractorRequestDto dto)
        {
            var username = GetUsername();
            var result = await _subContractorService.Update(id, dto);
            await _auditLog.LogAsync(username, "UPDATE", "SubContractor", id.ToString(),
                $"Updated service profile records for subcontractor ID {id}", GetIp());
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> Delete(int id)
        {
            var username = GetUsername();
            await _subContractorService.Delete(id);
            await _auditLog.LogAsync(username, "DELETE", "SubContractor", id.ToString(),
                $"Archived field vendor track for labor partner ID {id}", GetIp());
            return NoContent();
        }
    }
}