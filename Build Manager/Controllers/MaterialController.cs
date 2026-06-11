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

        private string GetUsername() => User.Identity?.Name ?? "unknown";
        private string? GetIp() => HttpContext.Connection.RemoteIpAddress?.ToString();

        [HttpGet]
        [Authorize(Roles = "User,Admin,Owner")]
        public async Task<ActionResult<IEnumerable<MaterialResponseDto>>> GetAll()
        {
            var result = await _materialService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "User,Admin,Owner")]
        public async Task<ActionResult<MaterialResponseDto>> GetById(int id)
        {
            var result = await _materialService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<MaterialResponseDto>> Create([FromBody] MaterialRequestDto dto)
        {
            var username = GetUsername();
            var result = await _materialService.Create(dto);
            await _auditLog.LogAsync(username, "CREATE", "Material", result.MaterialId.ToString(),
                "Logged raw materials batch arrival", GetIp());
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<MaterialResponseDto>> Update(int id, [FromBody] MaterialRequestDto dto)
        {
            var username = GetUsername();
            var result = await _materialService.Update(id, dto);
            await _auditLog.LogAsync(username, "UPDATE", "Material", id.ToString(),
                $"Adjusted volume density data metrics for material batch resource ID {id}", GetIp());
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> Delete(int id)
        {
            var username = GetUsername();
            await _materialService.Delete(id);
            await _auditLog.LogAsync(username, "DELETE", "Material", id.ToString(),
                $"Purged volume tracking data profiles for inventory log ID {id}", GetIp());
            return NoContent();
        }
    }
}