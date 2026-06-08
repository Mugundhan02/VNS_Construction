using BuildManager.DTOs;
using BuildManager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Owner,Admin")]
    public class SubContractorController : ControllerBase
    {
        private readonly ISubContractorService _subContractorService;
        private readonly IAuditLogService      _auditLog;

        public SubContractorController(ISubContractorService subContractorService, IAuditLogService auditLog)
        {
            _subContractorService = subContractorService;
            _auditLog             = auditLog;
        }

        private string? GetIp()   => HttpContext.Connection.RemoteIpAddress?.ToString();
        private string  GetUser() => User.Identity?.Name ?? "unknown";

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubContractorResponseDto>>> GetAll()
            => Ok(await _subContractorService.GetAll());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SubContractorResponseDto>> GetById(int id)
            => Ok(await _subContractorService.GetById(id));

        [HttpPost]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<SubContractorResponseDto>> Create([FromBody] SubContractorRequestDto dto)
        {
            var result = await _subContractorService.Create(dto);
            await _auditLog.LogAsync(GetUser(), "CREATE", "SubContractor", result.SubContractorId.ToString(), "SubContractor created", GetIp());
            return CreatedAtAction(nameof(GetById), new { id = result.SubContractorId }, result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<SubContractorResponseDto>> Update(int id, [FromBody] SubContractorRequestDto dto)
        {
            var result = await _subContractorService.Update(id, dto);
            await _auditLog.LogAsync(GetUser(), "UPDATE", "SubContractor", id.ToString(), "SubContractor updated", GetIp());
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> Delete(int id)
        {
            await _subContractorService.Delete(id);
            await _auditLog.LogAsync(GetUser(), "DELETE", "SubContractor", id.ToString(), "SubContractor deleted", GetIp());
            return NoContent();
        }
    }
}
