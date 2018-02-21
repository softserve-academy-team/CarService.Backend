using Microsoft.EntityFrameworkCore;
using CarService.DbAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CarService.DbAccess.EF
{
    public class CarServiceDbContext : IdentityDbContext<User>
    {
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

        public CarServiceDbContext(DbContextOptions options) : base(options)
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
               .HasOne(t => t.Sender)
               .WithMany(t => t.SendersTransactions)
               .HasForeignKey(t => t.SenderId);

            // OneToMany(User(Receiver), Transaction)
            modelBuilder.Entity<Transaction>()
               .HasOne(t => t.Receiver)
               .WithMany(t => t.ReceiversTransactions)
               .HasForeignKey(t => t.ReceiverId);

            // Unique constraint for User.EntityId
            modelBuilder.Entity<User>()
               .HasAlternateKey(u => u.EntityId)
               .HasName("AlternateKey_Entityid");    

            // AutoIncrement for User.EntityId
            modelBuilder.Entity<User>()
                .Property(u => u.EntityId)
                .ValueGeneratedOnAdd();             
        }
    }
}