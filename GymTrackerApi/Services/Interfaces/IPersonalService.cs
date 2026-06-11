using GymTrackerApi.DTOs.PersonalDTOs;

namespace GymTrackerApi.Services.Interfaces
{
    public interface IPersonalService
    {
        Task<IEnumerable<PersonalDTO>> GetAllAsync();
        Task<PersonalDTO?> GetByIdAsync(int id);
        Task<PersonalDTO?> GetByUserIdAsync(string userId);
        Task<PersonalDTO> CreateAsync(CreatePersonalDTO dto, string userId);
        Task<bool> UpdateAsync(int id, UpdatePersonalDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
