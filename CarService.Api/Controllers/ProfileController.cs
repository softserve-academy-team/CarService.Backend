using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarService.Api.Models.DTO;
using CarService.Api.Services;

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

        [HttpGet]
        [Route("user-info")]
        public async Task<IActionResult> GetUserInfo()
        {
            var user = new CustomerDTO {
                Email = "vasia@gmail.com",
                FirstName = "Vasia",
                LastName = "Pupkin",
                PhoneNumber = "+380551243219",
                City = "Kyiv",
                CardNumber = "**** 9012"
            };

            return Ok(user);
        }

        [HttpPost]
        [Route("edit/customer")]
        public async Task<IActionResult> EditCustomerInfo([FromBody] CustomerDTO customerDTO)
        {
            await _profileService.EditCustomerProfile(customerDTO);
            return Ok();
        }

        [HttpPost]
        [Route("edit/mechanic")]
        public async Task<IActionResult> EditMechanicInfo([FromBody] MechanicDTO mechanicDTO)
        {
            await _profileService.EditMechanicProfile(mechanicDTO);
            return Ok();
        }
    }
}