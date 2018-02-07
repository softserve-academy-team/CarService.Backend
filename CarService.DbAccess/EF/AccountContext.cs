using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CarService.DbAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CarService.DbAccess.EF
{
    public class AccountDbContext : IdentityDbContext<IdentityUser>
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
        {

        }
    }
}