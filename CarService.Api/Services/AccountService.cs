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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        public AccountService(UserManager<IdentityUser> userManager, IUnitOfWorkFactory unitOfWorkFactory)
        {
            this._userManager = userManager;
            this._unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<IdentityResult> RegisterMechanic(RegisterMechanicCredentials registerMechanicCredentials)
        {
            var user = new IdentityUser
            {
                Email = registerMechanicCredentials.Email,
                UserName = registerMechanicCredentials.Email,
                PhoneNumber = registerMechanicCredentials.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, registerMechanicCredentials.Password);
            if (!result.Succeeded)
                return result;

            AddMechanic(registerMechanicCredentials, user);
            return result;
        }

        public async Task<IdentityResult> RegisterCustomer(RegisterCustomerCredentials registerCustomerCredentials)
        {
            var user = new IdentityUser
            {
                Email = registerCustomerCredentials.Email,
                UserName = registerCustomerCredentials.Email,
                PhoneNumber = registerCustomerCredentials.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, registerCustomerCredentials.Password);
            if (!result.Succeeded)
                return result;

            AddCustomer(registerCustomerCredentials, user);
            return result;
        }


        private void AddCustomer(RegisterCustomerCredentials registerCustomerCredentials, IdentityUser user)
        {
            var customer = new Customer
            {
                IdIdentity = user.Id,
                Email = user.Email,
                Password = user.PasswordHash,
                Status = UserStatus.Inactive,
                FirstName = registerCustomerCredentials.FirstName,
                LastName = registerCustomerCredentials.LastName,
                PhoneNumber = registerCustomerCredentials.PhoneNumber,
                RegisterDate = DateTime.Now.ToUniversalTime(),
                City = registerCustomerCredentials.City
            };

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                IRepository<Customer> users = unitOfWork.Repository<Customer>();
                users.Add(customer);
                unitOfWork.Save();
            }
        }

        private void AddMechanic(RegisterMechanicCredentials registerMechanicCredentials, IdentityUser user)
        {
            var mechanic = new Mechanic
            {
                IdIdentity = user.Id,
                Email = user.Email,
                Password = user.PasswordHash,
                Status = UserStatus.Inactive,
                FirstName = registerMechanicCredentials.FirstName,
                LastName = registerMechanicCredentials.LastName,
                PhoneNumber = registerMechanicCredentials.PhoneNumber,
                RegisterDate = DateTime.Now.ToUniversalTime(),
                City = registerMechanicCredentials.City,
                WorkExperience = registerMechanicCredentials.WorkExperience
            };

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                IRepository<Mechanic> users = unitOfWork.Repository<Mechanic>();
                users.Add(mechanic);
                unitOfWork.Save();
            }
        }
    }
}