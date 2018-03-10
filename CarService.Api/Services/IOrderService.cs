using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarService.Api.Models;
using CarService.Api.Models.DTO;
using CarService.DbAccess.Entities;

namespace CarService.Api.Services
{
    public interface IOrderService
    {
        Task CreateOrder(string email, OrderCreationDto orderDto);
        Task<CustomerOrderInfo> GetCustomerOrderInfo(string email, int orderId);
        Task AcceptReviewProposition(string email, AcceptReviewProposition acceptReviewProposition);
    }
}