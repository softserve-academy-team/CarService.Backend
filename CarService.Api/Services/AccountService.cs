using System;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using CarService.Api.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;



namespace CarService.Api.Services
{

    public class AccountService : IAccountService
    {
        // later to get rid of
          private List<User> users = new List<User>
        {
            new User {Email="mechanicn@gmail.com", Password="12345", Role = "mechanic" },
            new User { Email="qwerty", Password="55555", Role = "user" }
        };

       private readonly AuthOptions _options;

        public AccountService(IOptions<AuthOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
        }

        public string createJwtToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            System.Console.WriteLine("_options.Issuer {0}", _options.Issuer);
            System.Console.WriteLine("_options.Lifetime {0}", _options.Lifetime);
            System.Console.WriteLine("_options.Audience {0}", _options.Audience);
           
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