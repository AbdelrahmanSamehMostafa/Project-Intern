using AutoMapper;
using HotelBookingSystem.Models;


namespace HotelBookingSystem.Profiles
{
    public class BookingProfile : Profile
    {
    public BookingProfile()
    {

        CreateMap<Booking, BookingDTO>().ReverseMap();
        CreateMap<Booking, BookingBaseDTO>().ReverseMap();
    }
}
}
