using Microsoft.AspNetCore.Identity;

namespace CarService.DbAccess.Entities
{
    public class AccountCustomer : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
    }
}