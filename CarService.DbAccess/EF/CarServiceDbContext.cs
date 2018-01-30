using Microsoft.EntityFrameworkCore;
using CarService.DbAccess.Entities;

namespace CarService.DbAccess.EF
{
    public class CarServiceDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Mechanic> Mechanics { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ReviewProposition> ReviewPropositions { get; set; }
        public DbSet<Auto> Autos { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Invitation> Invitations { get; set; }

        public CarServiceDbContext()
        {
            Database.EnsureCreated();
        }

        public CarServiceDbContext(DbContextOptions<CarServiceDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

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

            // OneToMany(User(Sender), Transaction)
            modelBuilder.Entity<Transaction>()
               .HasOne(x => x.Sender)
               .WithMany(x => x.SendersTransactions)
               .HasForeignKey(x => x.SenderId);

            // OneToMany(User(Receiver), Transaction)
            modelBuilder.Entity<Transaction>()
               .HasOne(x => x.Receiver)
               .WithMany(x => x.ReceiversTransactions)
               .HasForeignKey(x => x.ReceiverId);
        }
    }
}