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

        [Authorize]
        [HttpGet]
        [Route("user-info")]
        public async Task<IActionResult> GetUserInfo()
        {
            string email = User.Identity.Name;
            if (email == null)
                return BadRequest();

            var user = await _profileService.GetUserDTO(email);

            if (user == null)
                return BadRequest();

            return Ok(user);
        }

        [Authorize]
        [HttpPut]
        [Route("edit/customer")]
        public async Task<IActionResult> EditCustomerInfo([FromBody] CustomerDTO customerDTO)
        {
            await _profileService.EditCustomerProfile(customerDTO);
            return Ok();
        }

        [Authorize]
        [HttpPut]
        [Route("edit/mechanic")]
        public async Task<IActionResult> EditMechanicInfo([FromBody] MechanicDTO mechanicDTO)
        {
            await _profileService.EditMechanicProfile(mechanicDTO);
            return Ok();
        }

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
                return BadRequest();

            await _profileService.AddCarToFavorites(email, int.Parse(autoRiaId));
            return Ok();
        }

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
                return BadRequest();

            await _profileService.DeleteCarFromFavorites(email, int.Parse(autoRiaId));
            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("favorites/get")]
        public async Task<IActionResult> GetAllCarsFromFavorites()
        {
            string email = User.Identity.Name;
            if (email == null)
                return BadRequest();

            var cars = await _profileService.GetAllCarsFromFavorites(email);
            if (cars == null)
                return BadRequest();

            return Ok(cars);
        }

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
                return BadRequest();

            bool isCar = await _profileService.IsCarInFavorites(email, int.Parse(autoRiaId));

            return Ok(isCar);
        }

        [Authorize]
        [HttpGet]
        [Route("created-orders")]
        public async Task<IActionResult> GetUserCreatedOrders()
        {
            string email = User.Identity.Name;
            if (email == null)
                return BadRequest();

            IEnumerable<ProfileOrderInfo> orderList = await _profileService.GetUserCreatedOrders(email);
            return Ok(orderList);
        }

        [Authorize]
        [HttpGet]
        [Route("applied-orders")]
        public async Task<IActionResult> GetUserAppliedOrders()
        {
            string email = User.Identity.Name;
            if (email == null)
                return BadRequest();

            IEnumerable<ProfileOrderInfo> orderList = await _profileService.GetUserAppliedOrders(email);
            return Ok(orderList);
        }
    }
}