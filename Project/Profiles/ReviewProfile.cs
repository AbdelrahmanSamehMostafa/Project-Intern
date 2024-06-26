using AutoMapper;
using HotelBookingSystem.Models;

namespace HotelBookingSystem.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<ReviewForCreationDTO, Review>();
            CreateMap<ReviewDTO, Review>().ReverseMap();
            CreateMap<Review, ReviewWithIdDTO>().ReverseMap();
            CreateMap<ReviewWithHotelIdDTO, Review>().ReverseMap();
        }
    }
}