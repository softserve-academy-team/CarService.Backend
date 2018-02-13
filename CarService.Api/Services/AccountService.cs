using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CarService.DbAccess.Entities;
using CarService.Api.Models;
using CarService.DbAccess.DAL;
using System;

namespace CarService.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        public AccountService(UserManager<User> userManager, IUnitOfWorkFactory unitOfWorkFactory)
        {
            this._userManager = userManager;
            this._unitOfWorkFactory = unitOfWorkFactory;
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
                WorkExperience = registerMechanicCredentials.WorkExperience
            };

            var result = await _userManager.CreateAsync(user, registerMechanicCredentials.Password);
            if (!result.Succeeded)
                return result;
                
            return result;
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
                City = registerCustomerCredentials.City
            };

            var result = await _userManager.CreateAsync(user, registerCustomerCredentials.Password);
            if (!result.Succeeded)
                return result;

            return result;
        }
    }
}