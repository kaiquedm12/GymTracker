namespace GymTrackerApi.DTOs.TreinoDTOs
{
    using System;
    using System.Collections.Generic;

    public class CreateTreinoDTO
    {
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public List<int> ExerciciosIds { get; set; } // IDs dos exercícios que o treino vai incluir
    }
}
