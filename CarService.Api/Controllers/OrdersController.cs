using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CarService.Api.Models.DTO;
using CarService.Api.Models;
using CarService.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace CarService.Api.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>Create users order</summary>
        /// <returns>an IActionResult</returns>
        /// <remarks>TODO detail description</remarks>
        /// <param name="orderDto">Orders' model</param> 
        /// <response code="200">Order create successful</response>
        /// <response code="401">Unauthorized user</response> 
        /// <response code="500">Internal Server Error</response> 
        [Authorize]
        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody]OrderCreationDto orderDto)
        {
            string email = User.Identity.Name;
            if (email == null)
                return Unauthorized();

            await _orderService.CreateOrder(email, orderDto);
            return Ok();
        }

        [HttpGet("cities")]
        public async Task<IEnumerable<string>> GetCities()
        {
            return await _orderService.GetCitiesAsync();
        }

        [HttpPost]
        public async Task<IEnumerable<BaseOrderInfo>> GetOrders([FromBody]OrderSearchModel orderSearchModel, [FromQuery(Name = "skip")]int skip, [FromQuery(Name = "take")]int take)
        {
            return await this._orderService.GetOrdersAsync(orderSearchModel, skip, take);
        }
    }
}