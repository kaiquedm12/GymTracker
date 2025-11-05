namespace GymTrackerApi.Models.Exercicios;

using System.ComponentModel.DataAnnotations;
using GymTrackerApi.Models.Treinos;
using GymTrackerApi.Models.Relacionamentos;

public class Exercicio
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Repeticoes { get; set; } 
    public double peso { get; set; }

    // Chave estrangeira para o treino
    public int TreinoId { get; set; }
    public Treino? Treino { get; set; }

    public ICollection<TreinoExercicio> TreinosExercicios { get; set; }


}