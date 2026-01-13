using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LumelSalesManagementRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace LumelSalesManagementRepository.Context
{
   
    public class SalesManagementDbContext : DbContext
    {
        public SalesManagementDbContext(
     DbContextOptions<SalesManagementDbContext> options)
     : base(options)
        {
        }
        public SalesManagementDbContext()
        {
        }

        // Add DbSets for your entities
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<CustomerDetails> CustomerDetails { get; set; }
        public DbSet<ProductDetails> ProductDetails { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<DataRefreshLog> RefreshLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.HasKey(e => e.OrderId);
                entity.Property(e => e.Region).HasMaxLength(100);
                entity.Property(e => e.PaymentMethod).HasMaxLength(50);
                entity.Property(e => e.DateOfSale).HasColumnType("datetime");
                entity.Property(e => e.QuantitySold).IsRequired();
                entity.Property(e => e.ProductId).IsRequired();
                entity.Property(e => e.CustomerId).IsRequired();
                entity.Property(e => e.CategoryId).IsRequired();
                entity.Property(e =>e.PaymentMethod).HasMaxLength(50);

            });
            modelBuilder.Entity<CustomerDetails>(entity =>
            {
                entity.HasKey(e => e.CustomerId);
                entity.Property(e => e.CustomerName).HasMaxLength(200);
                entity.Property(e => e.CustomerEmail).HasMaxLength(100);
                entity.Property(e => e.CustomerAddress).HasMaxLength(300);
            });
            modelBuilder.Entity<ProductDetails>(entity =>
            {
                entity.HasKey(e => e.ProductId);
                entity.Property(e => e.ProductName).HasMaxLength(200);
                entity.Property(e => e.ProductDescription).HasMaxLength(500);
                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Discount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.ShippingCost).HasColumnType("decimal(18,2)");
            });
            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);
                entity.Property(e => e.CategoryName).HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(300);
            });
            modelBuilder.Entity<DataRefreshLog>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Type).HasMaxLength(100);
                entity.Property(e => e.Status).HasMaxLength(50);
                entity.Property(e => e.Message).HasMaxLength(500);
            });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.; Database =LumelSalesDB;Integrated Security=True;TrustServerCertificate=True;Persist Security Info=False;");
            
        }
    }
}
