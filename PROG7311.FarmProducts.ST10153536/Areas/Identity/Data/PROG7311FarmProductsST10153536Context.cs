using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PROG7311.FarmProducts.ST10153536.Models.Domain;
using PROG7311.FarmProducts.ST10153536.Models;

namespace PROG7311.FarmProducts.ST10153536.Data
{
    public class PROG7311FarmProductsST10153536Context : IdentityDbContext<ApplicationUser>
    {
        public PROG7311FarmProductsST10153536Context(DbContextOptions<PROG7311FarmProductsST10153536Context> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFarmer> ProductFarmers { get; set; }
        // Add DbSet for other entities if needed

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Disable cascade delete behavior globally
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // Configure relationships between ApplicationUser, Product, and ProductFarmer
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(a => a.ProductFarmers)
                .WithOne(pf => pf.ApplicationUser)
                .HasForeignKey(pf => pf.ApplicationUserId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductFarmers)
                .WithOne(pf => pf.Product)
                .HasForeignKey(pf => pf.ProductId);
        }
    }
}
