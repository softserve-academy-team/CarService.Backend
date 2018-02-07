using Microsoft.AspNetCore.Mvc;
using CarService.Api.Models;
using CarService.Api.Services;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using CarService.DbAccess.Entities;

namespace CarService.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        [HttpPost("registration")]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerCredentials registerCustomerCredentials)
        {
            var result = await _accountService.AddUser(registerCustomerCredentials);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }

        [HttpPost("registration/mechanic")]
        public async Task<IActionResult> RegisterMechanic([FromBody] RegisterMechanicCredentials registerMechanicCredentials)
        {
            var result = await _accountService.AddUser(registerMechanicCredentials);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }
    }
}