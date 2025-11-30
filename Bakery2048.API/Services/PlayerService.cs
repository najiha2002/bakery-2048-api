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
