namespace GymTrackerApi.Models.Relacionamentos;
using GymTrackerApi.Models.Treinos;
using GymTrackerApi.Models.Exercicios;

public class TreinoExercicio
{
    public int TreinoId { get; set; }
    public Treino Treino { get; set; } = null!;

    public int ExercicioId { get; set; }
    public Exercicio Exercicio { get; set; } = null!;
}
