using BuildManager.DTOs;
using BuildManager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Owner,Admin")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IAuditLogService _auditLog;

        public ClientController(IClientService clientService, IAuditLogService auditLog)
        {
            _clientService = clientService;
            _auditLog = auditLog;
        }

        private string? GetIp() => HttpContext.Connection.RemoteIpAddress?.ToString();
        private string GetUser() => User.Identity?.Name ?? "unknown";

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientResponseDto>>> GetAll()
            => Ok(await _clientService.GetAll());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClientResponseDto>> GetById(int id)
            => Ok(await _clientService.GetById(id));

        [HttpPost]
        public async Task<ActionResult<ClientResponseDto>> Create([FromBody] ClientRequestDto dto)
        {
            var result = await _clientService.Create(dto);
            await _auditLog.LogAsync(GetUser(), "CREATE", "Client", result.ClientId.ToString(), $"Registered construction contract client: {dto.ClientName}", GetIp());
            return CreatedAtAction(nameof(GetById), new { id = result.ClientId }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ClientResponseDto>> Update(int id, [FromBody] ClientRequestDto dto)
        {
            var result = await _clientService.Update(id, dto);
            await _auditLog.LogAsync(GetUser(), "UPDATE", "Client", id.ToString(), $"Updated master records for client ID {id}", GetIp());
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> Delete(int id)
        {
            await _clientService.Delete(id);
            await _auditLog.LogAsync(GetUser(), "DELETE", "Client", id.ToString(), $"Archived contract client registry track ID {id}", GetIp());
            return NoContent();
        }
    }
}