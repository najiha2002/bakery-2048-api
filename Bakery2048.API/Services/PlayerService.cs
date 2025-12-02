using Bakery2048.API.Data;
using Bakery2048.API.Models;
using Bakery2048.API.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Bakery2048.API.Services;

public class PlayerService
{
    private readonly ApplicationDbContext _context;

    public PlayerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public void ValidateUsername(string username)
    {
        // check if username is empty or whitespace
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("Username is required and cannot be empty or whitespace.");
        }

        // check minimum length
        if (username.Length < 3)
        {
            throw new ArgumentException("Username must be at least 3 characters long.");
        }

        // check maximum length
        if (username.Length > 50)
        {
            throw new ArgumentException("Username cannot exceed 50 characters.");
        }

        // check allowed characters (alphanumeric, underscore, hyphen)
        if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_-]+$"))
        {
            throw new ArgumentException("Username can only contain letters, numbers, underscores, and hyphens.");
        }
    }

    public void ValidateEmail(string email)
    {
        // basic email format validation
        if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            throw new ArgumentException("Invalid email format.");
        }
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
        ValidateUsername(username);
        await ValidatePlayerUniqueness(username, email);

        var player = new Player(username, email);
        _context.Players.Add(player);
        await _context.SaveChangesAsync();
        return player;
    }

    public async Task<Player?> UpdatePlayer(Guid id, UpdatePlayerDto updateDto)
    {
        var player = await _context.Players.FindAsync(id);
        if (player == null)
        {
            return null;
        }

        // Update only the fields that are provided (not null)
        if (updateDto.HighestScore.HasValue)
        {
            if (updateDto.HighestScore.Value < 0)
                throw new ArgumentException("Highest score cannot be negative.");
            player.HighestScore = updateDto.HighestScore.Value;
        }

        if (updateDto.CurrentScore.HasValue)
        {
            if (updateDto.CurrentScore.Value < 0)
                throw new ArgumentException("Current score cannot be negative.");
            player.CurrentScore = updateDto.CurrentScore.Value;
        }

        if (updateDto.BestTileAchieved.HasValue)
        {
            if (updateDto.BestTileAchieved.Value < 0)
                throw new ArgumentException("Best tile achieved cannot be negative.");
            player.BestTileAchieved = updateDto.BestTileAchieved.Value;
        }

        if (updateDto.Level.HasValue)
        {
            if (updateDto.Level.Value < 0)
                throw new ArgumentException("Level cannot be negative.");
            player.Level = updateDto.Level.Value;
        }

        if (updateDto.GamesPlayed.HasValue)
        {
            if (updateDto.GamesPlayed.Value < 0)
                throw new ArgumentException("Games played cannot be negative.");
            player.GamesPlayed = updateDto.GamesPlayed.Value;
        }

        if (updateDto.AverageScore.HasValue)
        {
            if (updateDto.AverageScore.Value < 0)
                throw new ArgumentException("Average score cannot be negative.");
            player.AverageScore = updateDto.AverageScore.Value;
        }

        if (updateDto.TotalPlayTime.HasValue)
            player.TotalPlayTime = updateDto.TotalPlayTime.Value;

        if (updateDto.WinStreak.HasValue)
        {
            if (updateDto.WinStreak.Value < 0)
                throw new ArgumentException("Win streak cannot be negative.");
            player.WinStreak = updateDto.WinStreak.Value;
        }

        if (updateDto.TotalMoves.HasValue)
        {
            if (updateDto.TotalMoves.Value < 0)
                throw new ArgumentException("Total moves cannot be negative.");
            player.TotalMoves = updateDto.TotalMoves.Value;
        }

        if (updateDto.PowerUpsUsed.HasValue)
        {
            if (updateDto.PowerUpsUsed.Value < 0)
                throw new ArgumentException("Power-ups used cannot be negative.");
            player.PowerUpsUsed = updateDto.PowerUpsUsed.Value;
        }

        if (updateDto.FavoriteItem != null)
            player.FavoriteItem = updateDto.FavoriteItem;

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
