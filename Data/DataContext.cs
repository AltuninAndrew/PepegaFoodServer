using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PepegaFoodServer.Models.DbModels;

namespace PepegaFoodServer.Data
{
    public class DataContext : IdentityDbContext<UserDBModel>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<CategoryDBModel> Categories { get; set; } 

        public DbSet<CountTypeDBModel> CountTypes { get; set; }

        public DbSet<ImageProductDBModel> ProductImages { get; set; }

        public DbSet<ProductToShopDBModel> ProductsToShops { get; set; }

        public DbSet<ShopDBModel> Shops { get; set; }

        public DbSet<ProductDBModel> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductToShopDBModel>()
                .HasKey(o => new { o.ProductId, o.ShopId });

        }
    }
}
