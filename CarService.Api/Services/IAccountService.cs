using System.Threading.Tasks;
using CarService.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace CarService.Api.Services
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterCustomer(RegisterCustomerCredentials registerCustomerCredentials);
        Task<IdentityResult> RegisterMechanic(RegisterMechanicCredentials registerMechanicCredentials);
        Task<IdentityResult> ConfirmEmail(string userId, string code);
    }
}