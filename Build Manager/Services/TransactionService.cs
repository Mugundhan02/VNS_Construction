using AutoMapper;
using BuildManager.Contexts;
using BuildManager.DTOs;
using BuildManager.Exceptions;
using BuildManager.Interfaces;
using BuildManager.Models;
using Microsoft.EntityFrameworkCore;

namespace BuildManager.Services
{
    // Refactored to use Primary Constructor syntax (Fixes IDE0290)
    public class TransactionService(BuildManagerDbContext context, IMapper mapper) : ITransactionService
    {
        private readonly BuildManagerDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        // ── Client Transactions ──────────────────────────────────────────────

        public async Task<IEnumerable<ClientTransactionResponseDto>> GetClientTransactions(int? clientId = null)
        {
            var query = _context.ClientTransactions
                .AsNoTracking()
                .Include(t => t.Client)
                .Include(t => t.PaymentType)
                .Include(t => t.ByWhom)
                .AsQueryable();

            if (clientId.HasValue)
                query = query.Where(t => t.ClientId == clientId.Value);

            var list = await query
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ClientTransactionResponseDto>>(list);
        }

        public async Task<ClientTransactionResponseDto> GetClientTransactionById(int id)
        {
            var entity = await _context.ClientTransactions
                .AsNoTracking()
                .Include(t => t.Client)
                .Include(t => t.PaymentType)
                .Include(t => t.ByWhom)
                .FirstOrDefaultAsync(t => t.ClientTransactionId == id)
                ?? throw new EntityNotFoundException("ClientTransaction", id);

            return _mapper.Map<ClientTransactionResponseDto>(entity);
        }

        public async Task<ClientTransactionResponseDto> CreateClientTransaction(ClientTransactionRequestDto dto)
        {
            var entity = _mapper.Map<ClientTransaction>(dto);
            _context.ClientTransactions.Add(entity);
            await _context.SaveChangesAsync();

            await LoadClientTransactionNavigations(entity);
            return _mapper.Map<ClientTransactionResponseDto>(entity);
        }

        public async Task<ClientTransactionResponseDto> UpdateClientTransaction(int id, ClientTransactionRequestDto dto)
        {
            var entity = await _context.ClientTransactions.FindAsync(id)
                         ?? throw new EntityNotFoundException("ClientTransaction", id);

            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();

            await LoadClientTransactionNavigations(entity);
            return _mapper.Map<ClientTransactionResponseDto>(entity);
        }

        public async Task<bool> DeleteClientTransaction(int id)
        {
            var entity = await _context.ClientTransactions.FindAsync(id);
            if (entity is null) return false;
            _context.ClientTransactions.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ── Supplier Transactions ────────────────────────────────────────────

        public async Task<IEnumerable<SupplierTransactionResponseDto>> GetSupplierTransactions(
            int? clientId = null, int? supplierId = null)
        {
            var query = _context.SupplierTransactions
                .AsNoTracking()
                .Include(t => t.Client)
                .Include(t => t.Supplier)
                .Include(t => t.Material)
                .Include(t => t.PaymentType)
                .Include(t => t.ToWhom)
                .AsQueryable();

            if (clientId.HasValue)
                query = query.Where(t => t.ClientId == clientId.Value);

            if (supplierId.HasValue)
                query = query.Where(t => t.SupplierId == supplierId.Value);

            var list = await query
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();

            return _mapper.Map<IEnumerable<SupplierTransactionResponseDto>>(list);
        }

        public async Task<SupplierTransactionResponseDto> GetSupplierTransactionById(int id)
        {
            var entity = await _context.SupplierTransactions
                .AsNoTracking()
                .Include(t => t.Client)
                .Include(t => t.Supplier)
                .Include(t => t.Material)
                .Include(t => t.PaymentType)
                .Include(t => t.ToWhom)
                .FirstOrDefaultAsync(t => t.SupplierTransactionId == id)
                ?? throw new EntityNotFoundException("SupplierTransaction", id);

            return _mapper.Map<SupplierTransactionResponseDto>(entity);
        }

        public async Task<SupplierTransactionResponseDto> CreateSupplierTransaction(SupplierTransactionRequestDto dto)
        {
            var entity = _mapper.Map<SupplierTransaction>(dto);
            _context.SupplierTransactions.Add(entity);
            await _context.SaveChangesAsync();

            await LoadSupplierTransactionNavigations(entity);
            return _mapper.Map<SupplierTransactionResponseDto>(entity);
        }

        public async Task<SupplierTransactionResponseDto> UpdateSupplierTransaction(int id, SupplierTransactionRequestDto dto)
        {
            var entity = await _context.SupplierTransactions.FindAsync(id)
                         ?? throw new EntityNotFoundException("SupplierTransaction", id);

            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();

            await LoadSupplierTransactionNavigations(entity);
            return _mapper.Map<SupplierTransactionResponseDto>(entity);
        }

        public async Task<bool> DeleteSupplierTransaction(int id)
        {
            var entity = await _context.SupplierTransactions.FindAsync(id);
            if (entity is null) return false;
            _context.SupplierTransactions.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ── SubContractor Transactions ───────────────────────────────────────

        public async Task<IEnumerable<SubContractorTransactionResponseDto>> GetSubContractorTransactions(
            int? clientId = null, int? subContractorId = null)
        {
            var query = _context.SubContractorTransactions
                .AsNoTracking()
                .Include(t => t.Client)
                .Include(t => t.SubContractor)
                .Include(t => t.JobWork)
                .Include(t => t.PaymentType)
                .Include(t => t.ToWhom)
                .AsQueryable();

            if (clientId.HasValue)
                query = query.Where(t => t.ClientId == clientId.Value);

            if (subContractorId.HasValue)
                query = query.Where(t => t.SubContractorId == subContractorId.Value);

            var list = await query
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();

            return _mapper.Map<IEnumerable<SubContractorTransactionResponseDto>>(list);
        }

        public async Task<SubContractorTransactionResponseDto> GetSubContractorTransactionById(int id)
        {
            var entity = await _context.SubContractorTransactions
                .AsNoTracking()
                .Include(t => t.Client)
                .Include(t => t.SubContractor)
                .Include(t => t.JobWork)
                .Include(t => t.PaymentType)
                .Include(t => t.ToWhom)
                .FirstOrDefaultAsync(t => t.SubContractorTransactionId == id)
                ?? throw new EntityNotFoundException("SubContractorTransaction", id);

            return _mapper.Map<SubContractorTransactionResponseDto>(entity);
        }

        public async Task<SubContractorTransactionResponseDto> CreateSubContractorTransaction(SubContractorTransactionRequestDto dto)
        {
            var entity = _mapper.Map<SubContractorTransaction>(dto);
            _context.SubContractorTransactions.Add(entity);
            await _context.SaveChangesAsync();

            await LoadSubContractorTransactionNavigations(entity);
            return _mapper.Map<SubContractorTransactionResponseDto>(entity);
        }

        public async Task<SubContractorTransactionResponseDto> UpdateSubContractorTransaction(int id, SubContractorTransactionRequestDto dto)
        {
            var entity = await _context.SubContractorTransactions.FindAsync(id)
                         ?? throw new EntityNotFoundException("SubContractorTransaction", id);

            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();

            await LoadSubContractorTransactionNavigations(entity);
            return _mapper.Map<SubContractorTransactionResponseDto>(entity);
        }

        public async Task<bool> DeleteSubContractorTransaction(int id)
        {
            var entity = await _context.SubContractorTransactions.FindAsync(id);
            if (entity is null) return false;
            _context.SubContractorTransactions.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ── Company Expense Transactions ─────────────────────────────────────

        public async Task<IEnumerable<CompanyExpenseTransactionResponseDto>> GetCompanyExpenseTransactions(
            int? companyId = null, int? clientId = null)
        {
            var query = _context.CompanyExpenseTransactions
                .AsNoTracking()
                .Include(t => t.Client)
                .Include(t => t.Company)
                .Include(t => t.OfficeExpense)
                .Include(t => t.PaymentType)
                .Include(t => t.ToWhom)
                .AsQueryable();

            if (companyId.HasValue)
                query = query.Where(t => t.CompanyId == companyId.Value);

            if (clientId.HasValue)
                query = query.Where(t => t.ClientId == clientId.Value);

            var list = await query
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CompanyExpenseTransactionResponseDto>>(list);
        }

        public async Task<CompanyExpenseTransactionResponseDto> GetCompanyExpenseTransactionById(int id)
        {
            var entity = await _context.CompanyExpenseTransactions
                .AsNoTracking()
                .Include(t => t.Client)
                .Include(t => t.Company)
                .Include(t => t.OfficeExpense)
                .Include(t => t.PaymentType)
                .Include(t => t.ToWhom)
                .FirstOrDefaultAsync(t => t.CompanyExpenseTransactionId == id)
                ?? throw new EntityNotFoundException("CompanyExpenseTransaction", id);

            return _mapper.Map<CompanyExpenseTransactionResponseDto>(entity);
        }

        public async Task<CompanyExpenseTransactionResponseDto> CreateCompanyExpenseTransaction(CompanyExpenseTransactionRequestDto dto)
        {
            var entity = _mapper.Map<CompanyExpenseTransaction>(dto);
            _context.CompanyExpenseTransactions.Add(entity);
            await _context.SaveChangesAsync();

            await LoadExpenseTransactionNavigations(entity);
            return _mapper.Map<CompanyExpenseTransactionResponseDto>(entity);
        }

        public async Task<CompanyExpenseTransactionResponseDto> UpdateCompanyExpenseTransaction(int id, CompanyExpenseTransactionRequestDto dto)
        {
            var entity = await _context.CompanyExpenseTransactions.FindAsync(id)
                         ?? throw new EntityNotFoundException("CompanyExpenseTransaction", id);

            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();

            await LoadExpenseTransactionNavigations(entity);
            return _mapper.Map<CompanyExpenseTransactionResponseDto>(entity);
        }

        public async Task<bool> DeleteCompanyExpenseTransaction(int id)
        {
            var entity = await _context.CompanyExpenseTransactions.FindAsync(id);
            if (entity is null) return false;
            _context.CompanyExpenseTransactions.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ── Summary / Dashboard ──────────────────────────────────────────────

        public async Task<CompanySummaryDto> GetCompanySummary(int companyId)
        {
            var company = await _context.Companies
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CompanyId == companyId);

            var totalCredits = await _context.ClientTransactions
                .Where(t => _context.Clients
                    .Where(c => c.ClientId == t.ClientId)
                    .Any())
                .SumAsync(t => t.CreditAmount);

            var totalDebits = await _context.ClientTransactions
                .SumAsync(t => t.DebitAmount);

            return new CompanySummaryDto
            {
                CompanyName = company?.CompanyName ?? string.Empty,
                CreditsAmount = totalCredits,
                DebitsAmount = totalDebits,
                BalanceAmount = totalCredits - totalDebits
            };
        }

        public async Task<ClientSummaryDto> GetClientSummary(int clientId)
        {
            var client = await _context.Clients
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ClientId == clientId)
                ?? throw new EntityNotFoundException("Client", clientId);

            var credits = await _context.ClientTransactions.Where(t => t.ClientId == clientId).SumAsync(t => t.CreditAmount);
            var debits = await _context.ClientTransactions.Where(t => t.ClientId == clientId).SumAsync(t => t.DebitAmount);
            var received = await _context.SupplierTransactions.Where(t => t.ClientId == clientId).SumAsync(t => t.PaidAmount);
            var expenses = await _context.SubContractorTransactions.Where(t => t.ClientId == clientId).SumAsync(t => t.PaidAmount);

            return new ClientSummaryDto
            {
                ClientId = client.ClientId,
                ClientName = client.ClientName,
                CreditsAmount = credits,
                DebitsAmount = debits,
                BalanceAmount = credits - debits,
                // Fixed to match Option A's clean fields: .Unit, .Rate, and .Amount
                EstimateUnits = client.EstimateDetails?.Unit ?? 0,
                EstimateRate = client.EstimateDetails?.Rate ?? 0,
                EstimateAmount = client.EstimateDetails?.Amount ?? 0,
                EstimateAmountReceived = received,
                EstimateAmountExpenses = expenses
            };
        }

        public async Task<IEnumerable<SupplierSummaryDto>> GetSupplierSummaryByClient(int clientId)
        {
            return await _context.SupplierTransactions
                .AsNoTracking()
                .Where(t => t.ClientId == clientId)
                .GroupBy(t => new { t.SupplierId, t.Supplier.SupplierName })
                .Select(g => new SupplierSummaryDto
                {
                    SupplierId = g.Key.SupplierId,
                    SupplierName = g.Key.SupplierName,
                    PayableAmount = g.Sum(t => t.Amount),
                    PaidAmount = g.Sum(t => t.PaidAmount),
                    BalanceAmount = g.Sum(t => t.Amount) - g.Sum(t => t.PaidAmount)
                })
                .OrderBy(s => s.SupplierName)
                .ToListAsync();
        }

        public async Task<IEnumerable<SubContractorSummaryDto>> GetSubContractorSummaryByClient(int clientId)
        {
            return await _context.SubContractorTransactions
                .AsNoTracking()
                .Where(t => t.ClientId == clientId)
                .GroupBy(t => new { t.SubContractorId, t.SubContractor.SubContractorName })
                .Select(g => new SubContractorSummaryDto
                {
                    SubContractorId = g.Key.SubContractorId,
                    SubContractorName = g.Key.SubContractorName,
                    PayableAmount = g.Sum(t => t.Amount),
                    PaidAmount = g.Sum(t => t.PaidAmount),
                    BalanceAmount = g.Sum(t => t.Amount) - g.Sum(t => t.PaidAmount)
                })
                .OrderBy(s => s.SubContractorName)
                .ToListAsync();
        }

        // ── Private Navigation Loaders ───────────────────────────────────────

        private async Task LoadClientTransactionNavigations(ClientTransaction entity)
        {
            await _context.Entry(entity).Reference(t => t.Client).LoadAsync();
            await _context.Entry(entity).Reference(t => t.PaymentType).LoadAsync();
            await _context.Entry(entity).Reference(t => t.ByWhom).LoadAsync();
        }

        private async Task LoadSupplierTransactionNavigations(SupplierTransaction entity)
        {
            await _context.Entry(entity).Reference(t => t.Client).LoadAsync();
            await _context.Entry(entity).Reference(t => t.Supplier).LoadAsync();
            await _context.Entry(entity).Reference(t => t.Material).LoadAsync();
            await _context.Entry(entity).Reference(t => t.PaymentType).LoadAsync();
            await _context.Entry(entity).Reference(t => t.ToWhom).LoadAsync();
        }

        private async Task LoadSubContractorTransactionNavigations(SubContractorTransaction entity)
        {
            await _context.Entry(entity).Reference(t => t.Client).LoadAsync();
            await _context.Entry(entity).Reference(t => t.SubContractor).LoadAsync();
            await _context.Entry(entity).Reference(t => t.JobWork).LoadAsync();
            await _context.Entry(entity).Reference(t => t.PaymentType).LoadAsync();
            await _context.Entry(entity).Reference(t => t.ToWhom).LoadAsync();
        }

        private async Task LoadExpenseTransactionNavigations(CompanyExpenseTransaction entity)
        {
            await _context.Entry(entity).Reference(t => t.Client).LoadAsync();
            await _context.Entry(entity).Reference(t => t.Company).LoadAsync();
            await _context.Entry(entity).Reference(t => t.OfficeExpense).LoadAsync();
            await _context.Entry(entity).Reference(t => t.PaymentType).LoadAsync();
            await _context.Entry(entity).Reference(t => t.ToWhom).LoadAsync();
        }
    }
}