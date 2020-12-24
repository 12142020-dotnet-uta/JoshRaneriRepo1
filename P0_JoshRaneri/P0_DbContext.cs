using Microsoft.EntityFrameworkCore;

namespace P0_JoshRaneri
{
    public class P0_DbContext : DbContext
    {
        // public DbSet<Customer> customers { get; set; }
        // public DbSet<Location> locations { get; set; }
        // public DbSet<Order> orders { get; set; }
        // public DbSet<Product> products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=StoreDb;Trusted_Connection=True;");
        }
    }
}