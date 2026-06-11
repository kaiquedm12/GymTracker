using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GymTrackerApi.DTOs.AlunoDTOs;
using GymTrackerApi.Services.Interfaces;

namespace GymTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoService _service;

        public AlunoController(IAlunoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlunoDTO>>> GetAll([FromQuery] int personalId)
        {
            var alunos = await _service.GetAllAsync(personalId);
            return Ok(alunos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlunoDTO>> GetById(int id)
        {
            var aluno = await _service.GetByIdAsync(id);
            if (aluno == null) return NotFound();
            return Ok(aluno);
        }

        [HttpPost]
        public async Task<ActionResult<AlunoDTO>> Create(CreateAlunoDTO dto, [FromQuery] int personalId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "anonymous";
            var aluno = await _service.CreateAsync(dto, personalId, userId);
            return CreatedAtAction(nameof(GetById), new { id = aluno.Id }, aluno);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateAlunoDTO dto)
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
