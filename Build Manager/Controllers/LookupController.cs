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
    public class LookupController : ControllerBase
    {
        private readonly ILookupService _lookupService;
        private readonly IAuditLogService _auditLog;

        public LookupController(ILookupService lookupService, IAuditLogService auditLog)
        {
            _lookupService = lookupService;
            _auditLog = auditLog;
        }

        private string GetUsername() => User.Identity?.Name ?? "unknown";
        private string? GetIp() => HttpContext.Connection.RemoteIpAddress?.ToString();

        // ── Payment Types ─────────────────────────────────────────────────────

        [HttpGet("payment-types")]
        [Authorize(Roles = "User,Admin,Owner")]
        public async Task<ActionResult<IEnumerable<PaymentTypeResponseDto>>> GetPaymentTypes()
        {
            var result = await _lookupService.GetAllPaymentTypes();
            return Ok(result);
        }

        [HttpPost("payment-types")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<PaymentTypeResponseDto>> CreatePaymentType([FromBody] PaymentTypeRequestDto dto)
        {
            var username = GetUsername();
            var result = await _lookupService.CreatePaymentType(dto);

            await _auditLog.LogAsync(username, "CREATE", "PaymentType", result.PaymentTypeId.ToString(),
                "Created billing taxonomy parameter", GetIp());

            return Ok(result);
        }

        [HttpPut("payment-types/{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<PaymentTypeResponseDto>> UpdatePaymentType(int id, [FromBody] PaymentTypeRequestDto dto)
        {
            var username = GetUsername();
            var result = await _lookupService.UpdatePaymentType(id, dto);

            await _auditLog.LogAsync(username, "UPDATE", "PaymentType", id.ToString(),
                $"Updated payment type configuration ID {id}", GetIp());

            return Ok(result);
        }

        [HttpDelete("payment-types/{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> DeletePaymentType(int id)
        {
            var username = GetUsername();
            await _lookupService.DeletePaymentType(id);

            await _auditLog.LogAsync(username, "DELETE", "PaymentType", id.ToString(),
                $"Removed payment option configurations entry ID {id}", GetIp());

            return NoContent();
        }

        // ── Whom ──────────────────────────────────────────────────────────────

        [HttpGet("whoms")]
        [Authorize(Roles = "User,Admin,Owner")]
        public async Task<ActionResult<IEnumerable<WhomResponseDto>>> GetWhoms()
        {
            var result = await _lookupService.GetAllWhom();
            return Ok(result);
        }

        [HttpPost("whoms")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<WhomResponseDto>> CreateWhom([FromBody] WhomRequestDto dto)
        {
            var username = GetUsername();
            var result = await _lookupService.CreateWhom(dto);

            await _auditLog.LogAsync(username, "CREATE", "Whom", result.WhomId.ToString(),
                "Configured tracking party assignment identifier", GetIp());

            return Ok(result);
        }

        [HttpPut("whoms/{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<WhomResponseDto>> UpdateWhom(int id, [FromBody] WhomRequestDto dto)
        {
            var username = GetUsername();
            var result = await _lookupService.UpdateWhom(id, dto);

            await _auditLog.LogAsync(username, "UPDATE", "Whom", id.ToString(),
                $"Modified entity trace configurations for ID {id}", GetIp());

            return Ok(result);
        }

        [HttpDelete("whoms/{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> DeleteWhom(int id)
        {
            var username = GetUsername();
            await _lookupService.DeleteWhom(id);

            await _auditLog.LogAsync(username, "DELETE", "Whom", id.ToString(),
                $"Cleared dynamic identity index record trace ID {id}", GetIp());

            return NoContent();
        }

        // ── Office Expenses ───────────────────────────────────────────────────

        [HttpGet("office-expenses")]
        [Authorize(Roles = "User,Admin,Owner")]
        public async Task<ActionResult<IEnumerable<OfficeExpenseResponseDto>>> GetOfficeExpenses()
        {
            var result = await _lookupService.GetAllOfficeExpenses();
            return Ok(result);
        }

        [HttpPost("office-expenses")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<OfficeExpenseResponseDto>> CreateOfficeExpense([FromBody] OfficeExpenseRequestDto dto)
        {
            var username = GetUsername();
            var result = await _lookupService.CreateOfficeExpense(dto);

            await _auditLog.LogAsync(username, "CREATE", "OfficeExpense", result.OfficeExpenseId.ToString(),
                "Logged corporate ledger expense variable", GetIp());

            return Ok(result);
        }

        [HttpPut("office-expenses/{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<OfficeExpenseResponseDto>> UpdateOfficeExpense(int id, [FromBody] OfficeExpenseRequestDto dto)
        {
            var username = GetUsername();
            var result = await _lookupService.UpdateOfficeExpense(id, dto);

            await _auditLog.LogAsync(username, "UPDATE", "OfficeExpense", id.ToString(),
                $"Adjusted cost parameters for asset profile catalog ID {id}", GetIp());

            return Ok(result);
        }

        [HttpDelete("office-expenses/{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> DeleteOfficeExpense(int id)
        {
            var username = GetUsername();
            await _lookupService.DeleteOfficeExpense(id);

            await _auditLog.LogAsync(username, "DELETE", "OfficeExpense", id.ToString(),
                $"Purged budget category metrics ID {id}", GetIp());

            return NoContent();
        }

        // ── Company Banks ─────────────────────────────────────────────────────

        [HttpGet("banks/company/{companyId:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<IEnumerable<CompanyBankResponseDto>>> GetBanksByCompany(int companyId)
        {
            var result = await _lookupService.GetBanksByCompany(companyId);
            return Ok(result);
        }

        [HttpPost("banks")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult<CompanyBankResponseDto>> CreateBank([FromBody] CompanyBankRequestDto dto)
        {
            var username = GetUsername();
            var result = await _lookupService.CreateCompanyBank(dto);

            await _auditLog.LogAsync(username, "CREATE", "CompanyBank", result.CompanyBankId.ToString(),
                "Registered commercial banking stream link", GetIp());

            return Ok(result);
        }

        [HttpPut("banks/{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult<CompanyBankResponseDto>> UpdateBank(int id, [FromBody] CompanyBankRequestDto dto)
        {
            var username = GetUsername();
            var result = await _lookupService.UpdateCompanyBank(id, dto);

            await _auditLog.LogAsync(username, "UPDATE", "CompanyBank", id.ToString(),
                $"Modified account reconciliation configuration profiles for bank entry ID {id}", GetIp());

            return Ok(result);
        }

        [HttpDelete("banks/{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> DeleteBank(int id)
        {
            var username = GetUsername();
            await _lookupService.DeleteCompanyBank(id);

            await _auditLog.LogAsync(username, "DELETE", "CompanyBank", id.ToString(),
                $"Deactivated commercial tracking banking line node ID {id}", GetIp());

            return NoContent();
        }

        // ── Installment Terms ─────────────────────────────────────────────────

        [HttpGet("installment-terms")]
        [Authorize(Roles = "User,Admin,Owner")]
        public async Task<ActionResult<IEnumerable<InstallmentTermResponseDto>>> GetInstallmentTerms()
        {
            var result = await _lookupService.GetAllInstallmentTerms();
            return Ok(result);
        }

        [HttpPost("installment-terms")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<InstallmentTermResponseDto>> CreateInstallmentTerm([FromBody] InstallmentTermRequestDto dto)
        {
            var username = GetUsername();
            var result = await _lookupService.CreateInstallmentTerm(dto);

            await _auditLog.LogAsync(username, "CREATE", "InstallmentTerm", result.InstallmentTermId.ToString(),
                "Logged contract payment milestone criteria", GetIp());

            return Ok(result);
        }

        [HttpPut("installment-terms/{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<InstallmentTermResponseDto>> UpdateInstallmentTerm(int id, [FromBody] InstallmentTermRequestDto dto)
        {
            var username = GetUsername();
            var result = await _lookupService.UpdateInstallmentTerm(id, dto);

            await _auditLog.LogAsync(username, "UPDATE", "InstallmentTerm", id.ToString(),
                $"Updated contract installment criteria index parameters for tracking node ID {id}", GetIp());

            return Ok(result);
        }

        [HttpDelete("installment-terms/{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> DeleteInstallmentTerm(int id)
        {
            var username = GetUsername();
            await _lookupService.DeleteInstallmentTerm(id);

            await _auditLog.LogAsync(username, "DELETE", "InstallmentTerm", id.ToString(),
                $"Removed scheduled contract execution sequence tracker ID {id}", GetIp());

            return NoContent();
        }
    }
}