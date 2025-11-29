using Microsoft.EntityFrameworkCore;
using Bakery2048.Models;
using Bakery2048.API.Models;

namespace Bakery2048.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // Your existing models from console app
    public DbSet<Player> Players { get; set; }
    public DbSet<Tile> Tiles { get; set; }
    public DbSet<PowerUp> PowerUps { get; set; }

    // New model for authentication
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Player configuration
        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Username).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Email).IsRequired().HasMaxLength(255);
        });

        // Tile configuration
        modelBuilder.Entity<Tile>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.ItemName).IsRequired().HasMaxLength(100);
            entity.Property(t => t.Color).IsRequired().HasMaxLength(7);
        });

        // PowerUp configuration
        modelBuilder.Entity<PowerUp>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.PowerUpName).IsRequired().HasMaxLength(100);
            entity.Property(p => p.IconUrl).HasMaxLength(500);
        });

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Username).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(255);
            entity.Property(u => u.PasswordHash).IsRequired();
            entity.Property(u => u.Role).IsRequired().HasMaxLength(50).HasDefaultValue("Player");
            entity.HasIndex(u => u.Email).IsUnique();
            entity.HasIndex(u => u.Username).IsUnique();
        });
    }
}
