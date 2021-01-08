using Microsoft.EntityFrameworkCore;
using DomainLib;

namespace DBAccessLib
{
    public class P0_DbContext : DbContext
    {
        /// <summary>
        /// initialize DbSet objects
        /// </summary>
        /// <value>DbSet<></value>
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<InventoryLine> InventoryLines { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public P0_DbContext() {}
        public P0_DbContext(DbContextOptions options) : base(options) {}
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=P0_StoreDb;Trusted_Connection=True;MultipleActiveResultSets=true;");
            }
        }
        /// <summary>
        /// overrides entities for composite keys and unique fields
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasIndex(u => u.UserName)
                .IsUnique();            
            modelBuilder.Entity<InventoryLine>()
                .HasKey(c => new { c.LocationId, c.ProductId });
            modelBuilder.Entity<Cart>()
                .HasKey(c => new { c.CartId, c.CustomerId});
            modelBuilder.Entity<CartItem>()
                .HasKey(c => new { c.CartId, c.ProductId});
            modelBuilder.Entity<OrderItem>()
                .HasKey(c => new { c.OrderId, c.ProductId});
        }
    }
}