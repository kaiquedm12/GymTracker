using GymTrackerApi.DTOs.TreinoDTOs;

namespace GymTrackerApi.Services.Interfaces
{
    public interface ITreinoService
    {
        Task<IEnumerable<TreinoDTO>> GetAllAsync();
        Task<TreinoDTO?> GetByIdAsync(int id);
        Task<TreinoDTO> CreateAsync(CreateTreinoDTO dto);
        Task<bool> UpdateAsync(int id, UpdateTreinoDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
