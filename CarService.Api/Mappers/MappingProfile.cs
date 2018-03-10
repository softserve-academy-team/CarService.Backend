using AutoMapper;
using CarService.Api.Models;
using CarService.Api.Models.DTO;
using CarService.DbAccess.Entities;

namespace CarService.Api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<Mechanic, MechanicDto>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<MechanicDto, Mechanic>();
            CreateMap<RegisterMechanicCredentials, Mechanic>()
                .ForMember("WorkExperience", opt => opt.MapFrom(c => c.Experience))
                .ForMember("UserName", opt => opt.MapFrom(c => c.Email))
                .ForMember("City", opt => opt.MapFrom(c => c.Location));
            CreateMap<RegisterCustomerCredentials, Customer>()
                .ForMember("UserName", opt => opt.MapFrom(c => c.Email))
                .ForMember("City", opt => opt.MapFrom(c => c.Location));
        }
    }
}