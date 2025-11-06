using AutoMapper;
using GymTrackerApi.DTOs.ExercicioDTOs;
using GymTrackerApi.Models.Exercicios;

namespace GymTrackerApi.Profiles
{
    public class ExercicioProfile : Profile
    {
        public ExercicioProfile()
        {
            // DTO → Model
            CreateMap<CreateExercicioDTO, Exercicio>();
            CreateMap<UpdateExercicioDTO, Exercicio>();

            // Model → DTO
            CreateMap<Exercicio, ExercicioDTO>();
        }
    }
}
