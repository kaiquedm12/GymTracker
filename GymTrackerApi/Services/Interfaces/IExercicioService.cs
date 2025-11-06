using GymTrackerApi.DTOs.ExercicioDTOs;

namespace GymTrackerApi.Services.Interfaces
{
    public interface IExercicioService
    {
        Task<IEnumerable<ExercicioDTO>> GetAllAsync();
        Task<ExercicioDTO?> GetByIdAsync(int id);
        Task<ExercicioDTO> CreateAsync(CreateExercicioDTO dto);
        Task<bool> UpdateAsync(int id, UpdateExercicioDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
