using BuildManager.DTOs;

namespace BuildManager.Services.Interfaces
{
    public interface ITransactionService
    {
        // Client Transactions
        Task<IEnumerable<ClientTransactionResponseDto>> GetClientTransactionsAsync(int? clientId = null);
        Task<ClientTransactionResponseDto?> GetClientTransactionByIdAsync(int id);
        Task<ClientTransactionResponseDto> CreateClientTransactionAsync(ClientTransactionRequestDto dto);
        Task<ClientTransactionResponseDto?> UpdateClientTransactionAsync(int id, ClientTransactionRequestDto dto);
        Task<bool> DeleteClientTransactionAsync(int id);

        // Supplier Transactions
        Task<IEnumerable<SupplierTransactionResponseDto>> GetSupplierTransactionsAsync(int? clientId = null, int? supplierId = null);
        Task<SupplierTransactionResponseDto?> GetSupplierTransactionByIdAsync(int id);
        Task<SupplierTransactionResponseDto> CreateSupplierTransactionAsync(SupplierTransactionRequestDto dto);
        Task<SupplierTransactionResponseDto?> UpdateSupplierTransactionAsync(int id, SupplierTransactionRequestDto dto);
        Task<bool> DeleteSupplierTransactionAsync(int id);

        // SubContractor Transactions
        Task<IEnumerable<SubContractorTransactionResponseDto>> GetSubContractorTransactionsAsync(int? clientId = null, int? subContractorId = null);
        Task<SubContractorTransactionResponseDto?> GetSubContractorTransactionByIdAsync(int id);
        Task<SubContractorTransactionResponseDto> CreateSubContractorTransactionAsync(SubContractorTransactionRequestDto dto);
        Task<SubContractorTransactionResponseDto?> UpdateSubContractorTransactionAsync(int id, SubContractorTransactionRequestDto dto);
        Task<bool> DeleteSubContractorTransactionAsync(int id);

        // Company Expense Transactions
        Task<IEnumerable<CompanyExpenseTransactionResponseDto>> GetCompanyExpenseTransactionsAsync(int? companyId = null, int? clientId = null);
        Task<CompanyExpenseTransactionResponseDto?> GetCompanyExpenseTransactionByIdAsync(int id);
        Task<CompanyExpenseTransactionResponseDto> CreateCompanyExpenseTransactionAsync(CompanyExpenseTransactionRequestDto dto);
        Task<CompanyExpenseTransactionResponseDto?> UpdateCompanyExpenseTransactionAsync(int id, CompanyExpenseTransactionRequestDto dto);
        Task<bool> DeleteCompanyExpenseTransactionAsync(int id);

        // Summary / Dashboard (mirrors the "New Transaction" summary screen)
        Task<CompanySummaryDto> GetCompanySummaryAsync(int companyId);
        Task<ClientSummaryDto?> GetClientSummaryAsync(int clientId);
        Task<IEnumerable<SupplierSummaryDto>> GetSupplierSummaryByClientAsync(int clientId);
        Task<IEnumerable<SubContractorSummaryDto>> GetSubContractorSummaryByClientAsync(int clientId);
    }
}
