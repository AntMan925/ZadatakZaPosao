using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Service.Entities;

namespace Project.Service.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductCategory>().HasData(
            new ProductCategory
            {
                Id = 1,
                Name = "Elektronika",
                Description = "Elektronicki uredjaji"
            },
            new ProductCategory
            {
                Id = 2,
                Name = "Odjeća",
                Description = "Muska i zenska roba"
            },
            new ProductCategory
            {
                Id = 3,
                Name = "Prehrana",
                Description = "Prehrambeni proizvodi"
            },
            new ProductCategory
            {
                Id = 4,
                Name = "Pet's",
                Description = "Stvari za kucne ljubimce"
            }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Laptop",
                    Price = 1200,
                    CategoryId = 1,
                    Stock = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 6)
                },
                new Product
                {
                    Id = 2,
                    Name = "Stolno racunalo",
                    Price = 900,
                    CategoryId = 1,
                    Stock = 30,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 6)
                },
                new Product
                {
                    Id = 3,
                    Name = "Monitor",
                    Price = 350,
                    CategoryId = 1,
                    Stock = 50,
                    IsActive = false,
                    CreatedAt = new DateTime(2026, 3, 6)
                },
                new Product
                {
                    Id = 4,
                    Name = "Majica",
                    Price = 25,
                    CategoryId = 2,
                    Stock = 100,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 6)
                },
                new Product
                {
                    Id = 5,
                    Name = "Hlace",
                    Price = 30,
                    CategoryId = 2,
                    Stock = 5012,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 6)
                },
                new Product
                {
                    Id = 6,
                    Name = "Zelena salata",
                    Price = 0.99M,
                    CategoryId = 3,
                    Stock = 10000,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 6)
                },
                new Product
                {
                    Id = 7,
                    Name = "Kruh",
                    Price = 1.30M,
                    CategoryId = 3,
                    Stock = 535,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 6)
                },
                new Product
                {
                    Id = 8,
                    Name = "Loptica",
                    Price = 2,
                    CategoryId = 4,
                    Stock = 5,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 3, 6)
                }
                );
        }
    }
}
