using BuildManager.DTOs;
using BuildManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildManager.Controllers
{
    /// <summary>
    /// Company User Management — Masters menu.
    /// Owner : full CRUD
    /// Admin : read only
    /// User  : no access
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Owner,Admin")]
    public class CompanyUserController : ControllerBase
    {
        private readonly ICompanyUserService _companyUserService;

        public CompanyUserController(ICompanyUserService companyUserService)
        {
            _companyUserService = companyUserService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CompanyUserResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
            => Ok(await _companyUserService.GetAllAsync());

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CompanyUserResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _companyUserService.GetByIdAsync(id);
            return result is null
                ? NotFound(new { message = $"User {id} not found." })
                : Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        [ProducesResponseType(typeof(CompanyUserResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CompanyUserRequestDto dto)
        {
            var result = await _companyUserService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.CompanyUserId }, result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Owner")]
        [ProducesResponseType(typeof(CompanyUserResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] CompanyUserRequestDto dto)
        {
            var result = await _companyUserService.UpdateAsync(id, dto);
            return result is null
                ? NotFound(new { message = $"User {id} not found." })
                : Ok(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Owner")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _companyUserService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound(new { message = $"User {id} not found." });
        }
    }
}
