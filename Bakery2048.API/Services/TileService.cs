using Bakery2048.API.Data;
using Bakery2048.Models;
using Microsoft.EntityFrameworkCore;

namespace Bakery2048.API.Services;

public class TileService
{
    private readonly ApplicationDbContext _context;

    public TileService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Tile>> GetAllTiles()
    {
        return await _context.Tiles.ToListAsync();
    }

    public async Task<Tile?> GetTileById(Guid id)
    {
        return await _context.Tiles.FindAsync(id);
    }

    public async Task<Tile?> GetTileByValue(int value)
    {
        return await _context.Tiles
            .FirstOrDefaultAsync(t => t.TileValue == value);
    }

    public async Task<Tile> CreateTile(string itemName, int tileValue, string color, string icon)
    {
        // check if an active tile with the same value already exists
        var existingTileByValue = await _context.Tiles
            .FirstOrDefaultAsync(t => t.TileValue == tileValue && t.IsActive);
        
        if (existingTileByValue != null)
        {
            throw new InvalidOperationException($"An active tile with value {tileValue} already exists.");
        }

        // check if an active tile with the same name already exists
        var existingTileByName = await _context.Tiles
            .FirstOrDefaultAsync(t => t.ItemName == itemName && t.IsActive);
        
        if (existingTileByName != null)
        {
            throw new InvalidOperationException($"An active tile with name '{itemName}' already exists.");
        }

        var tile = new Tile(itemName, tileValue)
        {
            Color = color,
            Icon = icon
        };
        
        _context.Tiles.Add(tile);
        await _context.SaveChangesAsync();
        return tile;
    }

    public async Task<Tile?> UpdateTile(Guid id, string itemName, int tileValue, string color, string icon)
    {
        var tile = await _context.Tiles.FindAsync(id);
        if (tile == null)
        {
            return null;
        }

        tile.ItemName = itemName;
        tile.TileValue = tileValue;
        tile.Color = color;
        tile.Icon = icon;

        await _context.SaveChangesAsync();
        return tile;
    }

    public async Task<bool> DeleteTile(Guid id)
    {
        var tile = await _context.Tiles.FindAsync(id);
        if (tile == null)
        {
            return false;
        }

        _context.Tiles.Remove(tile);
        await _context.SaveChangesAsync();
        return true;
    }
}
