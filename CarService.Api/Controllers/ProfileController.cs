using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarService.Api.Models.DTO;
using CarService.Api.Services;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace CarService.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;
        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        /// <summary>Get info about registered user</summary>
        /// <returns>Return registered user</returns>
        /// <remarks>Get and view information about registered user by user email</remarks> 
        /// <response code="200">Successful</response>
        /// <response code="400">User not found</response> 
        /// <response code="401">Unauthorized user</response> 
        /// <response code="500">Internal Server Error</response> 
        [Authorize]
        [HttpGet]
        [Route("user-info")]
        public async Task<IActionResult> GetUserInfo()
        {
            string email = User.Identity.Name;
            if (email == null)
                return Unauthorized();

            var user = await _profileService.GetUserDTO(email);

            if (user == null)
                return BadRequest();

            return Ok(user);
        }

        /// <summary>Edit customer</summary>
        /// <returns>an IActionResult</returns>
        /// <remarks>Edit information about customer in database</remarks> 
        /// <param name="customerDTO">Customers' model</param> 
        /// <response code="200">Successful</response>
        /// <response code="400">Customer not found</response> 
        /// <response code="401">Unauthorized user</response> 
        /// <response code="500">Internal Server Error</response> 
        [Authorize]
        [HttpPut]
        [Route("edit/customer")]
        public async Task<IActionResult> EditCustomerInfo([FromBody] CustomerDTO customerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            string email = User.Identity.Name;
            if (email == null)
                return Unauthorized();

            await _profileService.EditCustomerProfile(email, customerDTO);
            return Ok();
        }

        /// <summary>Edit mechanic</summary>
        /// <returns>an IActionResult</returns>
        /// <remarks>Edit information about mechanic in database</remarks> 
        /// <param name="mechanicDTO">Mechanics' model</param> 
        /// <response code="200">Successful</response>
        /// <response code="400">Mechanic not found</response> 
        /// <response code="401">Unauthorized user</response> 
        /// <response code="500">Internal Server Error</response> 
        [Authorize]
        [HttpPut]
        [Route("edit/mechanic")]
        public async Task<IActionResult> EditMechanicInfo([FromBody] MechanicDTO mechanicDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            string email = User.Identity.Name;
            if (email == null)
                return Unauthorized();

            await _profileService.EditMechanicProfile(email, mechanicDTO);
            return Ok();
        }

        /// <summary>Add auto to favorites</summary>
        /// <returns>an IActionResult</returns>
        /// <remarks>Add auto to favorite list by auto Id</remarks> 
        /// <response code="200">Successful</response>
        /// <response code="400">Auto not found</response> 
        /// <response code="401">Unauthorized user</response> 
        /// <response code="500">Internal Server Error</response> 
        [Authorize]
        [HttpPost]
        [Route("favorites/add")]
        public async Task<IActionResult> AddCarToFavorites()
        {
            string autoRiaId;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                autoRiaId = await reader.ReadToEndAsync();
            }

            string email = User.Identity.Name;
            if (email == null)
                return Unauthorized();

            await _profileService.AddCarToFavorites(email, int.Parse(autoRiaId));
            return Ok();
        }

        /// <summary>Delete auto from favorites</summary>
        /// <returns>an IActionResult</returns>
        /// <remarks>Delete auto from favorite list by auto Id</remarks> 
        /// <response code="200">Successful</response>
        /// <response code="400">Auto not found</response> 
        /// <response code="401">Unauthorized user</response> 
        /// <response code="500">Internal Server Error</response> 
        [Authorize]
        [HttpPost]
        [Route("favorites/delete")]
        public async Task<IActionResult> DeleteCarFromFavorites()
        {
            string autoRiaId;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                autoRiaId = await reader.ReadToEndAsync();
            }

            string email = User.Identity.Name;
            if (email == null)
                return Unauthorized();

            await _profileService.DeleteCarFromFavorites(email, int.Parse(autoRiaId));
            return Ok();
        }

        /// <summary>Get all auto from favorites</summary>
        /// <returns>an IActionResult</returns>
        /// <remarks>Get and list all auto from favorites</remarks> 
        /// <response code="200">Successful</response>
        /// <response code="400">Auto not found</response> 
        /// <response code="401">Unauthorized user</response> 
        /// <response code="500">Internal Server Error</response> 
        [Authorize]
        [HttpGet]
        [Route("favorites/get")]
        public async Task<IActionResult> GetAllCarsFromFavorites()
        {
            string email = User.Identity.Name;
            if (email == null)
                return Unauthorized();

            var cars = await _profileService.GetAllCarsFromFavorites(email);
            if (cars == null)
                return BadRequest();

            return Ok(cars);
        }

        /// <summary>Checks whether car is in favorites</summary>
        /// <returns>an IActionResult</returns>
        /// <remarks>Return true if car is in favorites and false otherwise</remarks> 
        /// <response code="200">Successful</response>
        /// <response code="400">Auto not found</response> 
        /// <response code="401">Unauthorized user</response> 
        /// <response code="500">Internal Server Error</response> 
        [Authorize]
        [HttpPost]
        [Route("favorites/isCarInFavorites")]
        public async Task<IActionResult> IsCarInFavorites()
        {
            string autoRiaId;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                autoRiaId = await reader.ReadToEndAsync();
            }

            string email = User.Identity.Name;
            if (email == null)
                return Unauthorized();

            bool isCar = await _profileService.IsCarInFavorites(email, int.Parse(autoRiaId));

            return Ok(isCar);
        }

        /// <summary>Get all created orders</summary>
        /// <returns>an IActionResult</returns>
        /// <remarks>Return list all created orders this registered user</remarks> 
        /// <response code="200">Successful</response>
        /// <response code="400">Orders not found</response> 
        /// <response code="401">Unauthorized user</response> 
        /// <response code="500">Internal Server Error</response> 
        [Authorize]
        [HttpGet]
        [Route("created-orders")]
        public async Task<IActionResult> GetUserCreatedOrders()
        {
            string email = User.Identity.Name;
            if (email == null)
                return Unauthorized();

            IEnumerable<ProfileOrderInfo> orderList = await _profileService.GetUserCreatedOrders(email);
            return Ok(orderList);
        }

        /// <summary>Get all applied orders</summary>
        /// <returns>an IActionResult</returns>
        /// <remarks>Return list all applied orders this registered user</remarks> 
        /// <response code="200">Successful</response>
        /// <response code="400">Orders not found</response> 
        /// <response code="401">Unauthorized user</response> 
        /// <response code="500">Internal Server Error</response> 
        [Authorize]
        [HttpGet]
        [Route("applied-orders")]
        public async Task<IActionResult> GetUserAppliedOrders()
        {
            string email = User.Identity.Name;
            if (email == null)
                return Unauthorized();

            IEnumerable<ProfileOrderInfo> orderList = await _profileService.GetUserAppliedOrders(email);
            return Ok(orderList);
        }
    }
}