using AutoMapper;
using BuildManager.Data;
using BuildManager.DTOs;
using BuildManager.Models;
using BuildManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuildManager.Services.Implementations
{
    public class ClientService : IClientService
    {
        private readonly BuildManagerDbContext _context;
        private readonly IMapper _mapper;

        public ClientService(BuildManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper  = mapper;
        }

        public async Task<IEnumerable<ClientResponseDto>> GetAllAsync()
        {
            var clients = await _context.Clients
                .AsNoTracking()
                .OrderBy(c => c.ClientName)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ClientResponseDto>>(clients);
        }

        public async Task<ClientResponseDto?> GetByIdAsync(int id)
        {
            var client = await _context.Clients
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ClientId == id);

            return client is null ? null : _mapper.Map<ClientResponseDto>(client);
        }

        public async Task<ClientResponseDto> CreateAsync(ClientRequestDto dto)
        {
            var client = _mapper.Map<Client>(dto);
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return _mapper.Map<ClientResponseDto>(client);
        }

        public async Task<ClientResponseDto?> UpdateAsync(int id, ClientRequestDto dto)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client is null) return null;

            _mapper.Map(dto, client);
            await _context.SaveChangesAsync();
            return _mapper.Map<ClientResponseDto>(client);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client is null) return false;

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
