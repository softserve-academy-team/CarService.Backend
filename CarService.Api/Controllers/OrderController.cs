using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CarService.Api.Models;
using CarService.Api.Models.DTO;
using CarService.Api.Services;
using CarService.DbAccess.DAL;
using CarService.DbAccess.Entities;


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

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody]OrderCreationDto orderDto)
        {
            string email = User.Identity.Name;
            if (email == null)
                return BadRequest();

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

        [Authorize]
        [HttpGet("order-info/{orderId}")]
        public async Task<IActionResult> GetCustomerOrderInfo(int orderId)
        {
            string email = User.Identity.Name;
            if (email == null)
                return BadRequest();

            var orderInfo = await _orderService.GetCustomerOrderInfo(email, orderId);

            if (orderInfo == null)
                return NotFound();

            return Ok(orderInfo);
        }

        [Authorize]
        [HttpPut("accept-proposition")]
        public async Task<IActionResult> AcceptReviewProposition([FromBody] AcceptReviewProposition acceptReviewProposition)
        {
            string email = User.Identity.Name;
            if (email == null)
                return BadRequest();

            await _orderService.AcceptReviewProposition(email, acceptReviewProposition);

            return Ok();
        }
    }
}