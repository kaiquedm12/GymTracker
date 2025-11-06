using System.ComponentModel.DataAnnotations;

namespace GymTrackerApi.DTOs.ExercicioDTOs
{
    public class CreateExercicioDTO
    {
        [Required(ErrorMessage = "O nome do exercício é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome não pode ultrapassar 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Range(1, 1000, ErrorMessage = "As repetições devem estar entre 1 e 1000.")]
        public int Repeticoes { get; set; }

        [Range(0, 1000, ErrorMessage = "O peso deve ser um valor válido.")]
        public double Peso { get; set; }
    }
}
