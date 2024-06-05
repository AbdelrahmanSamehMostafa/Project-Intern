using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelBookingSystem.Models;

namespace HotelBookingSystem.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDTO>().ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<CustomerForCreationDTO, Customer>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));

            CreateMap<CustomerForCreationDTO, CustomerDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<CustomerDTO, Customer>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => ExtractFirstName(src.Name)))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => ExtractLastName(src.Name)));
        }
        private string ExtractFirstName(string fullName)
        {
            return fullName.Split(' ')[0];
        }

        private string ExtractLastName(string fullName)
        {
            return fullName.Split(' ')[1];
        }
    }
}