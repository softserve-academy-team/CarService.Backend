using Microsoft.AspNetCore.Identity;

namespace CarService.Api.Models
{
    public class CustomerIdentityModel : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
    }
}