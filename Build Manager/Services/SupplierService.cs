using AutoMapper;
using BuildManager.Contexts;
using BuildManager.DTOs;
using BuildManager.Exceptions;
using BuildManager.Interfaces;
using BuildManager.Models;
using Microsoft.EntityFrameworkCore;

namespace BuildManager.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly BuildManagerDbContext _context;
        private readonly IMapper               _mapper;

        public SupplierService(BuildManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper  = mapper;
        }

        public async Task<IEnumerable<SupplierResponseDto>> GetAll()
        {
            var list = await _context.Suppliers.AsNoTracking().OrderBy(s => s.SupplierName).ToListAsync();
            return _mapper.Map<IEnumerable<SupplierResponseDto>>(list);
        }

        public async Task<SupplierResponseDto> GetById(int id)
        {
            var entity = await _context.Suppliers.AsNoTracking().FirstOrDefaultAsync(s => s.SupplierId == id)
                         ?? throw new EntityNotFoundException("Supplier", id);
            return _mapper.Map<SupplierResponseDto>(entity);
        }

        public async Task<SupplierResponseDto> Create(SupplierRequestDto dto)
        {
            var entity = _mapper.Map<Supplier>(dto);
            _context.Suppliers.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<SupplierResponseDto>(entity);
        }

        public async Task<SupplierResponseDto> Update(int id, SupplierRequestDto dto)
        {
            var entity = await _context.Suppliers.FindAsync(id)
                         ?? throw new EntityNotFoundException("Supplier", id);
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<SupplierResponseDto>(entity);
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.Suppliers.FindAsync(id)
                         ?? throw new EntityNotFoundException("Supplier", id);
            _context.Suppliers.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
