using BuildManager.DTOs;
using BuildManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildManager.Controllers
{
    /// <summary>
    /// Handles all lookup master data:
    /// Payment Types, Whom, Office Expenses, Company Banks, Installment Terms.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LookupController : ControllerBase
    {
        private readonly ILookupService _lookupService;

        public LookupController(ILookupService lookupService)
        {
            _lookupService = lookupService;
        }

        // ── Payment Types ─────────────────────────────────────────────────────

        [HttpGet("payment-types")]
        [ProducesResponseType(typeof(IEnumerable<PaymentTypeResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPaymentTypes()
            => Ok(await _lookupService.GetAllPaymentTypesAsync());

        [HttpPost("payment-types")]
        [Authorize(Roles = "Owner,Admin")]
        [ProducesResponseType(typeof(PaymentTypeResponseDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePaymentType([FromBody] PaymentTypeRequestDto dto)
        {
            var result = await _lookupService.CreatePaymentTypeAsync(dto);
            return Created(string.Empty, result);
        }

        [HttpPut("payment-types/{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        [ProducesResponseType(typeof(PaymentTypeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePaymentType(int id, [FromBody] PaymentTypeRequestDto dto)
        {
            var result = await _lookupService.UpdatePaymentTypeAsync(id, dto);
            if (result is null)
                return NotFound(new { message = $"PaymentType {id} not found." });

            return Ok(result);
        }

        [HttpDelete("payment-types/{id:int}")]
        [Authorize(Roles = "Owner")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePaymentType(int id)
        {
            var deleted = await _lookupService.DeletePaymentTypeAsync(id);
            if (!deleted)
                return NotFound(new { message = $"PaymentType {id} not found." });

            return NoContent();
        }

        // ── Whom ──────────────────────────────────────────────────────────────

        [HttpGet("whoms")]
        [ProducesResponseType(typeof(IEnumerable<WhomResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWhoms()
            => Ok(await _lookupService.GetAllWhomAsync());

        [HttpPost("whoms")]
        [Authorize(Roles = "Owner,Admin")]
        [ProducesResponseType(typeof(WhomResponseDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateWhom([FromBody] WhomRequestDto dto)
        {
            var result = await _lookupService.CreateWhomAsync(dto);
            return Created(string.Empty, result);
        }

        [HttpPut("whoms/{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        [ProducesResponseType(typeof(WhomResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateWhom(int id, [FromBody] WhomRequestDto dto)
        {
            var result = await _lookupService.UpdateWhomAsync(id, dto);
            if (result is null)
                return NotFound(new { message = $"Whom {id} not found." });

            return Ok(result);
        }

        [HttpDelete("whoms/{id:int}")]
        [Authorize(Roles = "Owner")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteWhom(int id)
        {
            var deleted = await _lookupService.DeleteWhomAsync(id);
            if (!deleted)
                return NotFound(new { message = $"Whom {id} not found." });

            return NoContent();
        }

        // ── Office Expenses ───────────────────────────────────────────────────

        [HttpGet("office-expenses")]
        [ProducesResponseType(typeof(IEnumerable<OfficeExpenseResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOfficeExpenses()
            => Ok(await _lookupService.GetAllOfficeExpensesAsync());

        [HttpPost("office-expenses")]
        [Authorize(Roles = "Owner,Admin")]
        [ProducesResponseType(typeof(OfficeExpenseResponseDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateOfficeExpense([FromBody] OfficeExpenseRequestDto dto)
        {
            var result = await _lookupService.CreateOfficeExpenseAsync(dto);
            return Created(string.Empty, result);
        }

        [HttpPut("office-expenses/{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        [ProducesResponseType(typeof(OfficeExpenseResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOfficeExpense(int id, [FromBody] OfficeExpenseRequestDto dto)
        {
            var result = await _lookupService.UpdateOfficeExpenseAsync(id, dto);
            if (result is null)
                return NotFound(new { message = $"OfficeExpense {id} not found." });

            return Ok(result);
        }

        [HttpDelete("office-expenses/{id:int}")]
        [Authorize(Roles = "Owner")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOfficeExpense(int id)
        {
            var deleted = await _lookupService.DeleteOfficeExpenseAsync(id);
            if (!deleted)
                return NotFound(new { message = $"OfficeExpense {id} not found." });

            return NoContent();
        }

        // ── Company Banks ─────────────────────────────────────────────────────

        [HttpGet("banks/company/{companyId:int}")]
        [ProducesResponseType(typeof(IEnumerable<CompanyBankResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBanksByCompany(int companyId)
            => Ok(await _lookupService.GetBanksByCompanyAsync(companyId));

        [HttpPost("banks")]
        [Authorize(Roles = "Owner")]
        [ProducesResponseType(typeof(CompanyBankResponseDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateBank([FromBody] CompanyBankRequestDto dto)
        {
            var result = await _lookupService.CreateCompanyBankAsync(dto);
            return Created(string.Empty, result);
        }

        [HttpPut("banks/{id:int}")]
        [Authorize(Roles = "Owner")]
        [ProducesResponseType(typeof(CompanyBankResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBank(int id, [FromBody] CompanyBankRequestDto dto)
        {
            var result = await _lookupService.UpdateCompanyBankAsync(id, dto);
            if (result is null)
                return NotFound(new { message = $"CompanyBank {id} not found." });

            return Ok(result);
        }

        [HttpDelete("banks/{id:int}")]
        [Authorize(Roles = "Owner")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBank(int id)
        {
            var deleted = await _lookupService.DeleteCompanyBankAsync(id);
            if (!deleted)
                return NotFound(new { message = $"CompanyBank {id} not found." });

            return NoContent();
        }

        // ── Installment Terms ─────────────────────────────────────────────────

        [HttpGet("installment-terms")]
        [ProducesResponseType(typeof(IEnumerable<InstallmentTermResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInstallmentTerms()
            => Ok(await _lookupService.GetAllInstallmentTermsAsync());

        [HttpPost("installment-terms")]
        [Authorize(Roles = "Owner,Admin")]
        [ProducesResponseType(typeof(InstallmentTermResponseDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateInstallmentTerm([FromBody] InstallmentTermRequestDto dto)
        {
            var result = await _lookupService.CreateInstallmentTermAsync(dto);
            return Created(string.Empty, result);
        }

        [HttpPut("installment-terms/{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        [ProducesResponseType(typeof(InstallmentTermResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateInstallmentTerm(int id, [FromBody] InstallmentTermRequestDto dto)
        {
            var result = await _lookupService.UpdateInstallmentTermAsync(id, dto);
            if (result is null)
                return NotFound(new { message = $"InstallmentTerm {id} not found." });

            return Ok(result);
        }

        [HttpDelete("installment-terms/{id:int}")]
        [Authorize(Roles = "Owner")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteInstallmentTerm(int id)
        {
            var deleted = await _lookupService.DeleteInstallmentTermAsync(id);
            if (!deleted)
                return NotFound(new { message = $"InstallmentTerm {id} not found." });

            return NoContent();
        }
    }
}
