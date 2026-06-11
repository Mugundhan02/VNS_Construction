using BuildManager.DTOs;
using BuildManager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        private readonly IAuditLogService _auditLog;

        public SupplierController(ISupplierService supplierService, IAuditLogService auditLog)
        {
            _supplierService = supplierService;
            _auditLog = auditLog;
        }

        private string GetUsername() => User.Identity?.Name ?? "unknown";
        private string? GetIp() => HttpContext.Connection.RemoteIpAddress?.ToString();

        [HttpGet]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<IEnumerable<SupplierResponseDto>>> GetAll()
        {
            var result = await _supplierService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<SupplierResponseDto>> GetById(int id)
        {
            var result = await _supplierService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<SupplierResponseDto>> Create([FromBody] SupplierRequestDto dto)
        {
            var username = GetUsername();
            var result = await _supplierService.Create(dto);
            await _auditLog.LogAsync(username, "CREATE", "Supplier", result.SupplierId.ToString(),
                $"Created new merchant dispatch merchant account: {dto.SupplierName}", GetIp());
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<SupplierResponseDto>> Update(int id, [FromBody] SupplierRequestDto dto)
        {
            var username = GetUsername();
            var result = await _supplierService.Update(id, dto);
            await _auditLog.LogAsync(username, "UPDATE", "Supplier", id.ToString(),
                $"Adjusted merchant logistics data for supplier ID {id}", GetIp());
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> Delete(int id)
        {
            var username = GetUsername();
            await _supplierService.Delete(id);
            await _auditLog.LogAsync(username, "DELETE", "Supplier", id.ToString(),
                $"Removed active supplier index record trace ID {id}", GetIp());
            return NoContent();
        }
    }
}