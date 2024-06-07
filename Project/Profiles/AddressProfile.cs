using AutoMapper;
using HotelBookingSystem.Models;

namespace HotelBookingSystem.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressBaseDTO>();
            CreateMap<AddressBaseDTO,Address>();
        }
    }
}