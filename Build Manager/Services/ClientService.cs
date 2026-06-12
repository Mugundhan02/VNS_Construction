using AutoMapper;
using BuildManager.Contexts;
using BuildManager.DTOs;
using BuildManager.Exceptions;
using BuildManager.Interfaces;
using BuildManager.Models;
using Microsoft.EntityFrameworkCore;

namespace BuildManager.Services
{
    public class ClientService : IClientService
    {
        private readonly BuildManagerDbContext _context;
        private readonly IMapper _mapper;

        public ClientService(BuildManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientResponseDto>> GetAll()
        {
            var list = await _context.Clients.AsNoTracking().OrderBy(c => c.ClientName).ToListAsync();
            return _mapper.Map<IEnumerable<ClientResponseDto>>(list);
        }

        public async Task<ClientResponseDto> GetById(int id)
        {
            var entity = await _context.Clients.AsNoTracking().FirstOrDefaultAsync(c => c.ClientId == id)
                         ?? throw new EntityNotFoundException("Client", id);
            return _mapper.Map<ClientResponseDto>(entity);
        }

        public async Task<ClientResponseDto> Create(ClientRequestDto dto)
        {
            var entity = _mapper.Map<Client>(dto);
            _context.Clients.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ClientResponseDto>(entity);
        }

        public async Task<ClientResponseDto> Update(int id, ClientRequestDto dto)
        {
            var entity = await _context.Clients.FindAsync(id)
                         ?? throw new EntityNotFoundException("Client", id);
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ClientResponseDto>(entity);
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.Clients.FindAsync(id)
                         ?? throw new EntityNotFoundException("Client", id);
            _context.Clients.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}