using System.Collections.Generic;
using System.Threading.Tasks;
using CarService.Api.Models;
using System.Security.Claims;

namespace CarService.Api.Services
{
    public interface IAccountService
    {
        string createJwtToken(ClaimsIdentity identity);
        ClaimsIdentity GetIdentity(string username, string password);
        
    }
}