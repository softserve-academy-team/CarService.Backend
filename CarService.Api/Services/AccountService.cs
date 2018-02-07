using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CarService.DbAccess.Entities;
using CarService.Api.Models;

namespace CarService.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AccountService(UserManager<IdentityUser> userManager)
        {
            this._userManager = userManager;
        }

        public void RegisterCustomer(RegisterCustomerCredentials registerCustomerCredentials)
        {

        }

        public void RegisterMechanic(RegisterCustomerCredentials registerMechanicCredentials)
        {
            
        }

        public async Task<IdentityResult> AddUser(RegisterCustomerCredentials registerCustomerCredentials)
        {
            var user = new IdentityUser
            {
                Email = registerCustomerCredentials.Email,
                UserName = registerCustomerCredentials.Email,
                PhoneNumber = registerCustomerCredentials.PhoneNumber,
            };

            return await _userManager.CreateAsync(user, registerCustomerCredentials.Password);
        }
    }
}