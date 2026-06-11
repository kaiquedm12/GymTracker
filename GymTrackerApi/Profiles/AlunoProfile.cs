using AutoMapper;
using GymTrackerApi.DTOs.AlunoDTOs;
using GymTrackerApi.Models.Pessoas;

namespace GymTrackerApi.Profiles
{
    public class AlunoProfile : Profile
    {
        public AlunoProfile()
        {
            CreateMap<CreateAlunoDTO, Aluno>();
            CreateMap<UpdateAlunoDTO, Aluno>();
            CreateMap<Aluno, AlunoDTO>();
        }
    }
}
