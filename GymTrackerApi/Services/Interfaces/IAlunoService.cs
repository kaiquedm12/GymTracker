using GymTrackerApi.DTOs.AlunoDTOs;

namespace GymTrackerApi.Services.Interfaces
{
    public interface IAlunoService
    {
        Task<IEnumerable<AlunoDTO>> GetAllAsync(int personalId);
        Task<AlunoDTO?> GetByIdAsync(int id);
        Task<AlunoDTO> CreateAsync(CreateAlunoDTO dto, int personalId, string userId);
        Task<bool> UpdateAsync(int id, UpdateAlunoDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
