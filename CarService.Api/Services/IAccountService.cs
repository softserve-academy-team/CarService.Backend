using System.Collections.Generic;
using System.Threading.Tasks;
using CarService.Api.Models;
using System.Security.Claims;

using Microsoft.AspNetCore.Identity;

namespace CarService.Api.Services
{
    public interface IAccountService
    {
        string createJwtToken(ClaimsIdentity identity);
        ClaimsIdentity GetIdentity(string username, string password);
        
        Task<IdentityResult> RegisterCustomer(RegisterCustomerCredentials registerCustomerCredentials);
        Task<IdentityResult> RegisterMechanic(RegisterMechanicCredentials registerMechanicCredentials);
    }
}