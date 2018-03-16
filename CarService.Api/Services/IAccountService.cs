using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarService.Api.Models;

using Microsoft.AspNetCore.Identity;

namespace CarService.Api.Services
{
    public interface IAccountService
    {
        string createJwtToken(ClaimsIdentity identity);
        Task<ClaimsIdentity> GetIdentity(string email, string password);
        Task<IdentityResult> RegisterCustomer(RegisterCustomerCredentials registerCustomerCredentials);
        Task<IdentityResult> RegisterMechanic(RegisterMechanicCredentials registerMechanicCredentials);
        Task<IdentityResult> ConfirmEmail(string userId, string code);
        Task<bool> IsEmailConfirmed(string email);
    }
}