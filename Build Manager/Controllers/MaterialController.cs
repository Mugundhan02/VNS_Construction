using BuildManager.DTOs;
using BuildManager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _materialService;
        private readonly IAuditLogService _auditLog;

        public MaterialController(IMaterialService materialService, IAuditLogService auditLog)
        {
            _materialService = materialService;
            _auditLog = auditLog;
        }

        private string? GetIp() => HttpContext.Connection.RemoteIpAddress?.ToString();
        private string GetUser() => User.Identity?.Name ?? "unknown";

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialResponseDto>>> GetAll()
            => Ok(await _materialService.GetAll());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MaterialResponseDto>> GetById(int id)
            => Ok(await _materialService.GetById(id));

        [HttpPost]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<MaterialResponseDto>> Create([FromBody] MaterialRequestDto dto)
        {
            var result = await _materialService.Create(dto);
            await _auditLog.LogAsync(GetUser(), "CREATE", "Material", result.MaterialId.ToString(), $"Logged raw materials batch arrival: {dto.MaterialName}", GetIp());
            return CreatedAtAction(nameof(GetById), new { id = result.MaterialId }, result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<MaterialResponseDto>> Update(int id, [FromBody] MaterialRequestDto dto)
        {
            var result = await _materialService.Update(id, dto);
            await _auditLog.LogAsync(GetUser(), "UPDATE", "Material", id.ToString(), $"Adjusted volume density data metrics for material batch resource ID {id}", GetIp());
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> Delete(int id)
        {
            await _materialService.Delete(id);
            await _auditLog.LogAsync(GetUser(), "DELETE", "Material", id.ToString(), $"Purged volume tracking data profiles for inventory log ID {id}", GetIp());
            return NoContent();
        }
    }
}