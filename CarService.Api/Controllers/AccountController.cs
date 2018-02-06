using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace CarService.Api.Controllers
{
    public class Credentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }


    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        // POST api/account
        [HttpPost]
        public IActionResult Register()
        {
            // var user = new IdentityUser {UserName = credentials.Email, Email = credentials.Email };          
                
            
            // var result = await userManager.CreateAsync(user, credentials.Password);
            // if(!result.Succeeded)
            //     return BadRequest(result.Errors);
            // await signInManager.SignInAsync(user, isPersistent: false);
            var jwt = new JwtSecurityToken();
            var tokenHandler = new JwtSecurityTokenHandler();
            return Ok(tokenHandler.WriteToken(jwt));
        }
        [HttpGet]
        public IActionResult Ropister()
        {
            // var user = new IdentityUser {UserName = credentials.Email, Email = credentials.Email };          
                
            
            // var result = await userManager.CreateAsync(user, credentials.Password);
            // if(!result.Succeeded)
            //     return BadRequest(result.Errors);
            // await signInManager.SignInAsync(user, isPersistent: false);
            var jwt = new JwtSecurityToken();
            var tokenHandler = new JwtSecurityTokenHandler();
            return Ok(tokenHandler.WriteToken(jwt));
        }

       
    }
}
