using AutoMapper;
using CarService.Api.Models.DTO;
using CarService.DbAccess.Entities;

namespace CarService.Api.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDTO>();
            CreateMap<Mechanic, MechanicDTO>();
            CreateMap<CustomerDTO, Customer>();
            CreateMap<MechanicDTO, Mechanic>();
        }
    }
}