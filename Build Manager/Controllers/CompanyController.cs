using BuildManager.DTOs;
using BuildManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildManager.Controllers
{
    /// <summary>
    /// Company Settings — Masters menu.
    /// Owner : full CRUD
    /// Admin : read + update
    /// User  : no access
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Owner,Admin")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CompanyResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
            => Ok(await _companyService.GetAllAsync());

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CompanyResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _companyService.GetByIdAsync(id);
            return result is null
                ? NotFound(new { message = $"Company {id} not found." })
                : Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        [ProducesResponseType(typeof(CompanyResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CompanyRequestDto dto)
        {
            var result = await _companyService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.CompanyId }, result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        [ProducesResponseType(typeof(CompanyResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] CompanyRequestDto dto)
        {
            var result = await _companyService.UpdateAsync(id, dto);
            return result is null
                ? NotFound(new { message = $"Company {id} not found." })
                : Ok(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Owner")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _companyService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound(new { message = $"Company {id} not found." });
        }
    }
}
