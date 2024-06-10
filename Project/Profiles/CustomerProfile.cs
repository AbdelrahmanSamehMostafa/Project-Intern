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

            // Map from Customer to CustomerDTO and reverse
            CreateMap<Customer, CustomerDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ReverseMap()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => ExtractFirstName(src.Name)))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => ExtractLastName(src.Name)));

            // Map from CustomerForCreationDTO to Customer and reverse
            CreateMap<CustomerForCreationDTO, Customer>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ReverseMap();

            // Map from CustomerForCreationDTO to CustomerDTO
            CreateMap<CustomerForCreationDTO, CustomerDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ReverseMap()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => ExtractFirstName(src.Name)))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => ExtractLastName(src.Name)));

            // Map from CustomerForUpdateDTO to Customer
            CreateMap<CustomerForUpdateDTO, Customer>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => ExtractFirstName(src.Name)))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => ExtractLastName(src.Name)))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => ExtractFirstName(src.Name) ?? ""))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => ExtractLastName(src.Name) ?? ""))
                .ReverseMap();
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