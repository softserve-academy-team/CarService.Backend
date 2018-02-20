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
    }
}