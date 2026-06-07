using AutoMapper;
using BuildManager.Data;
using BuildManager.DTOs;
using BuildManager.Models;
using BuildManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuildManager.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly BuildManagerDbContext _context;
        private readonly IMapper _mapper;

        public TransactionService(BuildManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper  = mapper;
        }

        // ── Client Transactions ──────────────────────────────────────────────

        public async Task<IEnumerable<ClientTransactionResponseDto>> GetClientTransactionsAsync(int? clientId = null)
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

        public async Task<ClientTransactionResponseDto?> GetClientTransactionByIdAsync(int id)
        {
            var entity = await _context.ClientTransactions
                .AsNoTracking()
                .Include(t => t.Client)
                .Include(t => t.PaymentType)
                .Include(t => t.ByWhom)
                .FirstOrDefaultAsync(t => t.ClientTransactionId == id);

            return entity is null ? null : _mapper.Map<ClientTransactionResponseDto>(entity);
        }

        public async Task<ClientTransactionResponseDto> CreateClientTransactionAsync(ClientTransactionRequestDto dto)
        {
            var entity = _mapper.Map<ClientTransaction>(dto);
            _context.ClientTransactions.Add(entity);
            await _context.SaveChangesAsync();

            await LoadClientTransactionNavigations(entity);
            return _mapper.Map<ClientTransactionResponseDto>(entity);
        }

        public async Task<ClientTransactionResponseDto?> UpdateClientTransactionAsync(int id, ClientTransactionRequestDto dto)
        {
            var entity = await _context.ClientTransactions.FindAsync(id);
            if (entity is null) return null;

            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();

            await LoadClientTransactionNavigations(entity);
            return _mapper.Map<ClientTransactionResponseDto>(entity);
        }

        public async Task<bool> DeleteClientTransactionAsync(int id)
        {
            var entity = await _context.ClientTransactions.FindAsync(id);
            if (entity is null) return false;
            _context.ClientTransactions.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ── Supplier Transactions ────────────────────────────────────────────

        public async Task<IEnumerable<SupplierTransactionResponseDto>> GetSupplierTransactionsAsync(
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

        public async Task<SupplierTransactionResponseDto?> GetSupplierTransactionByIdAsync(int id)
        {
            var entity = await _context.SupplierTransactions
                .AsNoTracking()
                .Include(t => t.Client)
                .Include(t => t.Supplier)
                .Include(t => t.Material)
                .Include(t => t.PaymentType)
                .Include(t => t.ToWhom)
                .FirstOrDefaultAsync(t => t.SupplierTransactionId == id);

            return entity is null ? null : _mapper.Map<SupplierTransactionResponseDto>(entity);
        }

        public async Task<SupplierTransactionResponseDto> CreateSupplierTransactionAsync(SupplierTransactionRequestDto dto)
        {
            var entity = _mapper.Map<SupplierTransaction>(dto);
            _context.SupplierTransactions.Add(entity);
            await _context.SaveChangesAsync();

            await LoadSupplierTransactionNavigations(entity);
            return _mapper.Map<SupplierTransactionResponseDto>(entity);
        }

        public async Task<SupplierTransactionResponseDto?> UpdateSupplierTransactionAsync(int id, SupplierTransactionRequestDto dto)
        {
            var entity = await _context.SupplierTransactions.FindAsync(id);
            if (entity is null) return null;

            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();

            await LoadSupplierTransactionNavigations(entity);
            return _mapper.Map<SupplierTransactionResponseDto>(entity);
        }

        public async Task<bool> DeleteSupplierTransactionAsync(int id)
        {
            var entity = await _context.SupplierTransactions.FindAsync(id);
            if (entity is null) return false;
            _context.SupplierTransactions.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ── SubContractor Transactions ───────────────────────────────────────

        public async Task<IEnumerable<SubContractorTransactionResponseDto>> GetSubContractorTransactionsAsync(
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

        public async Task<SubContractorTransactionResponseDto?> GetSubContractorTransactionByIdAsync(int id)
        {
            var entity = await _context.SubContractorTransactions
                .AsNoTracking()
                .Include(t => t.Client)
                .Include(t => t.SubContractor)
                .Include(t => t.JobWork)
                .Include(t => t.PaymentType)
                .Include(t => t.ToWhom)
                .FirstOrDefaultAsync(t => t.SubContractorTransactionId == id);

            return entity is null ? null : _mapper.Map<SubContractorTransactionResponseDto>(entity);
        }

        public async Task<SubContractorTransactionResponseDto> CreateSubContractorTransactionAsync(SubContractorTransactionRequestDto dto)
        {
            var entity = _mapper.Map<SubContractorTransaction>(dto);
            _context.SubContractorTransactions.Add(entity);
            await _context.SaveChangesAsync();

            await LoadSubContractorTransactionNavigations(entity);
            return _mapper.Map<SubContractorTransactionResponseDto>(entity);
        }

        public async Task<SubContractorTransactionResponseDto?> UpdateSubContractorTransactionAsync(int id, SubContractorTransactionRequestDto dto)
        {
            var entity = await _context.SubContractorTransactions.FindAsync(id);
            if (entity is null) return null;

            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();

            await LoadSubContractorTransactionNavigations(entity);
            return _mapper.Map<SubContractorTransactionResponseDto>(entity);
        }

        public async Task<bool> DeleteSubContractorTransactionAsync(int id)
        {
            var entity = await _context.SubContractorTransactions.FindAsync(id);
            if (entity is null) return false;
            _context.SubContractorTransactions.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ── Company Expense Transactions ─────────────────────────────────────

        public async Task<IEnumerable<CompanyExpenseTransactionResponseDto>> GetCompanyExpenseTransactionsAsync(
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

        public async Task<CompanyExpenseTransactionResponseDto?> GetCompanyExpenseTransactionByIdAsync(int id)
        {
            var entity = await _context.CompanyExpenseTransactions
                .AsNoTracking()
                .Include(t => t.Client)
                .Include(t => t.Company)
                .Include(t => t.OfficeExpense)
                .Include(t => t.PaymentType)
                .Include(t => t.ToWhom)
                .FirstOrDefaultAsync(t => t.CompanyExpenseTransactionId == id);

            return entity is null ? null : _mapper.Map<CompanyExpenseTransactionResponseDto>(entity);
        }

        public async Task<CompanyExpenseTransactionResponseDto> CreateCompanyExpenseTransactionAsync(CompanyExpenseTransactionRequestDto dto)
        {
            var entity = _mapper.Map<CompanyExpenseTransaction>(dto);
            _context.CompanyExpenseTransactions.Add(entity);
            await _context.SaveChangesAsync();

            await LoadExpenseTransactionNavigations(entity);
            return _mapper.Map<CompanyExpenseTransactionResponseDto>(entity);
        }

        public async Task<CompanyExpenseTransactionResponseDto?> UpdateCompanyExpenseTransactionAsync(int id, CompanyExpenseTransactionRequestDto dto)
        {
            var entity = await _context.CompanyExpenseTransactions.FindAsync(id);
            if (entity is null) return null;

            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();

            await LoadExpenseTransactionNavigations(entity);
            return _mapper.Map<CompanyExpenseTransactionResponseDto>(entity);
        }

        public async Task<bool> DeleteCompanyExpenseTransactionAsync(int id)
        {
            var entity = await _context.CompanyExpenseTransactions.FindAsync(id);
            if (entity is null) return false;
            _context.CompanyExpenseTransactions.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ── Summary / Dashboard ──────────────────────────────────────────────

        public async Task<CompanySummaryDto> GetCompanySummaryAsync(int companyId)
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
                CompanyName    = company?.CompanyName ?? string.Empty,
                CreditsAmount  = totalCredits,
                DebitsAmount   = totalDebits,
                BalanceAmount  = totalCredits - totalDebits
            };
        }

        public async Task<ClientSummaryDto?> GetClientSummaryAsync(int clientId)
        {
            var client = await _context.Clients
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ClientId == clientId);

            if (client is null) return null;

            var credits  = await _context.ClientTransactions.Where(t => t.ClientId == clientId).SumAsync(t => t.CreditAmount);
            var debits   = await _context.ClientTransactions.Where(t => t.ClientId == clientId).SumAsync(t => t.DebitAmount);
            var received = await _context.SupplierTransactions.Where(t => t.ClientId == clientId).SumAsync(t => t.PaidAmount);
            var expenses = await _context.SubContractorTransactions.Where(t => t.ClientId == clientId).SumAsync(t => t.PaidAmount);

            return new ClientSummaryDto
            {
                ClientId                 = client.ClientId,
                ClientName               = client.ClientName,
                CreditsAmount            = credits,
                DebitsAmount             = debits,
                BalanceAmount            = credits - debits,
                EstimateUnits            = client.EstimateUnit ?? 0,
                EstimateRate             = client.EstimateRate ?? 0,
                EstimateAmount           = client.EstimateAmount ?? 0,
                EstimateAmountReceived   = received,
                EstimateAmountExpenses   = expenses
            };
        }

        public async Task<IEnumerable<SupplierSummaryDto>> GetSupplierSummaryByClientAsync(int clientId)
        {
            return await _context.SupplierTransactions
                .AsNoTracking()
                .Where(t => t.ClientId == clientId)
                .GroupBy(t => new { t.SupplierId, t.Supplier.SupplierName })
                .Select(g => new SupplierSummaryDto
                {
                    SupplierId    = g.Key.SupplierId,
                    SupplierName  = g.Key.SupplierName,
                    PayableAmount = g.Sum(t => t.Amount),
                    PaidAmount    = g.Sum(t => t.PaidAmount),
                    BalanceAmount = g.Sum(t => t.Amount) - g.Sum(t => t.PaidAmount)
                })
                .OrderBy(s => s.SupplierName)
                .ToListAsync();
        }

        public async Task<IEnumerable<SubContractorSummaryDto>> GetSubContractorSummaryByClientAsync(int clientId)
        {
            return await _context.SubContractorTransactions
                .AsNoTracking()
                .Where(t => t.ClientId == clientId)
                .GroupBy(t => new { t.SubContractorId, t.SubContractor.SubContractorName })
                .Select(g => new SubContractorSummaryDto
                {
                    SubContractorId   = g.Key.SubContractorId,
                    SubContractorName = g.Key.SubContractorName,
                    PayableAmount     = g.Sum(t => t.Amount),
                    PaidAmount        = g.Sum(t => t.PaidAmount),
                    BalanceAmount     = g.Sum(t => t.Amount) - g.Sum(t => t.PaidAmount)
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
