using Bakery2048.API.Data;
using Bakery2048.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Bakery2048.API.Services;

public class PowerUpService
{
    private readonly ApplicationDbContext _context;

    public PowerUpService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<PowerUp>> GetAllPowerUps()
    {
        return await _context.PowerUps.ToListAsync();
    }

    public async Task<List<PowerUp>> GetUnlockedPowerUps()
    {
        return await _context.PowerUps
            .Where(p => p.IsUnlocked)
            .ToListAsync();
    }

    public async Task<PowerUp?> GetPowerUpById(Guid id)
    {
        return await _context.PowerUps.FindAsync(id);
    }

    public async Task<PowerUp> CreatePowerUp(string name, string description, PowerUpType type, bool isUnlocked, string iconUrl)
    {
        var powerUp = new PowerUp(name, type, 0)
        {
            Description = description,
            IsUnlocked = isUnlocked,
            IconUrl = iconUrl
        };
        
        _context.PowerUps.Add(powerUp);
        await _context.SaveChangesAsync();
        return powerUp;
    }

    public async Task<PowerUp?> UpdatePowerUp(Guid id, string name, string description, PowerUpType type, bool isUnlocked, string iconUrl, int usageCount)
    {
        var powerUp = await _context.PowerUps.FindAsync(id);
        if (powerUp == null)
        {
            return null;
        }

        powerUp.PowerUpName = name;
        powerUp.Description = description;
        powerUp.PowerUpType = type;
        powerUp.IsUnlocked = isUnlocked;
        powerUp.IconUrl = iconUrl;
        powerUp.UsageCount = usageCount;

        await _context.SaveChangesAsync();
        return powerUp;
    }

    public async Task<bool> DeletePowerUp(Guid id)
    {
        var powerUp = await _context.PowerUps.FindAsync(id);
        if (powerUp == null)
        {
            return false;
        }

        _context.PowerUps.Remove(powerUp);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<PowerUp?> IncrementUsageCount(Guid id)
    {
        var powerUp = await _context.PowerUps.FindAsync(id);
        if (powerUp == null)
        {
            return null;
        }

        powerUp.UsageCount++;
        await _context.SaveChangesAsync();
        return powerUp;
    }
}
