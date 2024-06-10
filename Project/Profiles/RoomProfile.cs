using AutoMapper;
using HotelBookingSystem.Models;

namespace HotelBookingSystem.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomDTO>().ReverseMap();
            
            CreateMap<Room, RoomWithIdDTO>().ReverseMap();
            
        }
    }
}