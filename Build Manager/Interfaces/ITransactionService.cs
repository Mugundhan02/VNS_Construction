using BuildManager.DTOs;

namespace BuildManager.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<ClientTransactionResponseDto>> GetClientTransactions(int? clientId = null);
        Task<ClientTransactionResponseDto> GetClientTransactionById(int id);
        Task<ClientTransactionResponseDto> CreateClientTransaction(ClientTransactionRequestDto dto);
        Task<ClientTransactionResponseDto> UpdateClientTransaction(int id, ClientTransactionRequestDto dto);
        Task<bool> DeleteClientTransaction(int id);

        Task<IEnumerable<SupplierTransactionResponseDto>> GetSupplierTransactions(int? clientId = null, int? supplierId = null);
        Task<SupplierTransactionResponseDto> GetSupplierTransactionById(int id);
        Task<SupplierTransactionResponseDto> CreateSupplierTransaction(SupplierTransactionRequestDto dto);
        Task<SupplierTransactionResponseDto> UpdateSupplierTransaction(int id, SupplierTransactionRequestDto dto);
        Task<bool> DeleteSupplierTransaction(int id);

        Task<IEnumerable<SubContractorTransactionResponseDto>> GetSubContractorTransactions(int? clientId = null, int? subContractorId = null);
        Task<SubContractorTransactionResponseDto> GetSubContractorTransactionById(int id);
        Task<SubContractorTransactionResponseDto> CreateSubContractorTransaction(SubContractorTransactionRequestDto dto);
        Task<SubContractorTransactionResponseDto> UpdateSubContractorTransaction(int id, SubContractorTransactionRequestDto dto);
        Task<bool> DeleteSubContractorTransaction(int id);

        Task<IEnumerable<CompanyExpenseTransactionResponseDto>> GetCompanyExpenseTransactions(int? companyId = null, int? clientId = null);
        Task<CompanyExpenseTransactionResponseDto> GetCompanyExpenseTransactionById(int id);
        Task<CompanyExpenseTransactionResponseDto> CreateCompanyExpenseTransaction(CompanyExpenseTransactionRequestDto dto);
        Task<CompanyExpenseTransactionResponseDto> UpdateCompanyExpenseTransaction(int id, CompanyExpenseTransactionRequestDto dto);
        Task<bool> DeleteCompanyExpenseTransaction(int id);

        Task<CompanySummaryDto> GetCompanySummary(int companyId);
        Task<ClientSummaryDto> GetClientSummary(int clientId);
        Task<IEnumerable<SupplierSummaryDto>> GetSupplierSummaryByClient(int clientId);
        Task<IEnumerable<SubContractorSummaryDto>> GetSubContractorSummaryByClient(int clientId);
    }
}
