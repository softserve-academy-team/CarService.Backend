using System.Threading.Tasks;
using System.Collections.Generic;
using CarService.Api.Models;
using CarService.Api.Models.DTO;

namespace CarService.Api.Services
{
    public interface IOrderService
    {
        Task CreateOrder(string email, OrderCreationDto orderDto);
        Task CreateReviewProposition(string email, ReviewPropositionCreationDto reviewPropositionDto);
        Task<IEnumerable<string>> GetCitiesAsync();
        Task<IEnumerable<BaseOrderInfo>> GetOrdersAsync(OrderSearchModel orderSearchModel, int skip, int take);
    }
}