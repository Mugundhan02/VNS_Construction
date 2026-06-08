using BuildManager.DTOs;
using BuildManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class JobWorkController : ControllerBase
    {
        private readonly IJobWorkService _jobWorkService;

        public JobWorkController(IJobWorkService jobWorkService)
        {
            _jobWorkService = jobWorkService;
        }

        /// <summary>Get all job works.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<JobWorkResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _jobWorkService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>Get a job work by ID.</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(JobWorkResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _jobWorkService.GetByIdAsync(id);
            if (result is null)
                return NotFound(new { message = $"JobWork {id} not found." });

            return Ok(result);
        }

        /// <summary>Create a new job work. (Owner, Admin)</summary>
        [HttpPost]
        [Authorize(Roles = "Owner,Admin")]
        [ProducesResponseType(typeof(JobWorkResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] JobWorkRequestDto dto)
        {
            var result = await _jobWorkService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.JobWorkId }, result);
        }

        /// <summary>Update a job work. (Owner, Admin)</summary>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Owner,Admin")]
        [ProducesResponseType(typeof(JobWorkResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] JobWorkRequestDto dto)
        {
            var result = await _jobWorkService.UpdateAsync(id, dto);
            if (result is null)
                return NotFound(new { message = $"JobWork {id} not found." });

            return Ok(result);
        }

        /// <summary>Delete a job work. (Owner only)</summary>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Owner")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _jobWorkService.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { message = $"JobWork {id} not found." });

            return NoContent();
        }
    }
}
