using AutoMapper;
using GymTrackerApi.DTOs.PersonalDTOs;
using GymTrackerApi.Models.Pessoas;

namespace GymTrackerApi.Profiles
{
    public class PersonalProfile : Profile
    {
        public PersonalProfile()
        {
            CreateMap<CreatePersonalDTO, Personal>();
            CreateMap<UpdatePersonalDTO, Personal>();
            CreateMap<Personal, PersonalDTO>();
        }
    }
}
