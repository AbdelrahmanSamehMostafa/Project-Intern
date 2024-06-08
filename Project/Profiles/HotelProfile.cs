using AutoMapper;
using HotelBookingSystem.Models;


namespace HotelBookingSystem.Profiles
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
            CreateMap<Hotel, HotelDto>();
            CreateMap<HotelCreateDto, Hotel>();
            CreateMap<HotelDto, HotelUpdateDto>();
            CreateMap<HotelUpdateDto, Hotel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}