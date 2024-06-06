using AutoMapper;
using HotelBookingSystem.Data.Models;

public class HotelProfile : Profile
{
    public HotelProfile()
    {
        CreateMap<Hotel, HotelDto>();
        CreateMap<HotelCreateDto, Hotel>();
        CreateMap<HotelUpdateDto, Hotel>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}