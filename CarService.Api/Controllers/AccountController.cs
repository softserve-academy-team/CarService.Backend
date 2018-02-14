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




namespace CarService.Api.Controllers
{ 
    
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public IActionResult Register()
        {
            return Ok("Works!");
        }
   
 
        [HttpPost("token")]
        public IActionResult Token([FromBody] UserDTO info)
        {

            ClaimsIdentity identity = _accountService.GetIdentity(info.email, info.password);
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
 
        
        
        
    }
}