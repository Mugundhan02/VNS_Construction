using AutoMapper;
using BuildManager.Contexts;
using BuildManager.DTOs;
using BuildManager.Exceptions;
using BuildManager.Interfaces;
using BuildManager.Models;
using Microsoft.EntityFrameworkCore;

namespace BuildManager.Services
{
    public class LookupService : ILookupService
    {
        private readonly BuildManagerDbContext _context;
        private readonly IMapper _mapper;

        public LookupService(BuildManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // ── Payment Types ────────────────────────────────────────────────────

        public async Task<IEnumerable<PaymentTypeResponseDto>> GetAllPaymentTypes()
        {
            var list = await _context.PaymentTypes
                .AsNoTracking()
                .OrderBy(p => p.PaymentTypeName)
                .ToListAsync();
            return _mapper.Map<IEnumerable<PaymentTypeResponseDto>>(list);
        }

        public async Task<PaymentTypeResponseDto> CreatePaymentType(PaymentTypeRequestDto dto)
        {
            var entity = _mapper.Map<PaymentType>(dto);
            _context.PaymentTypes.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<PaymentTypeResponseDto>(entity);
        }

        public async Task<PaymentTypeResponseDto> UpdatePaymentType(int id, PaymentTypeRequestDto dto)
        {
            var entity = await _context.PaymentTypes.FindAsync(id)
                         ?? throw new EntityNotFoundException("PaymentType", id);

            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<PaymentTypeResponseDto>(entity);
        }

        public async Task<bool> DeletePaymentType(int id)
        {
            var entity = await _context.PaymentTypes.FindAsync(id)
                         ?? throw new EntityNotFoundException("PaymentType", id);

            _context.PaymentTypes.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ── Whoms ────────────────────────────────────────────────────────────

        public async Task<IEnumerable<WhomResponseDto>> GetAllWhom()
        {
            var list = await _context.Whoms
                .AsNoTracking()
                .OrderBy(w => w.WhomName)
                .ToListAsync();
            return _mapper.Map<IEnumerable<WhomResponseDto>>(list);
        }

        public async Task<WhomResponseDto> CreateWhom(WhomRequestDto dto)
        {
            var entity = _mapper.Map<Whom>(dto);
            _context.Whoms.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<WhomResponseDto>(entity);
        }

        public async Task<WhomResponseDto> UpdateWhom(int id, WhomRequestDto dto)
        {
            var entity = await _context.Whoms.FindAsync(id)
                         ?? throw new EntityNotFoundException("Whom", id);

            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<WhomResponseDto>(entity);
        }

        public async Task<bool> DeleteWhom(int id)
        {
            var entity = await _context.Whoms.FindAsync(id)
                         ?? throw new EntityNotFoundException("Whom", id);

            _context.Whoms.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ── Office Expenses ──────────────────────────────────────────────────

        public async Task<IEnumerable<OfficeExpenseResponseDto>> GetAllOfficeExpenses()
        {
            var list = await _context.OfficeExpenses
                .AsNoTracking()
                .OrderBy(o => o.ExpenseName)
                .ToListAsync();
            return _mapper.Map<IEnumerable<OfficeExpenseResponseDto>>(list);
        }

        public async Task<OfficeExpenseResponseDto> CreateOfficeExpense(OfficeExpenseRequestDto dto)
        {
            var entity = _mapper.Map<OfficeExpense>(dto);
            _context.OfficeExpenses.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<OfficeExpenseResponseDto>(entity);
        }

        public async Task<OfficeExpenseResponseDto> UpdateOfficeExpense(int id, OfficeExpenseRequestDto dto)
        {
            var entity = await _context.OfficeExpenses.FindAsync(id)
                         ?? throw new EntityNotFoundException("OfficeExpense", id);

            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<OfficeExpenseResponseDto>(entity);
        }

        public async Task<bool> DeleteOfficeExpense(int id)
        {
            var entity = await _context.OfficeExpenses.FindAsync(id)
                         ?? throw new EntityNotFoundException("OfficeExpense", id);

            _context.OfficeExpenses.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ── Company Banks ────────────────────────────────────────────────────

        public async Task<IEnumerable<CompanyBankResponseDto>> GetBanksByCompany(int companyId)
        {
            var list = await _context.CompanyBanks
                .AsNoTracking()
                .Where(b => b.CompanyId == companyId)
                .OrderBy(b => b.BankName)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CompanyBankResponseDto>>(list);
        }

        public async Task<CompanyBankResponseDto> CreateCompanyBank(CompanyBankRequestDto dto)
        {
            var entity = _mapper.Map<CompanyBank>(dto);
            _context.CompanyBanks.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CompanyBankResponseDto>(entity);
        }

        public async Task<CompanyBankResponseDto> UpdateCompanyBank(int id, CompanyBankRequestDto dto)
        {
            var entity = await _context.CompanyBanks.FindAsync(id)
                         ?? throw new EntityNotFoundException("CompanyBank", id);

            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CompanyBankResponseDto>(entity);
        }

        public async Task<bool> DeleteCompanyBank(int id)
        {
            var entity = await _context.CompanyBanks.FindAsync(id)
                         ?? throw new EntityNotFoundException("CompanyBank", id);

            _context.CompanyBanks.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ── Installment Terms ────────────────────────────────────────────────

        public async Task<IEnumerable<InstallmentTermResponseDto>> GetAllInstallmentTerms()
        {
            var list = await _context.InstallmentTerms
                .AsNoTracking()
                .OrderBy(t => t.TermName)
                .ToListAsync();
            return _mapper.Map<IEnumerable<InstallmentTermResponseDto>>(list);
        }

        public async Task<InstallmentTermResponseDto> CreateInstallmentTerm(InstallmentTermRequestDto dto)
        {
            var entity = _mapper.Map<InstallmentTerm>(dto);
            _context.InstallmentTerms.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<InstallmentTermResponseDto>(entity);
        }

        public async Task<InstallmentTermResponseDto> UpdateInstallmentTerm(int id, InstallmentTermRequestDto dto)
        {
            var entity = await _context.InstallmentTerms.FindAsync(id)
                         ?? throw new EntityNotFoundException("InstallmentTerm", id);

            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<InstallmentTermResponseDto>(entity);
        }

        public async Task<bool> DeleteInstallmentTerm(int id)
        {
            var entity = await _context.InstallmentTerms.FindAsync(id)
                         ?? throw new EntityNotFoundException("InstallmentTerm", id);

            _context.InstallmentTerms.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}