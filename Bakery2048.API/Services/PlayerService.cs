using Bakery2048.API.Data;
using Bakery2048.Models;
using Microsoft.EntityFrameworkCore;

namespace Bakery2048.API.Services;

public class PlayerService
{
    private readonly ApplicationDbContext _context;

    public PlayerService(ApplicationDbContext context)
    {
        _context = context;
    }

    private async Task ValidatePlayerUniqueness(string username, string email, Guid? excludePlayerId = null)
    {
        // check if an active player with the same username already exists
        var usernameQuery = _context.Players
            .Where(p => p.Username == username && p.IsActive);
        
        if (excludePlayerId.HasValue)
        {
            usernameQuery = usernameQuery.Where(p => p.Id != excludePlayerId.Value);
        }
        
        var existingPlayerByUsername = await usernameQuery.FirstOrDefaultAsync();
        
        if (existingPlayerByUsername != null)
        {
            throw new InvalidOperationException($"An active player with username '{username}' already exists.");
        }

        // check if an active player with the same email already exists
        var emailQuery = _context.Players
            .Where(p => p.Email == email && p.IsActive);
        
        if (excludePlayerId.HasValue)
        {
            emailQuery = emailQuery.Where(p => p.Id != excludePlayerId.Value);
        }
        
        var existingPlayerByEmail = await emailQuery.FirstOrDefaultAsync();
        
        if (existingPlayerByEmail != null)
        {
            throw new InvalidOperationException($"An active player with email '{email}' already exists.");
        }
    }

    public async Task<List<Player>> GetAllPlayers()
    {
        return await _context.Players.ToListAsync();
    }

    public async Task<List<Player>> GetTopPlayers(int count)
    {
        return await _context.Players
            .OrderByDescending(p => p.HighestScore)
            .Take(count)
            .ToListAsync();
    }

    public async Task<Player?> GetPlayerById(Guid id)
    {
        return await _context.Players.FindAsync(id);
    }

    public async Task<Player> CreatePlayer(string username, string email)
    {
        await ValidatePlayerUniqueness(username, email);

        var player = new Player(username, email);
        _context.Players.Add(player);
        await _context.SaveChangesAsync();
        return player;
    }

    public async Task<Player?> UpdatePlayer(Guid id, string username, string email, int highestScore, int currentScore, int gamesPlayed)
    {
        var player = await _context.Players.FindAsync(id);
        if (player == null)
        {
            return null;
        }

        await ValidatePlayerUniqueness(username, email, id);

        player.Username = username;
        player.Email = email;
        player.HighestScore = highestScore;
        player.CurrentScore = currentScore;
        player.GamesPlayed = gamesPlayed;

        await _context.SaveChangesAsync();
        return player;
    }

    public async Task<bool> DeletePlayer(Guid id)
    {
        var player = await _context.Players.FindAsync(id);
        if (player == null)
        {
            return false;
        }

        _context.Players.Remove(player);
        await _context.SaveChangesAsync();
        return true;
    }
}
