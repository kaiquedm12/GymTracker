namespace GymTrackerApi.Models.Treinos;
using GymTrackerApi.Models.Exercicios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GymTrackerApi.Models.Relacionamentos;

public class Treino
{
    [Key]
    public int Id { get; set; }
    [Required (ErrorMessage = "O nome do treino é obrigatório.")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "A data do treino é obrigatória.")]
    public DateTime Data { get; set; } = DateTime.UtcNow;
    [Required(ErrorMessage = "A lista de exercícios é obrigatória.")]
    [MaxLength(500, ErrorMessage = "A lista de exercícios não pode exceder 500 caracteres.")]
    public string ListaExercicios { get; set; } = string.Empty;

    // Um treino tem vários exercícios
    public List<Exercicio> Exercicios { get; set; } = new();
    public ICollection<TreinoExercicio> TreinosExercicios { get; set; }


}
