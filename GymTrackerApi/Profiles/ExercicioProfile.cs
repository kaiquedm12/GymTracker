using AutoMapper;
using GymTrackerApi.DTOs.ExercicioDTOs;
using GymTrackerApi.Models.Exercicios;

namespace GymTrackerApi.Profiles
{
    public class ExercicioProfile : Profile
    {
        public ExercicioProfile()
        {
            CreateMap<CreateExercicioDTO, Exercicio>();
            CreateMap<UpdateExercicioDTO, Exercicio>();
            CreateMap<Exercicio, ExercicioDTO>();
        }
    }
}
