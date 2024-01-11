using Ecommerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data
{
    public class EcommerceDbContext:IdentityDbContext
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options):base(options)
        {

            
        }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShippingCompany> ShippingCompanies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart_item> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart_item>()
                .HasNoKey();
            

            base.OnModelCreating(modelBuilder);
        }
    }
}
