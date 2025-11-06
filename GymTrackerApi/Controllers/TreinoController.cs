using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymTrackerApi.Data;
using GymTrackerApi.DTOs.TreinoDTOs;
using GymTrackerApi.Models.Treinos;
using GymTrackerApi.Models.Relacionamentos;
using AutoMapper;

namespace GymTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreinoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TreinoController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // === GET: api/treino ===
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TreinoDTO>>> GetAll()
        {
            var treinos = await _context.Treinos
                .Include(t => t.TreinoExercicio)
                .ThenInclude(te => te.Exercicio)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<TreinoDTO>>(treinos));
        }

        // === GET: api/treino/{id} ===
        [HttpGet("{id}")]
        public async Task<ActionResult<TreinoDTO>> GetById(int id)
        {
            var treino = await _context.Treinos
                .Include(t => t.TreinoExercicio)
                .ThenInclude(te => te.Exercicio)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (treino == null) return NotFound();

            return Ok(_mapper.Map<TreinoDTO>(treino));
        }

        // === POST: api/treino ===
        [HttpPost]
        public async Task<ActionResult<TreinoDTO>> Create(CreateTreinoDTO dto)
        {
            var treino = _mapper.Map<Treino>(dto);
            _context.Treinos.Add(treino);
            await _context.SaveChangesAsync();

            // Relacionar exercícios (se houver IDs enviados)
            if (dto.ExerciciosIds != null && dto.ExerciciosIds.Any())
            {
                foreach (var exercicioId in dto.ExerciciosIds)
                {
                    _context.TreinosExercicios.Add(new TreinoExercicio
                    {
                        TreinoId = treino.Id,
                        ExercicioId = exercicioId
                    });
                }
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(GetById), new { id = treino.Id }, _mapper.Map<TreinoDTO>(treino));
        }

        // === PUT: api/treino/{id} ===
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTreinoDTO dto)
        {
            var treino = await _context.Treinos
                .Include(t => t.TreinoExercicio)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (treino == null) return NotFound();

            _mapper.Map(dto, treino);
            _context.TreinosExercicios.RemoveRange(treino.TreinoExercicio);

            if (dto.ExerciciosIds != null && dto.ExerciciosIds.Any())
            {
                treino.TreinoExercicio = dto.ExerciciosIds.Select(eid => new TreinoExercicio
                {
                    TreinoId = treino.Id,
                    ExercicioId = eid
                }).ToList();
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // === DELETE: api/treino/{id} ===
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var treino = await _context.Treinos.FindAsync(id);
            if (treino == null) return NotFound();

            _context.Treinos.Remove(treino);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
