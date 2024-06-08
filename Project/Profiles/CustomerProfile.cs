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
            CreateMap<Customer, CustomerDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<CustomerForCreationDTO, Customer>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));

            CreateMap<CustomerForCreationDTO, CustomerDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<CustomerDTO, Customer>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => ExtractFirstName(src.Name)))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => ExtractLastName(src.Name)));

            CreateMap<CustomerForUpdateDTO, Customer>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => ExtractFirstName(src.Name)))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => ExtractLastName(src.Name)));

            // Handle cases where fullName might not be in the expected format
            CreateMap<CustomerForUpdateDTO, Customer>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => ExtractFirstName(src.Name)))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => ExtractLastName(src.Name)))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => ExtractFirstName(src.Name) ?? ""))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => ExtractLastName(src.Name) ?? ""));
        }

        private string ExtractFirstName(string fullName)
        {
            return fullName?.Split(' ')[0];
        }

        private string ExtractLastName(string fullName)
        {
            var splitName = fullName?.Split(' ');
            return splitName != null && splitName.Length > 1 ? splitName[1] : "";
        }
    }

}