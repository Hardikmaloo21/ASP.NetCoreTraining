using Microsoft.EntityFrameworkCore;
using SalesManagementAPI.Models;

namespace SalesManagementAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // ── Sprint 1 ──────────────────────────────────────────────────────────
        public DbSet<User> Users { get; set; }
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        // ── Sprint 2 ──────────────────────────────────────────────────────────
        public DbSet<Product> Products { get; set; }

        // ── Sprint 3 ──────────────────────────────────────────────────────────
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerNote> CustomerNotes { get; set; }

        // ── Sprint 4 ──────────────────────────────────────────────────────────
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<SalesOrderItem> SalesOrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ── Sprint 1 ──────────────────────────────────────────────────────

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<PasswordResetToken>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ── Sprint 2 ──────────────────────────────────────────────────────

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.SKU)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            // ── Sprint 3 ──────────────────────────────────────────────────────

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<CustomerNote>()
                .HasOne(n => n.Customer)
                .WithMany(c => c.Notes)
                .HasForeignKey(n => n.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // ── Sprint 4 ──────────────────────────────────────────────────────

            // SalesOrder -> Customer
            modelBuilder.Entity<SalesOrder>()
                .HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // SalesOrder -> User (SalesRep)
            modelBuilder.Entity<SalesOrder>()
                .HasOne(o => o.SalesRep)
                .WithMany()
                .HasForeignKey(o => o.SalesRepId)
                .OnDelete(DeleteBehavior.Restrict);

            // SalesOrderItem -> SalesOrder
            modelBuilder.Entity<SalesOrderItem>()
                .HasOne(i => i.SalesOrder)
                .WithMany(o => o.Items)
                .HasForeignKey(i => i.SalesOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // SalesOrderItem -> Product
            modelBuilder.Entity<SalesOrderItem>()
                .HasOne(i => i.Product)
                .WithMany()
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Decimal precision for SalesOrder
            modelBuilder.Entity<SalesOrder>()
                .Property(o => o.SubTotal)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SalesOrder>()
                .Property(o => o.DiscountPercent)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SalesOrder>()
                .Property(o => o.DiscountAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SalesOrder>()
                .Property(o => o.Tax)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SalesOrder>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");

            // Decimal precision for SalesOrderItem
            modelBuilder.Entity<SalesOrderItem>()
                .Property(i => i.UnitPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SalesOrderItem>()
                .Property(i => i.TotalPrice)
                .HasColumnType("decimal(18,2)");

            // Unique index on OrderNumber
            modelBuilder.Entity<SalesOrder>()
                .HasIndex(o => o.OrderNumber)
                .IsUnique();
        }
    }
}
