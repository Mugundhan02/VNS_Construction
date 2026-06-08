using BuildManager.DTOs;

namespace BuildManager.Interfaces
{
    public interface ILookupService
    {
        Task<IEnumerable<PaymentTypeResponseDto>> GetAllPaymentTypes();
        Task<PaymentTypeResponseDto> CreatePaymentType(PaymentTypeRequestDto dto);
        Task<PaymentTypeResponseDto> UpdatePaymentType(int id, PaymentTypeRequestDto dto);
        Task<bool> DeletePaymentType(int id);

        Task<IEnumerable<WhomResponseDto>> GetAllWhom();
        Task<WhomResponseDto> CreateWhom(WhomRequestDto dto);
        Task<WhomResponseDto> UpdateWhom(int id, WhomRequestDto dto);
        Task<bool> DeleteWhom(int id);

        Task<IEnumerable<OfficeExpenseResponseDto>> GetAllOfficeExpenses();
        Task<OfficeExpenseResponseDto> CreateOfficeExpense(OfficeExpenseRequestDto dto);
        Task<OfficeExpenseResponseDto> UpdateOfficeExpense(int id, OfficeExpenseRequestDto dto);
        Task<bool> DeleteOfficeExpense(int id);

        Task<IEnumerable<CompanyBankResponseDto>> GetBanksByCompany(int companyId);
        Task<CompanyBankResponseDto> CreateCompanyBank(CompanyBankRequestDto dto);
        Task<CompanyBankResponseDto> UpdateCompanyBank(int id, CompanyBankRequestDto dto);
        Task<bool> DeleteCompanyBank(int id);

        Task<IEnumerable<InstallmentTermResponseDto>> GetAllInstallmentTerms();
        Task<InstallmentTermResponseDto> CreateInstallmentTerm(InstallmentTermRequestDto dto);
        Task<InstallmentTermResponseDto> UpdateInstallmentTerm(int id, InstallmentTermRequestDto dto);
        Task<bool> DeleteInstallmentTerm(int id);
    }
}
