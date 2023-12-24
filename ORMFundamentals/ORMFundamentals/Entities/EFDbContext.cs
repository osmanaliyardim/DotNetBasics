using Microsoft.EntityFrameworkCore;

namespace ORMFundamentals.Entities;

public class EFDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=(localdb)\mssqllocaldb;Database=ORMFundamentalsDb;"
            + "Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .Property(o => o.CreatedDate)
            .HasDefaultValueSql("getDate()");

        modelBuilder.Entity<Order>()
            .HasOne(p => p.Product)
            .WithMany(p => p.Orders)
            .HasForeignKey(p => p.ProductId)
            .IsRequired();
    }
}