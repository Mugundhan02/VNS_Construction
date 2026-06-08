using AutoMapper;
using BuildManager.Contexts;
using BuildManager.DTOs;
using BuildManager.Exceptions;
using BuildManager.Interfaces;
using BuildManager.Models;
using Microsoft.EntityFrameworkCore;

namespace BuildManager.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly BuildManagerDbContext _context;
        private readonly IMapper               _mapper;

        public MaterialService(BuildManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper  = mapper;
        }

        public async Task<IEnumerable<MaterialResponseDto>> GetAll()
        {
            var list = await _context.Materials.AsNoTracking().OrderBy(m => m.MaterialName).ToListAsync();
            return _mapper.Map<IEnumerable<MaterialResponseDto>>(list);
        }

        public async Task<MaterialResponseDto> GetById(int id)
        {
            var entity = await _context.Materials.AsNoTracking().FirstOrDefaultAsync(m => m.MaterialId == id)
                         ?? throw new EntityNotFoundException("Material", id);
            return _mapper.Map<MaterialResponseDto>(entity);
        }

        public async Task<MaterialResponseDto> Create(MaterialRequestDto dto)
        {
            var entity = _mapper.Map<Material>(dto);
            _context.Materials.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<MaterialResponseDto>(entity);
        }

        public async Task<MaterialResponseDto> Update(int id, MaterialRequestDto dto)
        {
            var entity = await _context.Materials.FindAsync(id)
                         ?? throw new EntityNotFoundException("Material", id);
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<MaterialResponseDto>(entity);
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.Materials.FindAsync(id)
                         ?? throw new EntityNotFoundException("Material", id);
            _context.Materials.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
