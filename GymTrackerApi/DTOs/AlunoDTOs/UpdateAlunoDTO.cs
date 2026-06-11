namespace GymTrackerApi.DTOs.AlunoDTOs
{
    public class UpdateAlunoDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; }
    }
}
