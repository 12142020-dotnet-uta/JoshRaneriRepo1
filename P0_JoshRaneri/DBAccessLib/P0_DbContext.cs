using Microsoft.EntityFrameworkCore;
using DomainLib;

namespace DBAccessLib
{
    public class P0_DbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<InventoryLine> InventoryLines { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=P0_StoreDb;Trusted_Connection=True;MultipleActiveResultSets=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InventoryLine>()
                .HasKey(c => new { c.LocationId, c.ProductId });
            modelBuilder.Entity<Customer>()
                .HasIndex(u => u.UserName)
                .IsUnique();
        }
    }
}