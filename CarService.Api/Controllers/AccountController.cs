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

using CarService.Api.Models;
using CarService.Api.Services;



namespace CarService.Api.Controllers
{
    public class UserDTO
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    
    
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        
        [HttpPost]
        public IActionResult Register()
        {
            return Json("Works!");
        }
        
        // JWT
          private List<User> users = new List<User>
        {
            new User {Email="admin@gmail.com", Password="12345", Role = "admin" },
            new User { Email="qwerty", Password="55555", Role = "user" }
        };
 
        [HttpPost("token")]
        public IActionResult Token([FromBody] UserDTO info)
        {

            var identity = GetIdentity(info.username, info.password);
            if (identity == null)
            {
                return BadRequest($"Invalid username or password.!!!! {info.username} {info.password}");
            }
 
            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
             
            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
 
           
            return Json(response);
        }
 
        private ClaimsIdentity GetIdentity(string username, string password)
        {
            User user = users.FirstOrDefault(x => x.Email == username && x.Password == password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
 
            // если пользователя не найдено
            return null;
        }
        
        
        
    }
}