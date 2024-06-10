using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelBookingSystem.Models;
using Project.Models;

namespace HotelBookingSystem.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<Admin, AdminDTO>().ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ReverseMap()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => ExtractFirstName(src.Name)))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => ExtractLastName(src.Name)))
                .ForMember(dest => dest.SuperAdminId, opt => opt.MapFrom(src => 1));

            CreateMap<AdminForCreationDTO, Admin>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.SuperAdminId, opt => opt.MapFrom(src => 1));

            CreateMap<AdminForCreationDTO, AdminDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<AdminForUpdateDTO, Admin>()
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