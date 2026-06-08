using BuildManager.DTOs;
using BuildManager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IAuditLogService    _auditLog;

        public TransactionController(ITransactionService transactionService, IAuditLogService auditLog)
        {
            _transactionService = transactionService;
            _auditLog           = auditLog;
        }

        private string? GetIp()   => HttpContext.Connection.RemoteIpAddress?.ToString();
        private string  GetUser() => User.Identity?.Name ?? "unknown";

        // ── Dashboard / Summary ───────────────────────────────────────────────

        [HttpGet("summary/company/{companyId:int}")]
        public async Task<ActionResult<CompanySummaryDto>> GetCompanySummary(int companyId)
            => Ok(await _transactionService.GetCompanySummary(companyId));

        [HttpGet("summary/client/{clientId:int}")]
        public async Task<ActionResult<ClientSummaryDto>> GetClientSummary(int clientId)
            => Ok(await _transactionService.GetClientSummary(clientId));

        [HttpGet("summary/client/{clientId:int}/suppliers")]
        public async Task<ActionResult<IEnumerable<SupplierSummaryDto>>> GetSupplierSummary(int clientId)
            => Ok(await _transactionService.GetSupplierSummaryByClient(clientId));

        [HttpGet("summary/client/{clientId:int}/subcontractors")]
        public async Task<ActionResult<IEnumerable<SubContractorSummaryDto>>> GetSubContractorSummary(int clientId)
            => Ok(await _transactionService.GetSubContractorSummaryByClient(clientId));

        // ── Client Transactions ───────────────────────────────────────────────

        [HttpGet("client")]
        public async Task<ActionResult<IEnumerable<ClientTransactionResponseDto>>> GetClientTransactions([FromQuery] int? clientId = null)
            => Ok(await _transactionService.GetClientTransactions(clientId));

        [HttpGet("client/{id:int}")]
        public async Task<ActionResult<ClientTransactionResponseDto>> GetClientTransactionById(int id)
            => Ok(await _transactionService.GetClientTransactionById(id));

        [HttpPost("client")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<ClientTransactionResponseDto>> CreateClientTransaction([FromBody] ClientTransactionRequestDto dto)
        {
            var result = await _transactionService.CreateClientTransaction(dto);
            await _auditLog.LogAsync(GetUser(), "CREATE", "ClientTransaction", result.ClientTransactionId.ToString(), "ClientTransaction created", GetIp());
            return CreatedAtAction(nameof(GetClientTransactionById), new { id = result.ClientTransactionId }, result);
        }

        [HttpPut("client/{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<ClientTransactionResponseDto>> UpdateClientTransaction(int id, [FromBody] ClientTransactionRequestDto dto)
        {
            var result = await _transactionService.UpdateClientTransaction(id, dto);
            await _auditLog.LogAsync(GetUser(), "UPDATE", "ClientTransaction", id.ToString(), "ClientTransaction updated", GetIp());
            return Ok(result);
        }

        [HttpDelete("client/{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> DeleteClientTransaction(int id)
        {
            await _transactionService.DeleteClientTransaction(id);
            await _auditLog.LogAsync(GetUser(), "DELETE", "ClientTransaction", id.ToString(), "ClientTransaction deleted", GetIp());
            return NoContent();
        }

        // ── Supplier Transactions ─────────────────────────────────────────────

        [HttpGet("supplier")]
        public async Task<ActionResult<IEnumerable<SupplierTransactionResponseDto>>> GetSupplierTransactions(
            [FromQuery] int? clientId   = null,
            [FromQuery] int? supplierId = null)
            => Ok(await _transactionService.GetSupplierTransactions(clientId, supplierId));

        [HttpGet("supplier/{id:int}")]
        public async Task<ActionResult<SupplierTransactionResponseDto>> GetSupplierTransactionById(int id)
            => Ok(await _transactionService.GetSupplierTransactionById(id));

        [HttpPost("supplier")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<SupplierTransactionResponseDto>> CreateSupplierTransaction([FromBody] SupplierTransactionRequestDto dto)
        {
            var result = await _transactionService.CreateSupplierTransaction(dto);
            await _auditLog.LogAsync(GetUser(), "CREATE", "SupplierTransaction", result.SupplierTransactionId.ToString(), "SupplierTransaction created", GetIp());
            return CreatedAtAction(nameof(GetSupplierTransactionById), new { id = result.SupplierTransactionId }, result);
        }

        [HttpPut("supplier/{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<SupplierTransactionResponseDto>> UpdateSupplierTransaction(int id, [FromBody] SupplierTransactionRequestDto dto)
        {
            var result = await _transactionService.UpdateSupplierTransaction(id, dto);
            await _auditLog.LogAsync(GetUser(), "UPDATE", "SupplierTransaction", id.ToString(), "SupplierTransaction updated", GetIp());
            return Ok(result);
        }

        [HttpDelete("supplier/{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> DeleteSupplierTransaction(int id)
        {
            await _transactionService.DeleteSupplierTransaction(id);
            await _auditLog.LogAsync(GetUser(), "DELETE", "SupplierTransaction", id.ToString(), "SupplierTransaction deleted", GetIp());
            return NoContent();
        }

        // ── SubContractor Transactions ────────────────────────────────────────

        [HttpGet("subcontractor")]
        public async Task<ActionResult<IEnumerable<SubContractorTransactionResponseDto>>> GetSubContractorTransactions(
            [FromQuery] int? clientId        = null,
            [FromQuery] int? subContractorId = null)
            => Ok(await _transactionService.GetSubContractorTransactions(clientId, subContractorId));

        [HttpGet("subcontractor/{id:int}")]
        public async Task<ActionResult<SubContractorTransactionResponseDto>> GetSubContractorTransactionById(int id)
            => Ok(await _transactionService.GetSubContractorTransactionById(id));

        [HttpPost("subcontractor")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<SubContractorTransactionResponseDto>> CreateSubContractorTransaction([FromBody] SubContractorTransactionRequestDto dto)
        {
            var result = await _transactionService.CreateSubContractorTransaction(dto);
            await _auditLog.LogAsync(GetUser(), "CREATE", "SubContractorTransaction", result.SubContractorTransactionId.ToString(), "SubContractorTransaction created", GetIp());
            return CreatedAtAction(nameof(GetSubContractorTransactionById), new { id = result.SubContractorTransactionId }, result);
        }

        [HttpPut("subcontractor/{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<SubContractorTransactionResponseDto>> UpdateSubContractorTransaction(int id, [FromBody] SubContractorTransactionRequestDto dto)
        {
            var result = await _transactionService.UpdateSubContractorTransaction(id, dto);
            await _auditLog.LogAsync(GetUser(), "UPDATE", "SubContractorTransaction", id.ToString(), "SubContractorTransaction updated", GetIp());
            return Ok(result);
        }

        [HttpDelete("subcontractor/{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> DeleteSubContractorTransaction(int id)
        {
            await _transactionService.DeleteSubContractorTransaction(id);
            await _auditLog.LogAsync(GetUser(), "DELETE", "SubContractorTransaction", id.ToString(), "SubContractorTransaction deleted", GetIp());
            return NoContent();
        }

        // ── Company Expense Transactions ──────────────────────────────────────

        [HttpGet("expense")]
        public async Task<ActionResult<IEnumerable<CompanyExpenseTransactionResponseDto>>> GetExpenseTransactions(
            [FromQuery] int? companyId = null,
            [FromQuery] int? clientId  = null)
            => Ok(await _transactionService.GetCompanyExpenseTransactions(companyId, clientId));

        [HttpGet("expense/{id:int}")]
        public async Task<ActionResult<CompanyExpenseTransactionResponseDto>> GetExpenseTransactionById(int id)
            => Ok(await _transactionService.GetCompanyExpenseTransactionById(id));

        [HttpPost("expense")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<CompanyExpenseTransactionResponseDto>> CreateExpenseTransaction([FromBody] CompanyExpenseTransactionRequestDto dto)
        {
            var result = await _transactionService.CreateCompanyExpenseTransaction(dto);
            await _auditLog.LogAsync(GetUser(), "CREATE", "CompanyExpenseTransaction", result.CompanyExpenseTransactionId.ToString(), "ExpenseTransaction created", GetIp());
            return CreatedAtAction(nameof(GetExpenseTransactionById), new { id = result.CompanyExpenseTransactionId }, result);
        }

        [HttpPut("expense/{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<CompanyExpenseTransactionResponseDto>> UpdateExpenseTransaction(int id, [FromBody] CompanyExpenseTransactionRequestDto dto)
        {
            var result = await _transactionService.UpdateCompanyExpenseTransaction(id, dto);
            await _auditLog.LogAsync(GetUser(), "UPDATE", "CompanyExpenseTransaction", id.ToString(), "ExpenseTransaction updated", GetIp());
            return Ok(result);
        }

        [HttpDelete("expense/{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> DeleteExpenseTransaction(int id)
        {
            await _transactionService.DeleteCompanyExpenseTransaction(id);
            await _auditLog.LogAsync(GetUser(), "DELETE", "CompanyExpenseTransaction", id.ToString(), "ExpenseTransaction deleted", GetIp());
            return NoContent();
        }
    }
}
