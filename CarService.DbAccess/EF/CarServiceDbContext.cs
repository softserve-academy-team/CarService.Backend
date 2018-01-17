using CarService.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace CarService.DbAccess.EF
{
    public class CarServiceDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Mechanic> Mechanics { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<ReviewProposition> ReviewPropositions { get; set; }

        public virtual DbSet<Auto> Autos { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }

        public virtual DbSet<Message> Messages { get; set; }

        public virtual DbSet<Dialog> Dialogs { get; set; }

        public virtual DbSet<Transaction> Transactions { get; set; }

        public virtual DbSet<Invitation> Invitations { get; set; }

        public CarServiceDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=CarServiceCore;Integrated Security=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            //OneToOne(Order, Review)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Review)
                .WithOne(r => r.Order)
                .HasForeignKey<Review>(r => r.OrderId);

            //ManyToMany(Customer, Auto)
            modelBuilder.Entity<CustomerAuto>()
                .HasKey(ca => new { ca.CustomerId, ca.AutoId });
        }
    }
}