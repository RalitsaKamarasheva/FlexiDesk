using AutoMapper;
using FlexiDesk.API.Models;
using FlexiDesk.Domain.Entities;

namespace FlexiDesk.API.Mappers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            // От DTO към Entity
            CreateMap<CreateReservationRequest, Reservation>();

            // От Entity към DTO
            CreateMap<Reservation, ReservationResponse>()
            .ForMember(dest => dest.ResourceName,
                       opt => opt.MapFrom(src => src.Resource.Name));
        }
    }
}
