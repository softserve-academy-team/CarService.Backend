using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CarService.DbAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarService.DbAccess.EF
{
    public class AccountDbContext : IdentityDbContext<AccountCustomer>
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
        {

        }
    }
}