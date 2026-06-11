namespace GymTrackerApi.DTOs.ExercicioDTOs
{
    public class ExercicioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Repeticoes { get; set; }
        public int Series { get; set; }
        public decimal Peso { get; set; }
        public int? AlunoId { get; set; }
    }
}
