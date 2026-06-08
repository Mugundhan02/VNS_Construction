using BuildManager.DTOs;
using BuildManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _materialService;

        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        /// <summary>Get all materials.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MaterialResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _materialService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>Get a material by ID.</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(MaterialResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _materialService.GetByIdAsync(id);
            if (result is null)
                return NotFound(new { message = $"Material {id} not found." });

            return Ok(result);
        }

        /// <summary>Create a new material. (Owner, Admin)</summary>
        [HttpPost]
        [Authorize(Roles = "Owner,Admin")]
        [ProducesResponseType(typeof(MaterialResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] MaterialRequestDto dto)
        {
            var result = await _materialService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.MaterialId }, result);
        }

        /// <summary>Update a material. (Owner, Admin)</summary>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        [ProducesResponseType(typeof(MaterialResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] MaterialRequestDto dto)
        {
            var result = await _materialService.UpdateAsync(id, dto);
            if (result is null)
                return NotFound(new { message = $"Material {id} not found." });

            return Ok(result);
        }

        /// <summary>Delete a material. (Owner only)</summary>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Owner")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _materialService.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { message = $"Material {id} not found." });

            return NoContent();
        }
    }
}
