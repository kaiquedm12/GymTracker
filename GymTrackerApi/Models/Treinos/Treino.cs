using GymTrackerApi.Models.Exercicios;
using GymTrackerApi.Models.Pessoas;
using GymTrackerApi.Models.Relacionamentos;

namespace GymTrackerApi.Models.Treinos;

public class Treino
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateTime Data { get; set; } = DateTime.UtcNow;
    public int DuracaoMinutos { get; set; }
    public string UserId { get; set; } = string.Empty;

    public int AlunoId { get; set; }
    public Aluno Aluno { get; set; } = null!;

    public List<Exercicio> Exercicios { get; set; } = new();
    public ICollection<TreinoExercicio> TreinoExercicio { get; set; } = new List<TreinoExercicio>();
}
