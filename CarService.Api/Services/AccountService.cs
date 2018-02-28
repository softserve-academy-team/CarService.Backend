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
using Microsoft.AspNetCore.Identity;
using CarService.DbAccess.Entities;
using CarService.DbAccess.DAL;
using Microsoft.Extensions.Configuration;
using System.Web;
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

        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;


        public AccountService(
            IOptions<AuthOptions> optionsAccessor,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IUnitOfWorkFactory unitOfWorkFactory,
            IEmailService emailService,
            IConfiguration configuration)
        {
            _options = optionsAccessor.Value;
            _userManager = userManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _unitOfWorkFactory = unitOfWorkFactory;
            _emailService = emailService;
            _configuration = configuration;
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

            if (user != null)
            {
                var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);
                if (!signInResult.Succeeded)
                    return null;

                string role = user is Mechanic ? MECHANIC_ROLE : СUSTOMER_ROLE;

                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }

        public async Task<bool> IsEmailConfirmed(string email)
        {
            User user = await _userManager.FindByEmailAsync(email);
            return (user != null) ? await _userManager.IsEmailConfirmedAsync(user) : false; 
        }

        public async Task<IdentityResult> RegisterCustomer(RegisterCustomerCredentials registerCustomerCredentials)
        {
            var user = new Customer
            {
                Email = registerCustomerCredentials.Email,
                UserName = registerCustomerCredentials.Email,
                Status = UserStatus.Inactive,
                FirstName = registerCustomerCredentials.FirstName,
                LastName = registerCustomerCredentials.LastName,
                RegisterDate = DateTime.Now.ToUniversalTime(),
                City = registerCustomerCredentials.Location
            };

            return await AddUser(user, registerCustomerCredentials.Password);
        }

        public async Task<IdentityResult> RegisterMechanic(RegisterMechanicCredentials registerMechanicCredentials)
        {
            var user = new Mechanic
            {
                Email = registerMechanicCredentials.Email,
                UserName = registerMechanicCredentials.Email,
                Status = UserStatus.Inactive,
                FirstName = registerMechanicCredentials.FirstName,
                LastName = registerMechanicCredentials.LastName,
                RegisterDate = DateTime.Now.ToUniversalTime(),
                City = registerMechanicCredentials.Location,
                WorkExperience = registerMechanicCredentials.Experience,
                Specialization = registerMechanicCredentials.Specialization
            };

            return await AddUser(user, registerMechanicCredentials.Password);
        }

        public async Task<IdentityResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            return result;
        }

        private async Task<IdentityResult> AddUser(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return result;

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = HttpUtility.UrlEncode(code);

            var callBackUrl = new UriBuilder();
            callBackUrl.Scheme = _configuration["Email:Scheme"];
            callBackUrl.Host = _configuration["Email:Host"];
            callBackUrl.Port = Convert.ToInt32(_configuration["Email:Port"]);
            callBackUrl.Path = _configuration["Email:EmailPath"];

            var userIdentityParams = new Dictionary<string, string>();
            userIdentityParams.Add("userId", user.Id.ToString());
            userIdentityParams.Add("code", code);
            var stringBuilder = new StringBuilder();
            callBackUrl.Query = stringBuilder.AppendJoin("&", userIdentityParams.Select(iup => $"{iup.Key}={iup.Value}")).ToString();

            await _emailService.SendEmailAsync(user.Email, "Confirm your account",
                           $"Please confirm your account by clicking : <a href='{callBackUrl.Uri}'>Confirm Car Service account</a>");

            return result;
        }
    }
}