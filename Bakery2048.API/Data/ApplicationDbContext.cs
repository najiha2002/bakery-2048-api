using Microsoft.EntityFrameworkCore;
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

        // Configure all DateTime properties to use UTC
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(new Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<DateTime, DateTime>(
                        v => v.Kind == DateTimeKind.Utc ? v : DateTime.SpecifyKind(v, DateTimeKind.Utc),
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                }
            }
        }

        // Player configuration
        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Username).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Email).IsRequired().HasMaxLength(255);
            
            // Foreign key relationship to User with cascade delete
            entity.HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
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

        // Seed initial Tiles
        modelBuilder.Entity<Tile>().HasData(
            new Tile("Flour", 2) { Id = Guid.Parse("10000000-0000-0000-0000-000000000001"), Color = "#fcefe6", Icon = "🌾" },
            new Tile("Egg", 4) { Id = Guid.Parse("10000000-0000-0000-0000-000000000002"), Color = "#f2e8cb", Icon = "🥚" },
            new Tile("Butter", 8) { Id = Guid.Parse("10000000-0000-0000-0000-000000000003"), Color = "#f5b682", Icon = "🧈" },
            new Tile("Sugar", 16) { Id = Guid.Parse("10000000-0000-0000-0000-000000000004"), Color = "#f29446", Icon = "🍬" },
            new Tile("Donut", 32) { Id = Guid.Parse("10000000-0000-0000-0000-000000000005"), Color = "#f88973", Icon = "🍩" },
            new Tile("Cookie", 64) { Id = Guid.Parse("10000000-0000-0000-0000-000000000006"), Color = "#ed7056", Icon = "🍪" },
            new Tile("Cupcake", 128) { Id = Guid.Parse("10000000-0000-0000-0000-000000000007"), Color = "#ede291", Icon = "🧁" },
            new Tile("Slice Cake", 256) { Id = Guid.Parse("10000000-0000-0000-0000-000000000008"), Color = "#fce130", Icon = "🍰" },
            new Tile("Whole Cake", 512) { Id = Guid.Parse("10000000-0000-0000-0000-000000000009"), Color = "#ffdb4a", Icon = "🎂" }
        );

        // Seed initial PowerUps
        modelBuilder.Entity<PowerUp>().HasData(
            new PowerUp("Score Boost", PowerUpType.ScoreBoost, 100) 
            { 
                Id = Guid.Parse("20000000-0000-0000-0000-000000000001"),
                Description = "Doubles your score for 30 seconds",
                IconUrl = "⚡"
            },
            new PowerUp("Time Extension", PowerUpType.TimeExtension, 150) 
            { 
                Id = Guid.Parse("20000000-0000-0000-0000-000000000002"),
                Description = "Adds 60 seconds to the timer",
                IconUrl = "⏰"
            },
            new PowerUp("Undo Move", PowerUpType.Undo, 50) 
            { 
                Id = Guid.Parse("20000000-0000-0000-0000-000000000003"),
                Description = "Undo your last move",
                IconUrl = "↩️"
            },
            new PowerUp("Tile Swap", PowerUpType.SwapTiles, 200) 
            { 
                Id = Guid.Parse("20000000-0000-0000-0000-000000000004"),
                Description = "Swap any two tiles on the board",
                IconUrl = "🔄"
            }
        );
    }
}
