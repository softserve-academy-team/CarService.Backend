using System.Threading.Tasks;
using CarService.Api.Models.DTO;
using CarService.DbAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Api.Services
{
    public interface IOrderService
    {
        Task CreateOrder(string email, OrderCreationDto orderDto);
    }
}