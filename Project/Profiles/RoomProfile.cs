using AutoMapper;
using HotelBookingSystem.Data.Models;

namespace HotelBookingSystem.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<RoomDTO, Room>();
            CreateMap<Room, RoomDTO>();
            CreateMap<RoomWithIdDTO, Room>();
            CreateMap<Room, RoomWithIdDTO>();
        }
    }
}