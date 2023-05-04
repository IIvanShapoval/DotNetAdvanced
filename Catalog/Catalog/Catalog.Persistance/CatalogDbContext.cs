using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Persistance
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
            :base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(c => c.Name).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Category>().ToTable("Categories");
            
            modelBuilder.Entity<Product>().Property(c => c.Name).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Product>().ToTable("Poducts");
        }
    }
}
