using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GymTrackerApi.DTOs.ExercicioDTOs;
using GymTrackerApi.Services.Interfaces;

namespace GymTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExercicioController : ControllerBase
    {
        private readonly IExercicioService _service;

        public ExercicioController(IExercicioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExercicioDTO>>> GetAll([FromQuery] int? alunoId = null)
        {
            var exercicios = await _service.GetAllAsync(alunoId);
            return Ok(exercicios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExercicioDTO>> GetById(int id)
        {
            var exercicio = await _service.GetByIdAsync(id);
            if (exercicio == null) return NotFound();
            return Ok(exercicio);
        }

        [HttpPost]
        public async Task<ActionResult<ExercicioDTO>> Create(CreateExercicioDTO dto)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "anonymous";
            var exercicio = await _service.CreateAsync(dto, userId);
            return CreatedAtAction(nameof(GetById), new { id = exercicio.Id }, exercicio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateExercicioDTO dto)
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
