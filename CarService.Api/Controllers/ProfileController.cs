using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarService.Api.Models.DTO;
using CarService.Api.Services;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Authorization;

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
        [Route("add")]
        public async Task<IActionResult> AddCarToFavourites()
        {
            string autoRiaId;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                autoRiaId = await reader.ReadToEndAsync();
            }
            string email = User.Identity.Name;
            if (email == null)
                return BadRequest(); //TODO: authorization failed

            await _profileService.AddCarToFavorites(email, int.Parse(autoRiaId));
            return Ok();
        }
    }
}