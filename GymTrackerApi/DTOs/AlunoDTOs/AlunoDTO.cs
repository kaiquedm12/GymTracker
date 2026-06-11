namespace GymTrackerApi.DTOs.AlunoDTOs
{
    public class AlunoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; }
        public int PersonalId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
