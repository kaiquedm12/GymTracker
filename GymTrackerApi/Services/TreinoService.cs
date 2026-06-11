using AutoMapper;
using GymTrackerApi.Data;
using GymTrackerApi.DTOs.TreinoDTOs;
using GymTrackerApi.Models.Treinos;
using GymTrackerApi.Models.Relacionamentos;
using GymTrackerApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymTrackerApi.Services
{
    public class TreinoService : ITreinoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TreinoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TreinoDTO>> GetAllAsync(int? alunoId = null)
        {
            var query = _context.Treinos
                .Include(t => t.TreinoExercicio)
                .ThenInclude(te => te.Exercicio)
                .AsQueryable();

            if (alunoId.HasValue)
                query = query.Where(t => t.AlunoId == alunoId);

            var treinos = await query.ToListAsync();
            return _mapper.Map<IEnumerable<TreinoDTO>>(treinos);
        }

        public async Task<TreinoDTO?> GetByIdAsync(int id)
        {
            var treino = await _context.Treinos
                .Include(t => t.TreinoExercicio)
                .ThenInclude(te => te.Exercicio)
                .FirstOrDefaultAsync(t => t.Id == id);

            return treino == null ? null : _mapper.Map<TreinoDTO>(treino);
        }

        public async Task<TreinoDTO> CreateAsync(CreateTreinoDTO dto, string userId)
        {
            var treino = _mapper.Map<Treino>(dto);
            treino.UserId = userId;
            _context.Treinos.Add(treino);
            await _context.SaveChangesAsync();

            if (dto.ExerciciosIds.Count != 0)
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

            return _mapper.Map<TreinoDTO>(treino);
        }

        public async Task<bool> UpdateAsync(int id, UpdateTreinoDTO dto)
        {
            var treino = await _context.Treinos
                .Include(t => t.TreinoExercicio)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (treino == null) return false;

            _mapper.Map(dto, treino);
            _context.TreinosExercicios.RemoveRange(treino.TreinoExercicio);

            if (dto.ExerciciosIds.Count != 0)
            {
                treino.TreinoExercicio = dto.ExerciciosIds.Select(eid => new TreinoExercicio
                {
                    TreinoId = treino.Id,
                    ExercicioId = eid
                }).ToList();
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var treino = await _context.Treinos.FindAsync(id);
            if (treino == null) return false;

            _context.Treinos.Remove(treino);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
