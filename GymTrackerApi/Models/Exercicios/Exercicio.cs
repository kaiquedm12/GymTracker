using System.ComponentModel.DataAnnotations.Schema;
using GymTrackerApi.Models.Pessoas;
using GymTrackerApi.Models.Treinos;
using GymTrackerApi.Models.Relacionamentos;

namespace GymTrackerApi.Models.Exercicios;

public class Exercicio
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Repeticoes { get; set; }
    public int Series { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal Peso { get; set; }

    public int? TreinoId { get; set; }
    public Treino? Treino { get; set; }

    public int? AlunoId { get; set; }
    public Aluno? Aluno { get; set; }

    public string UserId { get; set; } = string.Empty;

    public ICollection<TreinoExercicio> TreinosExercicios { get; set; } = new List<TreinoExercicio>();
}
