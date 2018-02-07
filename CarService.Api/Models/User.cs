using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CarService.Api.Models
{
    public class User : IdentityUser
    {
        // PasswordHash, PhoneNumber, Email are built-in properties of IdentityUser
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}