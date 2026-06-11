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
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IAuditLogService _auditLog;

        public TransactionController(ITransactionService transactionService, IAuditLogService auditLog)
        {
            _transactionService = transactionService;
            _auditLog = auditLog;
        }

        private string GetUsername() => User.Identity?.Name ?? "unknown";
        private string? GetIp() => HttpContext.Connection.RemoteIpAddress?.ToString();

        // ── Dashboard / Summary ───────────────────────────────────────────────

        [HttpGet("summary/company/{companyId:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<CompanySummaryDto>> GetCompanySummary(int companyId)
        {
            var result = await _transactionService.GetCompanySummary(companyId);
            return Ok(result);
        }

        [HttpGet("summary/client/{clientId:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<ClientSummaryDto>> GetClientSummary(int clientId)
        {
            var result = await _transactionService.GetClientSummary(clientId);
            return Ok(result);
        }

        [HttpGet("summary/client/{clientId:int}/suppliers")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<IEnumerable<SupplierSummaryDto>>> GetSupplierSummary(int clientId)
        {
            var result = await _transactionService.GetSupplierSummaryByClient(clientId);
            return Ok(result);
        }

        [HttpGet("summary/client/{clientId:int}/subcontractors")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<IEnumerable<SubContractorSummaryDto>>> GetSubContractorSummary(int clientId)
        {
            var result = await _transactionService.GetSubContractorSummaryByClient(clientId);
            return Ok(result);
        }

        // ── Client Transactions ───────────────────────────────────────────────

        [HttpGet("client")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<IEnumerable<ClientTransactionResponseDto>>> GetClientTransactions([FromQuery] int? clientId = null)
        {
            var result = await _transactionService.GetClientTransactions(clientId);
            return Ok(result);
        }

        [HttpGet("client/{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<ClientTransactionResponseDto>> GetClientTransactionById(int id)
        {
            var result = await _transactionService.GetClientTransactionById(id);
            return Ok(result);
        }

        [HttpPost("client")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<ClientTransactionResponseDto>> CreateClientTransaction([FromBody] ClientTransactionRequestDto dto)
        {
            var username = GetUsername();
            var result = await _transactionService.CreateClientTransaction(dto);

            await _auditLog.LogAsync(username, "CREATE", "ClientTransaction", result.ClientTransactionId.ToString(),
                "Logged commercial client inflow remittance transaction asset", GetIp());

            return Ok(result);
        }

        [HttpPut("client/{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<ClientTransactionResponseDto>> UpdateClientTransaction(int id, [FromBody] ClientTransactionRequestDto dto)
        {
            var username = GetUsername();
            var result = await _transactionService.UpdateClientTransaction(id, dto);

            await _auditLog.LogAsync(username, "UPDATE", "ClientTransaction", id.ToString(),
                $"Corrected entry data for client transaction record ID {id}", GetIp());

            return Ok(result);
        }

        [HttpDelete("client/{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> DeleteClientTransaction(int id)
        {
            var username = GetUsername();
            await _transactionService.DeleteClientTransaction(id);

            await _auditLog.LogAsync(username, "DELETE", "ClientTransaction", id.ToString(),
                $"Voided billing accounting ledger line trace ID {id}", GetIp());

            return NoContent();
        }

        // ── Supplier Transactions ─────────────────────────────────────────────

        [HttpGet("supplier")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<IEnumerable<SupplierTransactionResponseDto>>> GetSupplierTransactions(
            [FromQuery] int? clientId = null,
            [FromQuery] int? supplierId = null)
        {
            var result = await _transactionService.GetSupplierTransactions(clientId, supplierId);
            return Ok(result);
        }

        [HttpGet("supplier/{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<SupplierTransactionResponseDto>> GetSupplierTransactionById(int id)
        {
            var result = await _transactionService.GetSupplierTransactionById(id);
            return Ok(result);
        }

        [HttpPost("supplier")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<SupplierTransactionResponseDto>> CreateSupplierTransaction([FromBody] SupplierTransactionRequestDto dto)
        {
            var username = GetUsername();
            var result = await _transactionService.CreateSupplierTransaction(dto);

            await _auditLog.LogAsync(username, "CREATE", "SupplierTransaction", result.SupplierTransactionId.ToString(),
                "Recorded merchant materials dispatch invoice expenditure line record", GetIp());

            return Ok(result);
        }

        [HttpPut("supplier/{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<SupplierTransactionResponseDto>> UpdateSupplierTransaction(int id, [FromBody] SupplierTransactionRequestDto dto)
        {
            var username = GetUsername();
            var result = await _transactionService.UpdateSupplierTransaction(id, dto);

            await _auditLog.LogAsync(username, "UPDATE", "SupplierTransaction", id.ToString(),
                $"Updated entry processing trace for vendor balance account statement ID {id}", GetIp());

            return Ok(result);
        }

        [HttpDelete("supplier/{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> DeleteSupplierTransaction(int id)
        {
            var username = GetUsername();
            await _transactionService.DeleteSupplierTransaction(id);

            await _auditLog.LogAsync(username, "DELETE", "SupplierTransaction", id.ToString(),
                $"Voided supplier payment record ledger route trace ID {id}", GetIp());

            return NoContent();
        }

        // ── SubContractor Transactions ────────────────────────────────────────

        [HttpGet("subcontractor")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<IEnumerable<SubContractorTransactionResponseDto>>> GetSubContractorTransactions(
            [FromQuery] int? clientId = null,
            [FromQuery] int? subContractorId = null)
        {
            var result = await _transactionService.GetSubContractorTransactions(clientId, subContractorId);
            return Ok(result);
        }

        [HttpGet("subcontractor/{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<SubContractorTransactionResponseDto>> GetSubContractorTransactionById(int id)
        {
            var result = await _transactionService.GetSubContractorTransactionById(id);
            return Ok(result);
        }

        [HttpPost("subcontractor")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<SubContractorTransactionResponseDto>> CreateSubContractorTransaction([FromBody] SubContractorTransactionRequestDto dto)
        {
            var username = GetUsername();
            var result = await _transactionService.CreateSubContractorTransaction(dto);

            await _auditLog.LogAsync(username, "CREATE", "SubContractorTransaction", result.SubContractorTransactionId.ToString(),
                "Committed deployment disbursement payout for site partner subcontractor", GetIp());

            return Ok(result);
        }

        [HttpPut("subcontractor/{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<SubContractorTransactionResponseDto>> UpdateSubContractorTransaction(int id, [FromBody] SubContractorTransactionRequestDto dto)
        {
            var username = GetUsername();
            var result = await _transactionService.UpdateSubContractorTransaction(id, dto);

            await _auditLog.LogAsync(username, "UPDATE", "SubContractorTransaction", id.ToString(),
                $"Updated ledger data properties for subcontractor payment allocation tracking entry ID {id}", GetIp());

            return Ok(result);
        }

        [HttpDelete("subcontractor/{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> DeleteSubContractorTransaction(int id)
        {
            var username = GetUsername();
            await _transactionService.DeleteSubContractorTransaction(id);

            await _auditLog.LogAsync(username, "DELETE", "SubContractorTransaction", id.ToString(),
                $"Voided subcontractor work settlement record tracker ID {id}", GetIp());

            return NoContent();
        }

        // ── Company Expense Transactions ──────────────────────────────────────

        [HttpGet("expense")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<IEnumerable<CompanyExpenseTransactionResponseDto>>> GetExpenseTransactions(
            [FromQuery] int? companyId = null,
            [FromQuery] int? clientId = null)
        {
            var result = await _transactionService.GetCompanyExpenseTransactions(companyId, clientId);
            return Ok(result);
        }

        [HttpGet("expense/{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<CompanyExpenseTransactionResponseDto>> GetExpenseTransactionById(int id)
        {
            var result = await _transactionService.GetCompanyExpenseTransactionById(id);
            return Ok(result);
        }

        [HttpPost("expense")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<CompanyExpenseTransactionResponseDto>> CreateExpenseTransaction([FromBody] CompanyExpenseTransactionRequestDto dto)
        {
            var username = GetUsername();
            var result = await _transactionService.CreateCompanyExpenseTransaction(dto);

            await _auditLog.LogAsync(username, "CREATE", "CompanyExpenseTransaction", result.CompanyExpenseTransactionId.ToString(),
                "Logged field operational petty expense debit balance", GetIp());

            return Ok(result);
        }

        [HttpPut("expense/{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<CompanyExpenseTransactionResponseDto>> UpdateExpenseTransaction(int id, [FromBody] CompanyExpenseTransactionRequestDto dto)
        {
            var username = GetUsername();
            var result = await _transactionService.UpdateCompanyExpenseTransaction(id, dto);

            await _auditLog.LogAsync(username, "UPDATE", "CompanyExpenseTransaction", id.ToString(),
                $"Corrected transaction trace entry configuration parameters for expense data item ID {id}", GetIp());

            return Ok(result);
        }

        [HttpDelete("expense/{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> DeleteExpenseTransaction(int id)
        {
            var username = GetUsername();
            await _transactionService.DeleteCompanyExpenseTransaction(id);

            await _auditLog.LogAsync(username, "DELETE", "CompanyExpenseTransaction", id.ToString(),
                $"Voided structural field expense tracking log entry ID {id}", GetIp());

            return NoContent();
        }
    }
}