namespace GymTrackerApi.Models.Pessoas;

public class Aluno
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public DateTime? DataNascimento { get; set; }
    public string UserId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int PersonalId { get; set; }
    public Personal Personal { get; set; } = null!;

    public ICollection<Treinos.Treino> Treinos { get; set; } = new List<Treinos.Treino>();
    public ICollection<Exercicios.Exercicio> Exercicios { get; set; } = new List<Exercicios.Exercicio>();
}
