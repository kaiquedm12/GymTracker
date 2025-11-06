using AutoMapper;
using GymTrackerApi.DTOs.TreinoDTOs;
using GymTrackerApi.DTOs.ExercicioDTOs;
using GymTrackerApi.Models.Treinos;
using GymTrackerApi.Models.Exercicios;
using GymTrackerApi.Models.Relacionamentos;
using System.Linq;

namespace GymTrackerApi.Profiles
{
    public class TreinoProfile : Profile
    {
        public TreinoProfile()
        {
            // === Treino → TreinoDTO ===
            CreateMap<Treino, TreinoDTO>()
                .ForMember(dest => dest.Exercicios, opt => opt.MapFrom(src =>
                    src.TreinoExercicio.Select(te => te.Exercicio)));

            // === Exercicio → ExercicioDTO ===
            CreateMap<Exercicio, ExercicioDTO>();

            // === DTO → Model (para criação/atualização) ===
            CreateMap<CreateTreinoDTO, Treino>()
                .ForMember(dest => dest.TreinoExercicio, opt => opt.Ignore()); // vai ser montado manualmente no controller

            CreateMap<UpdateTreinoDTO, Treino>()
                .ForMember(dest => dest.TreinoExercicio, opt => opt.Ignore());
        }
    }
}
