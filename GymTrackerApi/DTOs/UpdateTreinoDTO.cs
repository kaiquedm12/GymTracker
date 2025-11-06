namespace GymTrackerApi.DTOs.TreinoDTOs
{
    using System;
    using System.Collections.Generic;

    public class UpdateTreinoDTO
    {
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public List<int> ExerciciosIds { get; set; }
    }
}
