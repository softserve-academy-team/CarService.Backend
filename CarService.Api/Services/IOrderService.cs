using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CarService.Api.Models;
using CarService.Api.Models.DTO;
using CarService.DbAccess.Entities;

namespace CarService.Api.Services
{
    public interface IOrderService
    {
        Task CreateOrder(string email, OrderCreationDto orderDto);
        Task CreateReviewProposition(string email, ReviewPropositionCreationDto reviewPropositionDto);
        Task<IEnumerable<string>> GetCitiesAsync();
        Task<IEnumerable<BaseOrderInfo>> GetOrdersAsync(OrderSearchModel orderSearchModel, int skip, int take);
        Task<CustomerOrderInfo> GetCustomerOrderInfo(string email, int orderId);
        Task<MechanicOrderInfo> GetMechanicOrderInfo(string email, int orderId);
        Task AcceptReviewProposition(string email, AcceptReviewProposition acceptReviewProposition);
    }
}