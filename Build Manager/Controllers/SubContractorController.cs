using BuildManager.DTOs;
using BuildManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildManager.Controllers
{
    /// <summary>
    /// SubContractor (Person Details) — Masters menu.
    /// Owner : full CRUD
    /// Admin : read + create + update
    /// User  : no access to masters
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Owner,Admin")]
    public class SubContractorController : ControllerBase
    {
        private readonly ISubContractorService _subContractorService;

        public SubContractorController(ISubContractorService subContractorService)
        {
            _subContractorService = subContractorService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SubContractorResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
            => Ok(await _subContractorService.GetAllAsync());

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(SubContractorResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _subContractorService.GetByIdAsync(id);
            return result is null
                ? NotFound(new { message = $"SubContractor {id} not found." })
                : Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Owner,Admin")]
        [ProducesResponseType(typeof(SubContractorResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] SubContractorRequestDto dto)
        {
            var result = await _subContractorService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.SubContractorId }, result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        [ProducesResponseType(typeof(SubContractorResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] SubContractorRequestDto dto)
        {
            var result = await _subContractorService.UpdateAsync(id, dto);
            return result is null
                ? NotFound(new { message = $"SubContractor {id} not found." })
                : Ok(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Owner")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _subContractorService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound(new { message = $"SubContractor {id} not found." });
        }
    }
}
