using BuildManager.DTOs;
using BuildManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildManager.Controllers
{
    /// <summary>
    /// Supplier (Person Details) — Masters menu.
    /// Owner : full CRUD
    /// Admin : read + create + update
    /// User  : no access to masters
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Owner,Admin")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SupplierResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
            => Ok(await _supplierService.GetAllAsync());

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(SupplierResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _supplierService.GetByIdAsync(id);
            return result is null
                ? NotFound(new { message = $"Supplier {id} not found." })
                : Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Owner,Admin")]
        [ProducesResponseType(typeof(SupplierResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] SupplierRequestDto dto)
        {
            var result = await _supplierService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.SupplierId }, result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        [ProducesResponseType(typeof(SupplierResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] SupplierRequestDto dto)
        {
            var result = await _supplierService.UpdateAsync(id, dto);
            return result is null
                ? NotFound(new { message = $"Supplier {id} not found." })
                : Ok(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Owner")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _supplierService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound(new { message = $"Supplier {id} not found." });
        }
    }
}
