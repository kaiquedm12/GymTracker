using AutoMapper;
using GymTrackerApi.Data;
using GymTrackerApi.DTOs.PersonalDTOs;
using GymTrackerApi.Models.Pessoas;
using GymTrackerApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymTrackerApi.Services
{
    public class PersonalService : IPersonalService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PersonalService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonalDTO>> GetAllAsync()
        {
            var personais = await _context.Personais.ToListAsync();
            return _mapper.Map<IEnumerable<PersonalDTO>>(personais);
        }

        public async Task<PersonalDTO?> GetByIdAsync(int id)
        {
            var personal = await _context.Personais.FindAsync(id);
            return personal == null ? null : _mapper.Map<PersonalDTO>(personal);
        }

        public async Task<PersonalDTO?> GetByUserIdAsync(string userId)
        {
            var personal = await _context.Personais.FirstOrDefaultAsync(p => p.UserId == userId);
            return personal == null ? null : _mapper.Map<PersonalDTO>(personal);
        }

        public async Task<PersonalDTO> CreateAsync(CreatePersonalDTO dto, string userId)
        {
            var personal = _mapper.Map<Personal>(dto);
            personal.UserId = userId;
            _context.Personais.Add(personal);
            await _context.SaveChangesAsync();
            return _mapper.Map<PersonalDTO>(personal);
        }

        public async Task<bool> UpdateAsync(int id, UpdatePersonalDTO dto)
        {
            var personal = await _context.Personais.FindAsync(id);
            if (personal == null) return false;

            _mapper.Map(dto, personal);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var personal = await _context.Personais.FindAsync(id);
            if (personal == null) return false;

            _context.Personais.Remove(personal);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
