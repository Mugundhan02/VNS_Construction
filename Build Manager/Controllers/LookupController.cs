using BuildManager.DTOs;
using BuildManager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LookupController : ControllerBase
    {
        private readonly ILookupService   _lookupService;
        private readonly IAuditLogService _auditLog;

        public LookupController(ILookupService lookupService, IAuditLogService auditLog)
        {
            _lookupService = lookupService;
            _auditLog      = auditLog;
        }

        private string? GetIp()   => HttpContext.Connection.RemoteIpAddress?.ToString();
        private string  GetUser() => User.Identity?.Name ?? "unknown";

        // ── Payment Types ─────────────────────────────────────────────────────

        [HttpGet("payment-types")]
        public async Task<ActionResult<IEnumerable<PaymentTypeResponseDto>>> GetPaymentTypes()
            => Ok(await _lookupService.GetAllPaymentTypes());

        [HttpPost("payment-types")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<PaymentTypeResponseDto>> CreatePaymentType([FromBody] PaymentTypeRequestDto dto)
        {
            var result = await _lookupService.CreatePaymentType(dto);
            await _auditLog.LogAsync(GetUser(), "CREATE", "PaymentType", result.PaymentTypeId.ToString(), "PaymentType created", GetIp());
            return Created(string.Empty, result);
        }

        [HttpPut("payment-types/{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<PaymentTypeResponseDto>> UpdatePaymentType(int id, [FromBody] PaymentTypeRequestDto dto)
        {
            var result = await _lookupService.UpdatePaymentType(id, dto);
            await _auditLog.LogAsync(GetUser(), "UPDATE", "PaymentType", id.ToString(), "PaymentType updated", GetIp());
            return Ok(result);
        }

        [HttpDelete("payment-types/{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> DeletePaymentType(int id)
        {
            await _lookupService.DeletePaymentType(id);
            await _auditLog.LogAsync(GetUser(), "DELETE", "PaymentType", id.ToString(), "PaymentType deleted", GetIp());
            return NoContent();
        }

        // ── Whom ──────────────────────────────────────────────────────────────

        [HttpGet("whoms")]
        public async Task<ActionResult<IEnumerable<WhomResponseDto>>> GetWhoms()
            => Ok(await _lookupService.GetAllWhom());

        [HttpPost("whoms")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<WhomResponseDto>> CreateWhom([FromBody] WhomRequestDto dto)
        {
            var result = await _lookupService.CreateWhom(dto);
            await _auditLog.LogAsync(GetUser(), "CREATE", "Whom", result.WhomId.ToString(), "Whom created", GetIp());
            return Created(string.Empty, result);
        }

        [HttpPut("whoms/{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<WhomResponseDto>> UpdateWhom(int id, [FromBody] WhomRequestDto dto)
        {
            var result = await _lookupService.UpdateWhom(id, dto);
            await _auditLog.LogAsync(GetUser(), "UPDATE", "Whom", id.ToString(), "Whom updated", GetIp());
            return Ok(result);
        }

        [HttpDelete("whoms/{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> DeleteWhom(int id)
        {
            await _lookupService.DeleteWhom(id);
            await _auditLog.LogAsync(GetUser(), "DELETE", "Whom", id.ToString(), "Whom deleted", GetIp());
            return NoContent();
        }

        // ── Office Expenses ───────────────────────────────────────────────────

        [HttpGet("office-expenses")]
        public async Task<ActionResult<IEnumerable<OfficeExpenseResponseDto>>> GetOfficeExpenses()
            => Ok(await _lookupService.GetAllOfficeExpenses());

        [HttpPost("office-expenses")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<OfficeExpenseResponseDto>> CreateOfficeExpense([FromBody] OfficeExpenseRequestDto dto)
        {
            var result = await _lookupService.CreateOfficeExpense(dto);
            await _auditLog.LogAsync(GetUser(), "CREATE", "OfficeExpense", result.OfficeExpenseId.ToString(), "OfficeExpense created", GetIp());
            return Created(string.Empty, result);
        }

        [HttpPut("office-expenses/{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<OfficeExpenseResponseDto>> UpdateOfficeExpense(int id, [FromBody] OfficeExpenseRequestDto dto)
        {
            var result = await _lookupService.UpdateOfficeExpense(id, dto);
            await _auditLog.LogAsync(GetUser(), "UPDATE", "OfficeExpense", id.ToString(), "OfficeExpense updated", GetIp());
            return Ok(result);
        }

        [HttpDelete("office-expenses/{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> DeleteOfficeExpense(int id)
        {
            await _lookupService.DeleteOfficeExpense(id);
            await _auditLog.LogAsync(GetUser(), "DELETE", "OfficeExpense", id.ToString(), "OfficeExpense deleted", GetIp());
            return NoContent();
        }

        // ── Company Banks ─────────────────────────────────────────────────────

        [HttpGet("banks/company/{companyId:int}")]
        public async Task<ActionResult<IEnumerable<CompanyBankResponseDto>>> GetBanksByCompany(int companyId)
            => Ok(await _lookupService.GetBanksByCompany(companyId));

        [HttpPost("banks")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult<CompanyBankResponseDto>> CreateBank([FromBody] CompanyBankRequestDto dto)
        {
            var result = await _lookupService.CreateCompanyBank(dto);
            await _auditLog.LogAsync(GetUser(), "CREATE", "CompanyBank", result.CompanyBankId.ToString(), "CompanyBank created", GetIp());
            return Created(string.Empty, result);
        }

        [HttpPut("banks/{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult<CompanyBankResponseDto>> UpdateBank(int id, [FromBody] CompanyBankRequestDto dto)
        {
            var result = await _lookupService.UpdateCompanyBank(id, dto);
            await _auditLog.LogAsync(GetUser(), "UPDATE", "CompanyBank", id.ToString(), "CompanyBank updated", GetIp());
            return Ok(result);
        }

        [HttpDelete("banks/{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> DeleteBank(int id)
        {
            await _lookupService.DeleteCompanyBank(id);
            await _auditLog.LogAsync(GetUser(), "DELETE", "CompanyBank", id.ToString(), "CompanyBank deleted", GetIp());
            return NoContent();
        }

        // ── Installment Terms ─────────────────────────────────────────────────

        [HttpGet("installment-terms")]
        public async Task<ActionResult<IEnumerable<InstallmentTermResponseDto>>> GetInstallmentTerms()
            => Ok(await _lookupService.GetAllInstallmentTerms());

        [HttpPost("installment-terms")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<InstallmentTermResponseDto>> CreateInstallmentTerm([FromBody] InstallmentTermRequestDto dto)
        {
            var result = await _lookupService.CreateInstallmentTerm(dto);
            await _auditLog.LogAsync(GetUser(), "CREATE", "InstallmentTerm", result.InstallmentTermId.ToString(), "InstallmentTerm created", GetIp());
            return Created(string.Empty, result);
        }

        [HttpPut("installment-terms/{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<InstallmentTermResponseDto>> UpdateInstallmentTerm(int id, [FromBody] InstallmentTermRequestDto dto)
        {
            var result = await _lookupService.UpdateInstallmentTerm(id, dto);
            await _auditLog.LogAsync(GetUser(), "UPDATE", "InstallmentTerm", id.ToString(), "InstallmentTerm updated", GetIp());
            return Ok(result);
        }

        [HttpDelete("installment-terms/{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> DeleteInstallmentTerm(int id)
        {
            await _lookupService.DeleteInstallmentTerm(id);
            await _auditLog.LogAsync(GetUser(), "DELETE", "InstallmentTerm", id.ToString(), "InstallmentTerm deleted", GetIp());
            return NoContent();
        }
    }
}
