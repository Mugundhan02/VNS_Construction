using AutoMapper;
using BuildManager.Data;
using BuildManager.DTOs;
using BuildManager.Models;
using BuildManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuildManager.Services.Implementations
{
    public class MaterialService : IMaterialService
    {
        private readonly BuildManagerDbContext _context;
        private readonly IMapper _mapper;

        public MaterialService(BuildManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper  = mapper;
        }

        public async Task<IEnumerable<MaterialResponseDto>> GetAllAsync()
        {
            var materials = await _context.Materials
                .AsNoTracking()
                .OrderBy(m => m.MaterialName)
                .ToListAsync();

            return _mapper.Map<IEnumerable<MaterialResponseDto>>(materials);
        }

        public async Task<MaterialResponseDto?> GetByIdAsync(int id)
        {
            var material = await _context.Materials
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MaterialId == id);

            return material is null ? null : _mapper.Map<MaterialResponseDto>(material);
        }

        public async Task<MaterialResponseDto> CreateAsync(MaterialRequestDto dto)
        {
            var material = _mapper.Map<Material>(dto);
            _context.Materials.Add(material);
            await _context.SaveChangesAsync();
            return _mapper.Map<MaterialResponseDto>(material);
        }

        public async Task<MaterialResponseDto?> UpdateAsync(int id, MaterialRequestDto dto)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material is null) return null;

            _mapper.Map(dto, material);
            await _context.SaveChangesAsync();
            return _mapper.Map<MaterialResponseDto>(material);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material is null) return false;

            _context.Materials.Remove(material);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
