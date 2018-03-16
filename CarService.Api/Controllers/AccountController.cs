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
        private readonly IEmailService _emailService;
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

        /// <summary>User Log In</summary>
        /// <returns>Return JWT token</returns>
        /// <remarks>Login user and generate JWT token</remarks>
        /// <param name="info">Login model</param> 
        /// <response code="200">Log In successful</response>
        /// <response code="400">Invalid username or password</response>
        /// <response code="455">Email registration not confirmed</response> 
        /// <response code="500">Internal Server Error</response> 
        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody] UserDTO info)
        {
            ClaimsIdentity identity = await _accountService.GetIdentity(info.email, info.password);

            if (identity == null)
                return BadRequest($"Invalid username or password.!!!! {info.email}");

            if (!await _accountService.IsEmailConfirmed(info.email))
                // return BadRequest("Please confirm your email.");
                return StatusCode(455);

            var encodedJwt = _accountService.createJwtToken(identity);

            var response = new
            {
                access_token = encodedJwt
            };

            return Ok(response);
        }

        /// <summary>Registration new customer</summary>
        /// <returns>an IActionResult</returns>
        /// <remarks>Registration new customer</remarks>
        /// <param name="registerCustomerCredentials">Customer registration model</param> 
        /// <response code="200">Registration successful</response>
        /// <response code="400">Invalid username or password</response>
        /// <response code="500">Internal Server Error</response> 
        [HttpPost("registration/customer")]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerCredentials registerCustomerCredentials)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _accountService.RegisterCustomer(registerCustomerCredentials);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok();
        }

        /// <summary>Registration new mechanic</summary>
        /// <returns>an IActionResult</returns>
        /// <remarks>Registration new mechanic</remarks>
        /// <param name="registerMechanicCredentials">Mechanic registration model</param> 
        /// <response code="200">Registration successful</response>
        /// <response code="400">Invalid username or password</response>
        /// <response code="500">Internal Server Error</response> 
        [HttpPost("registration/mechanic")]
        public async Task<IActionResult> RegisterMechanic([FromBody] RegisterMechanicCredentials registerMechanicCredentials)
        {
            if (!ModelState.IsValid)
                return BadRequest();
                
            var result = await _accountService.RegisterMechanic(registerMechanicCredentials);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok();
        }

        /// <summary>Confirm registration by email</summary>
        /// <returns>Return redirect to front-end page confirmed email</returns>
        /// <remarks>TODO detail description</remarks>
        /// <param name="userId">Id user in database</param> 
        /// <param name="code">Identity autogenerate code</param> 
        /// <response code="200">Email confirmed successful</response>
        /// <response code="455">Email registration not confirmed</response> 
        /// <response code="500">Internal Server Error</response> 
        [HttpGet("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var result = await _accountService.ConfirmEmail(userId, code);
            if (!result.Succeeded)
                return StatusCode(455);
            return Redirect(_configuration["Email:Redirect"]);
        }
    }
}
