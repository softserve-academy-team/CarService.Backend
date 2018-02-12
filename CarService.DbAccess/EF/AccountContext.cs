using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CarService.DbAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CarService.DbAccess.EF
{
    public class AccountDbContext : IdentityDbContext<User>
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
        {

        }

        //public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Mechanic> Mechanics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // OneToOne(Order, Review)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Review)
                .WithOne(r => r.Order)
                .HasForeignKey<Review>(r => r.OrderId);

            // ManyToMany(Customer, Auto)
            modelBuilder.Entity<CustomerAuto>()
                .HasKey(ca => new { ca.CustomerId, ca.AutoId });

            // // OneToMany(User(Sender), Transaction)
            // modelBuilder.Entity<Transaction>()
            //    .HasOne(x => x.Sender)
            //    .WithMany(x => x.SendersTransactions)
            //    .HasForeignKey(x => x.SenderId);

            // // OneToMany(User(Receiver), Transaction)
            // modelBuilder.Entity<Transaction>()
            //    .HasOne(x => x.Receiver)
            //    .WithMany(x => x.ReceiversTransactions)
            //    .HasForeignKey(x => x.ReceiverId);
        }
    }
}