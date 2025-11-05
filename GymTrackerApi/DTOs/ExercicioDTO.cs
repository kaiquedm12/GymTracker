namespace GymTrackerApi.DTOs.ExercicioDTOs
{
    public class ExercicioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Repeticoes { get; set; }
        public double peso { get; set; }
    }
}