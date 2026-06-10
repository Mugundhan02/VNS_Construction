using BuildManager.DTOs;
using BuildManager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Owner,Admin")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        private readonly IAuditLogService _auditLog;

        public SupplierController(ISupplierService supplierService, IAuditLogService auditLog)
        {
            _supplierService = supplierService;
            _auditLog = auditLog;
        }

        private string? GetIp() => HttpContext.Connection.RemoteIpAddress?.ToString();
        private string GetUser() => User.Identity?.Name ?? "unknown";

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierResponseDto>>> GetAll()
            => Ok(await _supplierService.GetAll());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SupplierResponseDto>> GetById(int id)
            => Ok(await _supplierService.GetById(id));

        [HttpPost]
        public async Task<ActionResult<SupplierResponseDto>> Create([FromBody] SupplierRequestDto dto)
        {
            var result = await _supplierService.Create(dto);
            await _auditLog.LogAsync(GetUser(), "CREATE", "Supplier", result.SupplierId.ToString(), $"Created new merchant dispatch merchant account: {dto.SupplierName}", GetIp());
            return CreatedAtAction(nameof(GetById), new { id = result.SupplierId }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<SupplierResponseDto>> Update(int id, [FromBody] SupplierRequestDto dto)
        {
            var result = await _supplierService.Update(id, dto);
            await _auditLog.LogAsync(GetUser(), "UPDATE", "Supplier", id.ToString(), $"Adjusted merchant logistics data for supplier ID {id}", GetIp());
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> Delete(int id)
        {
            await _supplierService.Delete(id);
            await _auditLog.LogAsync(GetUser(), "DELETE", "Supplier", id.ToString(), $"Removed active supplier index record trace ID {id}", GetIp());
            return NoContent();
        }
    }
}