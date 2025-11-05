namespace GymTrackerApi.DTOs.TreinoExercicioDTOs
{
    public class TreinoExercicioDTO
    {
        public int TreinoId { get; set; }
        public int ExercicioId { get; set; }

        public int Repeticoes { get; set; }
        public double Peso { get; set; }
    }
}