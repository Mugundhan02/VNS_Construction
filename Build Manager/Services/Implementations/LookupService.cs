using AutoMapper;
using BuildManager.Data;
using BuildManager.DTOs;
using BuildManager.Models;
using BuildManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuildManager.Services.Implementations
{
    /// <summary>
    /// Handles all simple lookup masters:
    /// PaymentType, Whom, OfficeExpense, CompanyBank, InstallmentTerm.
    /// </summary>
    public class LookupService : ILookupService
    {
        private readonly BuildManagerDbContext _context;
        private readonly IMapper _mapper;

        public LookupService(BuildManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper  = mapper;
        }

        // ── Payment Types ────────────────────────────────────────────────────

        public async Task<IEnumerable<PaymentTypeResponseDto>> GetAllPaymentTypesAsync()
        {
            var list = await _context.PaymentTypes
                .AsNoTracking()
                .OrderBy(p => p.PaymentTypeName)
                .ToListAsync();
            return _mapper.Map<IEnumerable<PaymentTypeResponseDto>>(list);
        }

        public async Task<PaymentTypeResponseDto> CreatePaymentTypeAsync(PaymentTypeRequestDto dto)
        {
            var entity = _mapper.Map<PaymentType>(dto);
            _context.PaymentTypes.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<PaymentTypeResponseDto>(entity);
        }

        public async Task<PaymentTypeResponseDto?> UpdatePaymentTypeAsync(int id, PaymentTypeRequestDto dto)
        {
            var entity = await _context.PaymentTypes.FindAsync(id);
            if (entity is null) return null;
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<PaymentTypeResponseDto>(entity);
        }

        public async Task<bool> DeletePaymentTypeAsync(int id)
        {
            var entity = await _context.PaymentTypes.FindAsync(id);
            if (entity is null) return false;
            _context.PaymentTypes.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ── Whoms ────────────────────────────────────────────────────────────

        public async Task<IEnumerable<WhomResponseDto>> GetAllWhomAsync()
        {
            var list = await _context.Whoms
                .AsNoTracking()
                .OrderBy(w => w.WhomName)
                .ToListAsync();
            return _mapper.Map<IEnumerable<WhomResponseDto>>(list);
        }

        public async Task<WhomResponseDto> CreateWhomAsync(WhomRequestDto dto)
        {
            var entity = _mapper.Map<Whom>(dto);
            _context.Whoms.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<WhomResponseDto>(entity);
        }

        public async Task<WhomResponseDto?> UpdateWhomAsync(int id, WhomRequestDto dto)
        {
            var entity = await _context.Whoms.FindAsync(id);
            if (entity is null) return null;
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<WhomResponseDto>(entity);
        }

        public async Task<bool> DeleteWhomAsync(int id)
        {
            var entity = await _context.Whoms.FindAsync(id);
            if (entity is null) return false;
            _context.Whoms.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ── Office Expenses ──────────────────────────────────────────────────

        public async Task<IEnumerable<OfficeExpenseResponseDto>> GetAllOfficeExpensesAsync()
        {
            var list = await _context.OfficeExpenses
                .AsNoTracking()
                .OrderBy(o => o.ExpenseName)
                .ToListAsync();
            return _mapper.Map<IEnumerable<OfficeExpenseResponseDto>>(list);
        }

        public async Task<OfficeExpenseResponseDto> CreateOfficeExpenseAsync(OfficeExpenseRequestDto dto)
        {
            var entity = _mapper.Map<OfficeExpense>(dto);
            _context.OfficeExpenses.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<OfficeExpenseResponseDto>(entity);
        }

        public async Task<OfficeExpenseResponseDto?> UpdateOfficeExpenseAsync(int id, OfficeExpenseRequestDto dto)
        {
            var entity = await _context.OfficeExpenses.FindAsync(id);
            if (entity is null) return null;
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<OfficeExpenseResponseDto>(entity);
        }

        public async Task<bool> DeleteOfficeExpenseAsync(int id)
        {
            var entity = await _context.OfficeExpenses.FindAsync(id);
            if (entity is null) return false;
            _context.OfficeExpenses.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ── Company Banks ────────────────────────────────────────────────────

        public async Task<IEnumerable<CompanyBankResponseDto>> GetBanksByCompanyAsync(int companyId)
        {
            var list = await _context.CompanyBanks
                .AsNoTracking()
                .Where(b => b.CompanyId == companyId)
                .OrderBy(b => b.BankName)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CompanyBankResponseDto>>(list);
        }

        public async Task<CompanyBankResponseDto> CreateCompanyBankAsync(CompanyBankRequestDto dto)
        {
            var entity = _mapper.Map<CompanyBank>(dto);
            _context.CompanyBanks.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CompanyBankResponseDto>(entity);
        }

        public async Task<CompanyBankResponseDto?> UpdateCompanyBankAsync(int id, CompanyBankRequestDto dto)
        {
            var entity = await _context.CompanyBanks.FindAsync(id);
            if (entity is null) return null;
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CompanyBankResponseDto>(entity);
        }

        public async Task<bool> DeleteCompanyBankAsync(int id)
        {
            var entity = await _context.CompanyBanks.FindAsync(id);
            if (entity is null) return false;
            _context.CompanyBanks.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ── Installment Terms ────────────────────────────────────────────────

        public async Task<IEnumerable<InstallmentTermResponseDto>> GetAllInstallmentTermsAsync()
        {
            var list = await _context.InstallmentTerms
                .AsNoTracking()
                .OrderBy(t => t.TermName)
                .ToListAsync();
            return _mapper.Map<IEnumerable<InstallmentTermResponseDto>>(list);
        }

        public async Task<InstallmentTermResponseDto> CreateInstallmentTermAsync(InstallmentTermRequestDto dto)
        {
            var entity = _mapper.Map<InstallmentTerm>(dto);
            _context.InstallmentTerms.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<InstallmentTermResponseDto>(entity);
        }

        public async Task<InstallmentTermResponseDto?> UpdateInstallmentTermAsync(int id, InstallmentTermRequestDto dto)
        {
            var entity = await _context.InstallmentTerms.FindAsync(id);
            if (entity is null) return null;
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<InstallmentTermResponseDto>(entity);
        }

        public async Task<bool> DeleteInstallmentTermAsync(int id)
        {
            var entity = await _context.InstallmentTerms.FindAsync(id);
            if (entity is null) return false;
            _context.InstallmentTerms.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
