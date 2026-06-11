namespace GymTrackerApi.DTOs.TreinoDTOs
{
    public class CreateTreinoDTO
    {
        public string Nome { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public int DuracaoMinutos { get; set; }
        public int AlunoId { get; set; }
        public List<int> ExerciciosIds { get; set; } = new();
    }
}
