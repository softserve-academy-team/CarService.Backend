
using System;
using System.Threading.Tasks;
using AutoMapper;
using CarService.Api.Models.DTO;
using CarService.Api.Services;
using CarService.DbAccess.DAL;
using CarService.DbAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Api.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
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
    }
}