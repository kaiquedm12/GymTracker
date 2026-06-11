using AutoMapper;
using GymTrackerApi.Data;
using GymTrackerApi.DTOs.AlunoDTOs;
using GymTrackerApi.Models.Pessoas;
using GymTrackerApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymTrackerApi.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AlunoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AlunoDTO>> GetAllAsync(int personalId)
        {
            var alunos = await _context.Alunos
                .Where(a => a.PersonalId == personalId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<AlunoDTO>>(alunos);
        }

        public async Task<AlunoDTO?> GetByIdAsync(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            return aluno == null ? null : _mapper.Map<AlunoDTO>(aluno);
        }

        public async Task<AlunoDTO> CreateAsync(CreateAlunoDTO dto, int personalId, string userId)
        {
            var personal = await _context.Personais.FindAsync(personalId);
            if (personal == null)
                throw new KeyNotFoundException("Personal não encontrado.");

            var aluno = _mapper.Map<Aluno>(dto);
            aluno.PersonalId = personalId;
            aluno.UserId = userId;
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();
            return _mapper.Map<AlunoDTO>(aluno);
        }

        public async Task<bool> UpdateAsync(int id, UpdateAlunoDTO dto)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null) return false;

            _mapper.Map(dto, aluno);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null) return false;

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
