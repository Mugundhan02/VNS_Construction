using BuildManager.DTOs;
using BuildManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildManager.Controllers
{
    /// <summary>
    /// Client (Person Details) — Masters menu.
    /// Owner : full CRUD
    /// Admin : read + create + update
    /// User  : no access to masters
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Owner,Admin")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClientResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
            => Ok(await _clientService.GetAllAsync());

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ClientResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _clientService.GetByIdAsync(id);
            return result is null
                ? NotFound(new { message = $"Client {id} not found." })
                : Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Owner,Admin")]
        [ProducesResponseType(typeof(ClientResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ClientRequestDto dto)
        {
            var result = await _clientService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.ClientId }, result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        [ProducesResponseType(typeof(ClientResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] ClientRequestDto dto)
        {
            var result = await _clientService.UpdateAsync(id, dto);
            return result is null
                ? NotFound(new { message = $"Client {id} not found." })
                : Ok(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Owner")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _clientService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound(new { message = $"Client {id} not found." });
        }
    }
}
