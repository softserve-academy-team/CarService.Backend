using System.Threading.Tasks;
using CarService.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace CarService.Api.Services
{
    public interface IAccountService
    {
        void RegisterCustomer(RegisterCustomerCredentials registerCustomerCredentials);
        void RegisterMechanic(RegisterCustomerCredentials registerMechanicCredentials);
        Task<IdentityResult> AddUser(RegisterCustomerCredentials registerCustomerCredentials);
    }
}