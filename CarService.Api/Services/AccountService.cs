using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CarService.DbAccess.Entities;
using CarService.Api.Models;
using CarService.DbAccess.DAL;
using System;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.SignalR;

namespace CarService.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IHubContext<SignalRHub> _signalRContext;

        public AccountService(
            UserManager<User> userManager, 
            IUnitOfWorkFactory unitOfWorkFactory, 
            IEmailService emailService, 
            IConfiguration configuration,
            IHubContext<SignalRHub> signalRContext)
        {
            this._userManager = userManager;
            this._unitOfWorkFactory = unitOfWorkFactory;
            this._emailService = emailService;
            this._configuration = configuration;
            this._signalRContext = signalRContext;
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

            return await AddUser(user, registerCustomerCredentials.Password);
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

            await this._signalRContext.Clients.All.InvokeAsync("ConfirmEmail", "Email confirmed");

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
            callBackUrl.Host =   _configuration["Email:Host"];
            callBackUrl.Port =   Convert.ToInt32(_configuration["Email:Port"]); 
            callBackUrl.Path =   _configuration["Email:EmailPath"];
              
            var userIdentityParams = new Dictionary<string, string>();
            userIdentityParams.Add("userId", user.Id);
            userIdentityParams.Add("code", code);
            var stringBuilder = new StringBuilder();
            callBackUrl.Query = stringBuilder.AppendJoin("&", userIdentityParams.Select(iup=>$"{iup.Key}={iup.Value}")).ToString();
            
            await _emailService.SendEmailAsync(user.Email, "Confirm your account",
                           $"Please confirm your account by clicking : <a href='{callBackUrl.Uri}'>Confirm Car Service account</a>");

            return result;
        }
    }
}