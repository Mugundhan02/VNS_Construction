using BuildManager.DTOs;

namespace BuildManager.Services.Interfaces
{
    public interface ILookupService
    {
        // ── Payment Types ────────────────────────────────────────────────────
        Task<IEnumerable<PaymentTypeResponseDto>> GetAllPaymentTypesAsync();
        Task<PaymentTypeResponseDto> CreatePaymentTypeAsync(PaymentTypeRequestDto dto);
        Task<PaymentTypeResponseDto?> UpdatePaymentTypeAsync(int id, PaymentTypeRequestDto dto);
        Task<bool> DeletePaymentTypeAsync(int id);

        // ── Whom ─────────────────────────────────────────────────────────────
        Task<IEnumerable<WhomResponseDto>> GetAllWhomAsync();
        Task<WhomResponseDto> CreateWhomAsync(WhomRequestDto dto);
        Task<WhomResponseDto?> UpdateWhomAsync(int id, WhomRequestDto dto);
        Task<bool> DeleteWhomAsync(int id);

        // ── Office Expenses ───────────────────────────────────────────────────
        Task<IEnumerable<OfficeExpenseResponseDto>> GetAllOfficeExpensesAsync();
        Task<OfficeExpenseResponseDto> CreateOfficeExpenseAsync(OfficeExpenseRequestDto dto);
        Task<OfficeExpenseResponseDto?> UpdateOfficeExpenseAsync(int id, OfficeExpenseRequestDto dto);
        Task<bool> DeleteOfficeExpenseAsync(int id);

        // ── Company Banks ─────────────────────────────────────────────────────
        Task<IEnumerable<CompanyBankResponseDto>> GetBanksByCompanyAsync(int companyId);
        Task<CompanyBankResponseDto> CreateCompanyBankAsync(CompanyBankRequestDto dto);
        Task<CompanyBankResponseDto?> UpdateCompanyBankAsync(int id, CompanyBankRequestDto dto);
        Task<bool> DeleteCompanyBankAsync(int id);

        // ── Installment Terms ─────────────────────────────────────────────────
        Task<IEnumerable<InstallmentTermResponseDto>> GetAllInstallmentTermsAsync();
        Task<InstallmentTermResponseDto> CreateInstallmentTermAsync(InstallmentTermRequestDto dto);
        Task<InstallmentTermResponseDto?> UpdateInstallmentTermAsync(int id, InstallmentTermRequestDto dto);
        Task<bool> DeleteInstallmentTermAsync(int id);
    }
}
