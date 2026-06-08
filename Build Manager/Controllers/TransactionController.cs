using BuildManager.DTOs;
using BuildManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // ── Dashboard / Summary ───────────────────────────────────────────────

        /// <summary>
        /// Get company-level financial summary (total credits, debits, balance).
        /// Mirrors the "New Transaction" overview screen.
        /// </summary>
        [HttpGet("summary/company/{companyId:int}")]
        [ProducesResponseType(typeof(CompanySummaryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCompanySummary(int companyId)
            => Ok(await _transactionService.GetCompanySummaryAsync(companyId));

        /// <summary>
        /// Get client-level financial summary including estimate vs actual.
        /// </summary>
        [HttpGet("summary/client/{clientId:int}")]
        [ProducesResponseType(typeof(ClientSummaryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetClientSummary(int clientId)
        {
            var result = await _transactionService.GetClientSummaryAsync(clientId);
            if (result is null)
                return NotFound(new { message = $"Client {clientId} not found." });

            return Ok(result);
        }

        /// <summary>
        /// Get supplier payable / paid / balance summary for a client.
        /// </summary>
        [HttpGet("summary/client/{clientId:int}/suppliers")]
        [ProducesResponseType(typeof(IEnumerable<SupplierSummaryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSupplierSummary(int clientId)
            => Ok(await _transactionService.GetSupplierSummaryByClientAsync(clientId));

        /// <summary>
        /// Get sub-contractor payable / paid / balance summary for a client.
        /// </summary>
        [HttpGet("summary/client/{clientId:int}/subcontractors")]
        [ProducesResponseType(typeof(IEnumerable<SubContractorSummaryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSubContractorSummary(int clientId)
            => Ok(await _transactionService.GetSubContractorSummaryByClientAsync(clientId));

        // ── Client Transactions ───────────────────────────────────────────────

        /// <summary>
        /// Get client transactions. Optional filter by clientId.
        /// </summary>
        [HttpGet("client")]
        [ProducesResponseType(typeof(IEnumerable<ClientTransactionResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetClientTransactions([FromQuery] int? clientId = null)
            => Ok(await _transactionService.GetClientTransactionsAsync(clientId));

        /// <summary>Get a single client transaction by ID.</summary>
        [HttpGet("client/{id:int}")]
        [ProducesResponseType(typeof(ClientTransactionResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetClientTransactionById(int id)
        {
            var result = await _transactionService.GetClientTransactionByIdAsync(id);
            if (result is null)
                return NotFound(new { message = $"ClientTransaction {id} not found." });

            return Ok(result);
        }

        /// <summary>Create a client transaction. (Owner, Admin)</summary>
        [HttpPost("client")]
        [Authorize(Roles = "Owner,Admin")]
        [ProducesResponseType(typeof(ClientTransactionResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateClientTransaction([FromBody] ClientTransactionRequestDto dto)
        {
            var result = await _transactionService.CreateClientTransactionAsync(dto);
            return CreatedAtAction(nameof(GetClientTransactionById),
                new { id = result.ClientTransactionId }, result);
        }

        /// <summary>Update a client transaction. (Owner, Admin)</summary>
        [HttpPut("client/{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        [ProducesResponseType(typeof(ClientTransactionResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateClientTransaction(int id, [FromBody] ClientTransactionRequestDto dto)
        {
            var result = await _transactionService.UpdateClientTransactionAsync(id, dto);
            if (result is null)
                return NotFound(new { message = $"ClientTransaction {id} not found." });

            return Ok(result);
        }

        /// <summary>Delete a client transaction. (Owner only)</summary>
        [HttpDelete("client/{id:int}")]
        [Authorize(Roles = "Owner")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteClientTransaction(int id)
        {
            var deleted = await _transactionService.DeleteClientTransactionAsync(id);
            if (!deleted)
                return NotFound(new { message = $"ClientTransaction {id} not found." });

            return NoContent();
        }

        // ── Supplier Transactions ─────────────────────────────────────────────

        /// <summary>
        /// Get supplier transactions. Optional filter by clientId and/or supplierId.
        /// </summary>
        [HttpGet("supplier")]
        [ProducesResponseType(typeof(IEnumerable<SupplierTransactionResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSupplierTransactions(
            [FromQuery] int? clientId   = null,
            [FromQuery] int? supplierId = null)
            => Ok(await _transactionService.GetSupplierTransactionsAsync(clientId, supplierId));

        /// <summary>Get a single supplier transaction by ID.</summary>
        [HttpGet("supplier/{id:int}")]
        [ProducesResponseType(typeof(SupplierTransactionResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSupplierTransactionById(int id)
        {
            var result = await _transactionService.GetSupplierTransactionByIdAsync(id);
            if (result is null)
                return NotFound(new { message = $"SupplierTransaction {id} not found." });

            return Ok(result);
        }

        /// <summary>Create a supplier transaction. (Owner, Admin)</summary>
        [HttpPost("supplier")]
        [Authorize(Roles = "Owner,Admin")]
        [ProducesResponseType(typeof(SupplierTransactionResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSupplierTransaction([FromBody] SupplierTransactionRequestDto dto)
        {
            var result = await _transactionService.CreateSupplierTransactionAsync(dto);
            return CreatedAtAction(nameof(GetSupplierTransactionById),
                new { id = result.SupplierTransactionId }, result);
        }

        /// <summary>Update a supplier transaction. (Owner, Admin)</summary>
        [HttpPut("supplier/{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        [ProducesResponseType(typeof(SupplierTransactionResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSupplierTransaction(int id, [FromBody] SupplierTransactionRequestDto dto)
        {
            var result = await _transactionService.UpdateSupplierTransactionAsync(id, dto);
            if (result is null)
                return NotFound(new { message = $"SupplierTransaction {id} not found." });

            return Ok(result);
        }

        /// <summary>Delete a supplier transaction. (Owner only)</summary>
        [HttpDelete("supplier/{id:int}")]
        [Authorize(Roles = "Owner")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSupplierTransaction(int id)
        {
            var deleted = await _transactionService.DeleteSupplierTransactionAsync(id);
            if (!deleted)
                return NotFound(new { message = $"SupplierTransaction {id} not found." });

            return NoContent();
        }

        // ── SubContractor Transactions ────────────────────────────────────────

        /// <summary>
        /// Get sub-contractor transactions. Optional filter by clientId and/or subContractorId.
        /// </summary>
        [HttpGet("subcontractor")]
        [ProducesResponseType(typeof(IEnumerable<SubContractorTransactionResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSubContractorTransactions(
            [FromQuery] int? clientId        = null,
            [FromQuery] int? subContractorId = null)
            => Ok(await _transactionService.GetSubContractorTransactionsAsync(clientId, subContractorId));

        /// <summary>Get a single sub-contractor transaction by ID.</summary>
        [HttpGet("subcontractor/{id:int}")]
        [ProducesResponseType(typeof(SubContractorTransactionResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSubContractorTransactionById(int id)
        {
            var result = await _transactionService.GetSubContractorTransactionByIdAsync(id);
            if (result is null)
                return NotFound(new { message = $"SubContractorTransaction {id} not found." });

            return Ok(result);
        }

        /// <summary>Create a sub-contractor transaction. (Owner, Admin)</summary>
        [HttpPost("subcontractor")]
        [Authorize(Roles = "Owner,Admin")]
        [ProducesResponseType(typeof(SubContractorTransactionResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSubContractorTransaction([FromBody] SubContractorTransactionRequestDto dto)
        {
            var result = await _transactionService.CreateSubContractorTransactionAsync(dto);
            return CreatedAtAction(nameof(GetSubContractorTransactionById),
                new { id = result.SubContractorTransactionId }, result);
        }

        /// <summary>Update a sub-contractor transaction. (Owner, Admin)</summary>
        [HttpPut("subcontractor/{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        [ProducesResponseType(typeof(SubContractorTransactionResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSubContractorTransaction(int id, [FromBody] SubContractorTransactionRequestDto dto)
        {
            var result = await _transactionService.UpdateSubContractorTransactionAsync(id, dto);
            if (result is null)
                return NotFound(new { message = $"SubContractorTransaction {id} not found." });

            return Ok(result);
        }

        /// <summary>Delete a sub-contractor transaction. (Owner only)</summary>
        [HttpDelete("subcontractor/{id:int}")]
        [Authorize(Roles = "Owner")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSubContractorTransaction(int id)
        {
            var deleted = await _transactionService.DeleteSubContractorTransactionAsync(id);
            if (!deleted)
                return NotFound(new { message = $"SubContractorTransaction {id} not found." });

            return NoContent();
        }

        // ── Company Expense Transactions ──────────────────────────────────────

        /// <summary>
        /// Get company expense transactions. Optional filter by companyId and/or clientId.
        /// </summary>
        [HttpGet("expense")]
        [ProducesResponseType(typeof(IEnumerable<CompanyExpenseTransactionResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetExpenseTransactions(
            [FromQuery] int? companyId = null,
            [FromQuery] int? clientId  = null)
            => Ok(await _transactionService.GetCompanyExpenseTransactionsAsync(companyId, clientId));

        /// <summary>Get a single company expense transaction by ID.</summary>
        [HttpGet("expense/{id:int}")]
        [ProducesResponseType(typeof(CompanyExpenseTransactionResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetExpenseTransactionById(int id)
        {
            var result = await _transactionService.GetCompanyExpenseTransactionByIdAsync(id);
            if (result is null)
                return NotFound(new { message = $"ExpenseTransaction {id} not found." });

            return Ok(result);
        }

        /// <summary>Create a company expense transaction. (Owner, Admin)</summary>
        [HttpPost("expense")]
        [Authorize(Roles = "Owner,Admin")]
        [ProducesResponseType(typeof(CompanyExpenseTransactionResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateExpenseTransaction([FromBody] CompanyExpenseTransactionRequestDto dto)
        {
            var result = await _transactionService.CreateCompanyExpenseTransactionAsync(dto);
            return CreatedAtAction(nameof(GetExpenseTransactionById),
                new { id = result.CompanyExpenseTransactionId }, result);
        }

        /// <summary>Update a company expense transaction. (Owner, Admin)</summary>
        [HttpPut("expense/{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        [ProducesResponseType(typeof(CompanyExpenseTransactionResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateExpenseTransaction(int id, [FromBody] CompanyExpenseTransactionRequestDto dto)
        {
            var result = await _transactionService.UpdateCompanyExpenseTransactionAsync(id, dto);
            if (result is null)
                return NotFound(new { message = $"ExpenseTransaction {id} not found." });

            return Ok(result);
        }

        /// <summary>Delete a company expense transaction. (Owner only)</summary>
        [HttpDelete("expense/{id:int}")]
        [Authorize(Roles = "Owner")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteExpenseTransaction(int id)
        {
            var deleted = await _transactionService.DeleteCompanyExpenseTransactionAsync(id);
            if (!deleted)
                return NotFound(new { message = $"ExpenseTransaction {id} not found." });

            return NoContent();
        }
    }
}
