using BuildManager.DTOs;
using BuildManager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobWorkController : ControllerBase
    {
        private readonly IJobWorkService _jobWorkService;
        private readonly IAuditLogService _auditLog;

        public JobWorkController(IJobWorkService jobWorkService, IAuditLogService auditLog)
        {
            _jobWorkService = jobWorkService;
            _auditLog = auditLog;
        }

        private string GetUsername() => User.Identity?.Name ?? "unknown";
        private string? GetIp() => HttpContext.Connection.RemoteIpAddress?.ToString();

        [HttpGet]
        [Authorize(Roles = "User,Admin,Owner")]
        public async Task<ActionResult<IEnumerable<JobWorkResponseDto>>> GetAll()
        {
            var result = await _jobWorkService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "User,Admin,Owner")]
        public async Task<ActionResult<JobWorkResponseDto>> GetById(int id)
        {
            var result = await _jobWorkService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<JobWorkResponseDto>> Create([FromBody] JobWorkRequestDto dto)
        {
            var username = GetUsername();
            var result = await _jobWorkService.Create(dto);
            await _auditLog.LogAsync(username, "CREATE", "JobWork", result.JobWorkId.ToString(),
                "Assigned site work schedule task", GetIp());
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<JobWorkResponseDto>> Update(int id, [FromBody] JobWorkRequestDto dto)
        {
            var username = GetUsername();
            var result = await _jobWorkService.Update(id, dto);
            await _auditLog.LogAsync(username, "UPDATE", "JobWork", id.ToString(),
                $"Modified operational milestones for job work task ID {id}", GetIp());
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> Delete(int id)
        {
            var username = GetUsername();
            await _jobWorkService.Delete(id);
            await _auditLog.LogAsync(username, "DELETE", "JobWork", id.ToString(),
                $"Dropped site work order blueprint trace ID {id}", GetIp());
            return NoContent();
        }
    }
}