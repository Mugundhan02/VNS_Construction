using BuildManager.DTOs;
using BuildManager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IAuditLogService _auditLog;

        public ClientController(IClientService clientService, IAuditLogService auditLog)
        {
            _clientService = clientService;
            _auditLog = auditLog;
        }

        private string GetUsername() => User.Identity?.Name ?? "unknown";
        private string? GetIp() => HttpContext.Connection.RemoteIpAddress?.ToString();

        [HttpGet]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<IEnumerable<ClientResponseDto>>> GetAll()
        {
            var result = await _clientService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<ClientResponseDto>> GetById(int id)
        {
            var result = await _clientService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<ClientResponseDto>> Create([FromBody] ClientRequestDto dto)
        {
            var username = GetUsername();
            var result = await _clientService.Create(dto);
            await _auditLog.LogAsync(username, "CREATE", "Client", result.ClientId.ToString(),
                $"Registered construction contract client: {dto.ClientName}", GetIp());
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<ClientResponseDto>> Update(int id, [FromBody] ClientRequestDto dto)
        {
            var username = GetUsername();
            var result = await _clientService.Update(id, dto);
            await _auditLog.LogAsync(username, "UPDATE", "Client", id.ToString(),
                $"Updated master records for client ID {id}", GetIp());
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> Delete(int id)
        {
            var username = GetUsername();
            await _clientService.Delete(id);
            await _auditLog.LogAsync(username, "DELETE", "Client", id.ToString(),
                $"Archived contract client registry track ID {id}", GetIp());
            return NoContent();
        }
    }
}