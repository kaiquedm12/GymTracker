using AutoMapper;
using GymTrackerApi.DTOs.ExercicioDTOs;
using GymTrackerApi.DTOs.TreinoExercicioDTOs;
using GymTrackerApi.Models.Exercicios;
using GymTrackerApi.Models.Treinos;
using GymTrackerApi.Models.Relacionamentos;
using GymTrackerApi.DTOs.TreinoDTOs;

namespace GymTrackerApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Treino, TreinoDTO>().ReverseMap();
            CreateMap<Exercicio, ExercicioDTO>().ReverseMap();
            CreateMap<TreinoExercicio, TreinoExercicioDTO>().ReverseMap();
        }
    }
}
