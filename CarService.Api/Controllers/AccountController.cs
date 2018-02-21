using System;
using Microsoft.AspNetCore.Mvc;
using CarService.Api.Models;
using CarService.Api.Services;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;



using CarService.DbAccess.Entities;

namespace CarService.Api.Controllers
{ 
    
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
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
                access_token = encodedJwt,
                username = identity.Name
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
 
        
    }
}
