using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GymTrackerApi.DTOs.PersonalDTOs;
using GymTrackerApi.Services.Interfaces;

namespace GymTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonalController : ControllerBase
    {
        private readonly IPersonalService _service;

        public PersonalController(IPersonalService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonalDTO>>> GetAll()
        {
            var personais = await _service.GetAllAsync();
            return Ok(personais);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonalDTO>> GetById(int id)
        {
            var personal = await _service.GetByIdAsync(id);
            if (personal == null) return NotFound();
            return Ok(personal);
        }

        [HttpGet("me")]
        public async Task<ActionResult<PersonalDTO>> GetMe()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var personal = await _service.GetByUserIdAsync(userId);
            if (personal == null) return NotFound();
            return Ok(personal);
        }

        [HttpPost]
        public async Task<ActionResult<PersonalDTO>> Create(CreatePersonalDTO dto)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "anonymous";
            var personal = await _service.CreateAsync(dto, userId);
            return CreatedAtAction(nameof(GetById), new { id = personal.Id }, personal);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdatePersonalDTO dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
