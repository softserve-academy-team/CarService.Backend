using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarService.Api.Models.DTO;

namespace CarService.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProfileController : Controller
    {
        [HttpGet]
        [Route("user-info")]
        public async Task<IActionResult> GetUserInfo()
        {
            var user = new MechanicDTO {
                Email = "vasia@gmail.com",
                FirstName = "Vasia",
                LastName = "Pupkin",
                PhoneNumber = "+380551243219",
                City = "Kyiv",
                CardNumber = "**** 9012",
                WorkExperience = 9,
                Specialization = "Audi, Volkswagen, Skoda"
            };

            return Ok(user);
        }

        [HttpPost]
        [Route("edit/customer")]
        public async Task<IActionResult> EditCustomerInfo([FromBody] CustomerDTO customerDTO)
        {
            return Ok();
        }

        [HttpPost]
        [Route("edit/mechanic")]
        public async Task<IActionResult> EditCustomerInfo([FromBody] MechanicDTO mechanicDTO)
        {
            return Ok();
        }
    }
}