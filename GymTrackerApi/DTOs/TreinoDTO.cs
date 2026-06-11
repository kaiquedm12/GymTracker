using GymTrackerApi.DTOs.ExercicioDTOs;

namespace GymTrackerApi.DTOs.TreinoDTOs
{
    public class TreinoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public int DuracaoMinutos { get; set; }
        public int AlunoId { get; set; }
        public List<ExercicioDTO> Exercicios { get; set; } = new();
    }
}
