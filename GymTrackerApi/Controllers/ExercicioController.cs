using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymTrackerApi.Data;
using GymTrackerApi.DTOs.ExercicioDTOs;
using GymTrackerApi.Models.Exercicios;
using AutoMapper;

namespace GymTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercicioController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ExercicioController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/exercicio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExercicioDTO>>> GetAll()
        {
            var exercicios = await _context.Exercicios.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ExercicioDTO>>(exercicios));
        }

        // GET: api/exercicio/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ExercicioDTO>> GetById(int id)
        {
            var exercicio = await _context.Exercicios.FindAsync(id);
            if (exercicio == null) return NotFound();
            return Ok(_mapper.Map<ExercicioDTO>(exercicio));
        }

        // POST: api/exercicio
        [HttpPost]
        public async Task<ActionResult<ExercicioDTO>> Create(CreateExercicioDTO dto)
        {
            var exercicio = _mapper.Map<Exercicio>(dto);
            _context.Exercicios.Add(exercicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = exercicio.Id }, _mapper.Map<ExercicioDTO>(exercicio));
        }

        // PUT: api/exercicio/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateExercicioDTO dto)
        {
            var exercicio = await _context.Exercicios.FindAsync(id);
            if (exercicio == null) return NotFound();

            _mapper.Map(dto, exercicio);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/exercicio/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exercicio = await _context.Exercicios.FindAsync(id);
            if (exercicio == null) return NotFound();

            _context.Exercicios.Remove(exercicio);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
