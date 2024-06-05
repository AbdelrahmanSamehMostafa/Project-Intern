using AutoMapper;
using HotelBookingSystem.Data.Models;

namespace HotelBookingSystem.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<ReviewForCreationDTO, Review>();
            CreateMap<ReviewDTO, Review>();
            CreateMap<Review, ReviewDTO>();
            CreateMap<Review, ReviewWithIdDTO>();   
            CreateMap<ReviewWithIdDTO, Review>();
        }
    }
}