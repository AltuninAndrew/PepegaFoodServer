using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PepegaFoodServer.Models.DbModels;

namespace PepegaFoodServer.Data
{
    public class DataContext : IdentityDbContext<UserModel>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<CategoryModel> Categories { get; set; } 

        public DbSet<CountTypeModel> CountTypes { get; set; }

        public DbSet<ImageProductModel> ProductImages { get; set; }

        public DbSet<ProductToShopModel> ProductsToShops { get; set; }

        public DbSet<ShopModel> Shops { get; set; }

        public DbSet<ProductModel> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductToShopModel>()
                .HasKey(o => new { o.ProductId, o.ShopId });

        }
    }
}
