using AutoMapper;
using GymTrackerApi.DTOs.TreinoDTOs;
using GymTrackerApi.DTOs.ExercicioDTOs;
using GymTrackerApi.Models.Treinos;
using GymTrackerApi.Models.Exercicios;
using GymTrackerApi.Models.Relacionamentos;

namespace GymTrackerApi.Profiles
{
    public class TreinoProfile : Profile
    {
        public TreinoProfile()
        {
            CreateMap<Treino, TreinoDTO>()
                .ForMember(dest => dest.Exercicios, opt => opt.MapFrom(src =>
                    src.TreinoExercicio.Select(te => te.Exercicio)));

            CreateMap<Exercicio, ExercicioDTO>();

            CreateMap<CreateTreinoDTO, Treino>()
                .ForMember(dest => dest.TreinoExercicio, opt => opt.Ignore());

            CreateMap<UpdateTreinoDTO, Treino>()
                .ForMember(dest => dest.TreinoExercicio, opt => opt.Ignore());
        }
    }
}
