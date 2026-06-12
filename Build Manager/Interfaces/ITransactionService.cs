using BuildManager.DTOs;

namespace BuildManager.Interfaces
{
    public interface ITransactionService
    {
        // ── Dashboard / Summary Sheets ────────────────────────────────────────
        Task<CompanySummaryDto> GetCompanySummary(int companyId);
        Task<ClientSummaryDto> GetClientSummary(int clientId);
        Task<IEnumerable<SupplierSummaryDto>> GetSupplierSummaryByClient(int clientId);
        Task<IEnumerable<SubContractorSummaryDto>> GetSubContractorSummaryByClient(int clientId);

        // ── Client Ledger Operations ──────────────────────────────────────────
        Task<IEnumerable<ClientTransactionResponseDto>> GetClientTransactions(int? clientId);
        Task<ClientTransactionResponseDto> GetClientTransactionById(int id);
        Task<ClientTransactionResponseDto> CreateClientTransaction(ClientTransactionRequestDto dto);
        Task<ClientTransactionResponseDto> UpdateClientTransaction(int id, ClientTransactionRequestDto dto);
        Task<bool> DeleteClientTransaction(int id);

        // ── Supplier Ledger Operations ────────────────────────────────────────
        Task<IEnumerable<SupplierTransactionResponseDto>> GetSupplierTransactions(int? clientId, int? supplierId);
        Task<SupplierTransactionResponseDto> GetSupplierTransactionById(int id);
        Task<SupplierTransactionResponseDto> CreateSupplierTransaction(SupplierTransactionRequestDto dto);
        Task<SupplierTransactionResponseDto> UpdateSupplierTransaction(int id, SupplierTransactionRequestDto dto);
        Task<bool> DeleteSupplierTransaction(int id);

        // ── SubContractor Ledger Operations ───────────────────────────────────
        Task<IEnumerable<SubContractorTransactionResponseDto>> GetSubContractorTransactions(int? clientId, int? subContractorId);
        Task<SubContractorTransactionResponseDto> GetSubContractorTransactionById(int id);
        Task<SubContractorTransactionResponseDto> CreateSubContractorTransaction(SubContractorTransactionRequestDto dto);
        Task<SubContractorTransactionResponseDto> UpdateSubContractorTransaction(int id, SubContractorTransactionRequestDto dto);
        Task<bool> DeleteSubContractorTransaction(int id);

        // ── Company Expense Ledger Operations ─────────────────────────────────
        Task<IEnumerable<CompanyExpenseTransactionResponseDto>> GetCompanyExpenseTransactions(int? companyId, int? clientId);
        Task<CompanyExpenseTransactionResponseDto> GetCompanyExpenseTransactionById(int id);
        Task<CompanyExpenseTransactionResponseDto> CreateCompanyExpenseTransaction(CompanyExpenseTransactionRequestDto dto);
        Task<CompanyExpenseTransactionResponseDto> UpdateCompanyExpenseTransaction(int id, CompanyExpenseTransactionRequestDto dto);
        Task<bool> DeleteCompanyExpenseTransaction(int id);
    }
}