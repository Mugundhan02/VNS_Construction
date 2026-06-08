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
        private readonly IJobWorkService  _jobWorkService;
        private readonly IAuditLogService _auditLog;

        public JobWorkController(IJobWorkService jobWorkService, IAuditLogService auditLog)
        {
            _jobWorkService = jobWorkService;
            _auditLog       = auditLog;
        }

        private string? GetIp()   => HttpContext.Connection.RemoteIpAddress?.ToString();
        private string  GetUser() => User.Identity?.Name ?? "unknown";

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobWorkResponseDto>>> GetAll()
            => Ok(await _jobWorkService.GetAll());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<JobWorkResponseDto>> GetById(int id)
            => Ok(await _jobWorkService.GetById(id));

        [HttpPost]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<JobWorkResponseDto>> Create([FromBody] JobWorkRequestDto dto)
        {
            var result = await _jobWorkService.Create(dto);
            await _auditLog.LogAsync(GetUser(), "CREATE", "JobWork", result.JobWorkId.ToString(), "JobWork created", GetIp());
            return CreatedAtAction(nameof(GetById), new { id = result.JobWorkId }, result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<JobWorkResponseDto>> Update(int id, [FromBody] JobWorkRequestDto dto)
        {
            var result = await _jobWorkService.Update(id, dto);
            await _auditLog.LogAsync(GetUser(), "UPDATE", "JobWork", id.ToString(), "JobWork updated", GetIp());
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> Delete(int id)
        {
            await _jobWorkService.Delete(id);
            await _auditLog.LogAsync(GetUser(), "DELETE", "JobWork", id.ToString(), "JobWork deleted", GetIp());
            return NoContent();
        }
    }
}
