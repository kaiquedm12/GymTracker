using AutoMapper;
using GymTrackerApi.Data;
using GymTrackerApi.DTOs.ExercicioDTOs;
using GymTrackerApi.Models.Exercicios;
using GymTrackerApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymTrackerApi.Services
{
    public class ExercicioService : IExercicioService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ExercicioService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExercicioDTO>> GetAllAsync()
        {
            var exercicios = await _context.Exercicios.ToListAsync();
            return _mapper.Map<IEnumerable<ExercicioDTO>>(exercicios);
        }

        public async Task<ExercicioDTO?> GetByIdAsync(int id)
        {
            var exercicio = await _context.Exercicios.FindAsync(id);
            return exercicio == null ? null : _mapper.Map<ExercicioDTO>(exercicio);
        }

        public async Task<ExercicioDTO> CreateAsync(CreateExercicioDTO dto)
        {
            var exercicio = _mapper.Map<Exercicio>(dto);
            _context.Exercicios.Add(exercicio);
            await _context.SaveChangesAsync();
            return _mapper.Map<ExercicioDTO>(exercicio);
        }

        public async Task<bool> UpdateAsync(int id, UpdateExercicioDTO dto)
        {
            var exercicio = await _context.Exercicios.FindAsync(id);
            if (exercicio == null) return false;

            _mapper.Map(dto, exercicio);
            _context.Exercicios.Update(exercicio);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var exercicio = await _context.Exercicios.FindAsync(id);
            if (exercicio == null) return false;

            _context.Exercicios.Remove(exercicio);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
