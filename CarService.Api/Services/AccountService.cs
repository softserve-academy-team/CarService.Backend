using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CarService.DbAccess.Entities;
using CarService.Api.Models;
using CarService.DbAccess.DAL;

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

        public void RegisterCustomer(RegisterCustomerCredentials registerCustomerCredentials)
        {
            IUnitOfWork unitOfWork = _unitOfWorkFactory.Create();
            IRepository<User> users = unitOfWork.Repository<User>();
            System.Console.WriteLine("ADasdasda");
            users.Add(new User());
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