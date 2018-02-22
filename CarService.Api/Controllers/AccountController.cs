using Microsoft.AspNetCore.Mvc;
using CarService.Api.Models;
using CarService.Api.Services;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;


namespace CarService.Api.Controllers
{

    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IEmailService   _emailService;
        private readonly IConfiguration _configuration;

        public AccountController(
            IAccountService accountService,
            IEmailService emailService,
            IConfiguration configuration)
        {
            _accountService = accountService;
            _emailService = emailService;
            _configuration = configuration;
        }

  
        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody] UserDTO info)
        {

            ClaimsIdentity identity = await _accountService.GetIdentity(info.email, info.password);
            if (identity == null)
            {
                return BadRequest($"Invalid username or password.!!!! {info.email} {info.password}");
            }
 
            var encodedJwt = _accountService.createJwtToken(identity);
             
            var response = new
            {
                access_token = encodedJwt
            };
 
           
            return Ok(response);
        }
      
        [HttpPost("registration/customer")]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerCredentials registerCustomerCredentials)
        {
            var result = await _accountService.RegisterCustomer(registerCustomerCredentials);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok();
        }

        [HttpPost("registration/mechanic")]
        public async Task<IActionResult> RegisterMechanic([FromBody] RegisterMechanicCredentials registerMechanicCredentials)
        {
            var result = await _accountService.RegisterMechanic(registerMechanicCredentials);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok();
        }

        [HttpGet("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var result = await _accountService.ConfirmEmail(userId, code);
            
            return Redirect(_configuration["Email:Redirect"]);
        }

    }
}
