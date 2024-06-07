using AutoMapper;
using HotelBookingSystem.Models;

namespace HotelBookingSystem.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<AddressForCreationDTO, Address>();
            CreateMap<Address, AddressWithIdDTO>();
            CreateMap<AddressWithIdDTO, Address>();
            // CreateMap<Address, AddressWithHotelNameDTO>()
            // .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Hotel.Name));
        }
    }
}