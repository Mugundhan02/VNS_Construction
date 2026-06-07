using AutoMapper;
using BuildManager.Data;
using BuildManager.DTOs;
using BuildManager.Models;
using BuildManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuildManager.Services.Implementations
{
    public class SupplierService : ISupplierService
    {
        private readonly BuildManagerDbContext _context;
        private readonly IMapper _mapper;

        public SupplierService(BuildManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper  = mapper;
        }

        public async Task<IEnumerable<SupplierResponseDto>> GetAllAsync()
        {
            var suppliers = await _context.Suppliers
                .AsNoTracking()
                .OrderBy(s => s.SupplierName)
                .ToListAsync();

            return _mapper.Map<IEnumerable<SupplierResponseDto>>(suppliers);
        }

        public async Task<SupplierResponseDto?> GetByIdAsync(int id)
        {
            var supplier = await _context.Suppliers
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.SupplierId == id);

            return supplier is null ? null : _mapper.Map<SupplierResponseDto>(supplier);
        }

        public async Task<SupplierResponseDto> CreateAsync(SupplierRequestDto dto)
        {
            var supplier = _mapper.Map<Supplier>(dto);
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
            return _mapper.Map<SupplierResponseDto>(supplier);
        }

        public async Task<SupplierResponseDto?> UpdateAsync(int id, SupplierRequestDto dto)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier is null) return null;

            _mapper.Map(dto, supplier);
            await _context.SaveChangesAsync();
            return _mapper.Map<SupplierResponseDto>(supplier);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier is null) return false;

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
