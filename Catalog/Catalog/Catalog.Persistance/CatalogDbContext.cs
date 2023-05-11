using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Persistance
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
            : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(c => c.Name).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Category>().Property(c => c.ParentCategoryId).IsRequired(false);
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder
                .Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany()
                .HasForeignKey(e => e.ParentCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
                        
            
            modelBuilder.Entity<Product>().Property(c => c.Name).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Product>().ToTable("Poducts");

            modelBuilder.Entity<Category>().HasData(
                        new Category
                        {
                            Id = 1,
                            Name = "Electronics",
                            Image = new Uri("https://example.com/electronics.jpg")
                        },
                        new Category
                        {
                            Id = 2,
                            Name = "Computers",
                            Image = new Uri("https://example.com/computers.jpg"),
                            ParentCategoryId = 1
                        },
                        new Category
                        {
                            Id = 3,
                            Name = "Smartphones",
                            Image = new Uri("https://example.com/smartphones.jpg"),
                            ParentCategoryId = 1
                        },
                        new Category
                        {
                            Id = 4,
                            Name = "Laptops",
                            Image = new Uri("https://example.com/laptops.jpg"),
                            ParentCategoryId = 2
                        });

            modelBuilder.Entity<Product>().HasData(
                        new Product
                        {
                            Id = 1,
                            Name = "iPhone 13",
                            Description = "The latest and greatest iPhone.",
                            Image = "https://example.com/iphone13.jpg",
                            Price = 999.99m,
                            CategoryId = 3,
                            Amount = 100
                        },
                        new Product
                        {
                            Id = 2,
                            Name = "MacBook Pro",
                            Description = "A powerful and versatile laptop.",
                            Image = "https://example.com/macbookpro.jpg",
                            Price = 1999.99m,
                            CategoryId = 4,
                            Amount = 50
                        },
                        new Product
                        {
                            Id = 3,
                            Name = "Asus ROG Strix",
                            Description = "A powerful gaming laptop.",
                            Image = "https://example.com/macbookpro.jpg",
                            Price = 1999.99m,
                            CategoryId = 4,
                            Amount = 30
                        });
        }
    }
}
