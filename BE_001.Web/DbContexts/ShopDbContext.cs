using BE_001.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BE_001.Web.DbContexts;

public class ShopDbContext : IdentityDbContext<User>
{
    public ShopDbContext() : base() { }

    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasMany(e => e.Items)
            .WithMany();

        modelBuilder.Entity<Item>()
            .HasMany<Review>(i => i.Reviews)
            .WithOne()
            .HasForeignKey(r => r.ItemId)
            .IsRequired();
        
        base.OnModelCreating(modelBuilder);
    }
}