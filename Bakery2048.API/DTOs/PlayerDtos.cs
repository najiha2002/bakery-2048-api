using System.ComponentModel.DataAnnotations;

namespace Bakery2048.API.DTOs;

// what the client sends when creating a new player
public class CreatePlayerDto
{
    [Required(ErrorMessage = "Username is required")]
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;
}

// what the client sends when updating an existing player
public class UpdatePlayerDto
{
    public int? HighestScore { get; set; }
    public int? CurrentScore { get; set; }
    public int? BestTileAchieved { get; set; }
    public int? Level { get; set; }
    public int? GamesPlayed { get; set; }
    public double? AverageScore { get; set; }
    public TimeSpan? TotalPlayTime { get; set; }
    public int? WinStreak { get; set; }
    public int? TotalMoves { get; set; }
    public int? PowerUpsUsed { get; set; }
    public string? FavoriteItem { get; set; }
}

// what the API returns to the client
public class PlayerResponseDto
{
    public Guid Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public int HighestScore { get; set; }
    
    public int CurrentScore { get; set; }

    public int GamesPlayed { get; set; }

    public int WinStreak { get; set; }

    public DateTime DateCreated { get; set; }
}
