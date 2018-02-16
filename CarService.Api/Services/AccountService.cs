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
          private List<Models.User> users = new List<Models.User>
        {
            new Models.User {Email="mechanicn@gmail.com", Password="12345", Role = "mechanic" },
            new Models.User { Email="qwerty", Password="55555", Role = "user" }
        };

        private readonly AuthOptions _options;
        private readonly UserManager<Models.User> _userManager;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public AccountService(IOptions<AuthOptions> optionsAccessor, UserManager<Models.User> userManager, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _options = optionsAccessor.Value;
            this._userManager = userManager;
            this._unitOfWorkFactory = unitOfWorkFactory;
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

        public ClaimsIdentity GetIdentity(string username, string password)
        {
            Models.User user = users.FirstOrDefault(x => x.Email == username && x.Password == password);
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
                CardNumber = registerCustomerCredentials.CardNumber
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
                CardNumber = registerMechanicCredentials.CardNumber
            };

            var result = await _userManager.CreateAsync(user, registerMechanicCredentials.Password);
            if (!result.Succeeded)
                return result;

            return result;
        }

    }
}