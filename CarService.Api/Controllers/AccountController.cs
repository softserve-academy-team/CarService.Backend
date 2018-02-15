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
        private readonly IEmailService   _emailService;

        public AccountController(
            IAccountService accountService,
            IEmailService emailService)
        {
            this._accountService = accountService;
                 _emailService = emailService;
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
            //if (userId == null || code == null)
            //{
            //    return RedirectToAction(nameof(HomeController.Index), "Home");
            //}
            //var user = await _userManager.FindByIdAsync(userId);
            //if (user == null)
            //{
            //    throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            //}
            //var result = await _userManager.ConfirmEmailAsync(user, code);


            //await this._hubContext.Clients.All.InvokeAsync("ConfirmEmail", "Message from web api", "Email confirmed");

            //return View(result.Succeeded ? "ConfirmEmail" : "Error");
            return Ok();
        }
    }
}