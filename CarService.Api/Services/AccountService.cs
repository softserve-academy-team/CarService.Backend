using System;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using CarService.Api.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CarService.DbAccess.Entities;
using CarService.DbAccess.DAL;


namespace CarService.Api.Services
{

    public class AccountService : IAccountService
    {
        private const string MECHANIC_ROLE = "mechanic";
        private const string СUSTOMER_ROLE = "customer";
        private readonly AuthOptions _options;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public AccountService(IOptions<AuthOptions> optionsAccessor,
         UserManager<User> userManager, 
         SignInManager<User> signInManager,
         IUnitOfWorkFactory unitOfWorkFactory)
        {
            _options = optionsAccessor.Value;
            _userManager = userManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public string createJwtToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
          
            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(_options.Lifetime)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.Key)), SecurityAlgorithms.HmacSha256));
            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        public async Task<ClaimsIdentity> GetIdentity(string email, string password)
        {
            User user = await _userManager.FindByEmailAsync(email);
            await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);
           
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
 
            return null;
        }

        public async Task<IdentityResult> RegisterCustomer(RegisterCustomerCredentials registerCustomerCredentials)
        {
            var user = new Customer
            {
                Email = registerCustomerCredentials.Email,
                UserName = registerCustomerCredentials.Email,
                PhoneNumber = registerCustomerCredentials.PhoneNumber,
                Status = UserStatus.Inactive,
                FirstName = registerCustomerCredentials.FirstName,
                LastName = registerCustomerCredentials.LastName,
                RegisterDate = DateTime.Now.ToUniversalTime(),
                City = registerCustomerCredentials.City,
                CardNumber = registerCustomerCredentials.CardNumber,
                Role = СUSTOMER_ROLE
            };

            var result = await _userManager.CreateAsync(user, registerCustomerCredentials.Password);
            if (!result.Succeeded)
                return result;

            return result;
        }

        public async Task<IdentityResult> RegisterMechanic(RegisterMechanicCredentials registerMechanicCredentials)
        {
            var user = new Mechanic
            {
                Email = registerMechanicCredentials.Email,
                UserName = registerMechanicCredentials.Email,
                PhoneNumber = registerMechanicCredentials.PhoneNumber,
                Status = UserStatus.Inactive,
                FirstName = registerMechanicCredentials.FirstName,
                LastName = registerMechanicCredentials.LastName,
                RegisterDate = DateTime.Now.ToUniversalTime(),
                City = registerMechanicCredentials.City,
                WorkExperience = registerMechanicCredentials.WorkExperience,
                CardNumber = registerMechanicCredentials.CardNumber,
                Role = MECHANIC_ROLE
            };

            var result = await _userManager.CreateAsync(user, registerMechanicCredentials.Password);
            if (!result.Succeeded)
                return result;

            return result;
        }

    }
}