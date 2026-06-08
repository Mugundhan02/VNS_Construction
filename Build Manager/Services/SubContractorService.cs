using AutoMapper;
using BuildManager.Contexts;
using BuildManager.DTOs;
using BuildManager.Exceptions;
using BuildManager.Interfaces;
using BuildManager.Models;
using Microsoft.EntityFrameworkCore;

namespace BuildManager.Services
{
    public class SubContractorService : ISubContractorService
    {
        private readonly BuildManagerDbContext _context;
        private readonly IMapper               _mapper;

        public SubContractorService(BuildManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper  = mapper;
        }

        public async Task<IEnumerable<SubContractorResponseDto>> GetAll()
        {
            var list = await _context.SubContractors.AsNoTracking().OrderBy(s => s.SubContractorName).ToListAsync();
            return _mapper.Map<IEnumerable<SubContractorResponseDto>>(list);
        }

        public async Task<SubContractorResponseDto> GetById(int id)
        {
            var entity = await _context.SubContractors.AsNoTracking().FirstOrDefaultAsync(s => s.SubContractorId == id)
                         ?? throw new EntityNotFoundException("SubContractor", id);
            return _mapper.Map<SubContractorResponseDto>(entity);
        }

        public async Task<SubContractorResponseDto> Create(SubContractorRequestDto dto)
        {
            var entity = _mapper.Map<SubContractor>(dto);
            _context.SubContractors.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<SubContractorResponseDto>(entity);
        }

        public async Task<SubContractorResponseDto> Update(int id, SubContractorRequestDto dto)
        {
            var entity = await _context.SubContractors.FindAsync(id)
                         ?? throw new EntityNotFoundException("SubContractor", id);
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<SubContractorResponseDto>(entity);
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.SubContractors.FindAsync(id)
                         ?? throw new EntityNotFoundException("SubContractor", id);
            _context.SubContractors.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
