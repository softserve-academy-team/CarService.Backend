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
        readonly UserManager<AccountCustomer> _userManager;
        public AccountController(UserManager<AccountCustomer> userManager)
        {
            this._userManager = userManager;
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Register([FromBody] RegisterCustomerCredentials registerCustomerCredentials)
        {
            AccountCustomer accountCustomer = new AccountCustomer {
                Email = registerCustomerCredentials.Email,
                UserName = registerCustomerCredentials.Email,
                FirstName = registerCustomerCredentials.FirstName,
                LastName = registerCustomerCredentials.LastName,
                PhoneNumber = registerCustomerCredentials.PhoneNumber,
                City = registerCustomerCredentials.City  
            };
            var result = await _userManager.CreateAsync(accountCustomer, registerCustomerCredentials.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok();
        }
    }
}