using System.ComponentModel.DataAnnotations;

namespace Bakery2048.API.DTOs;

// what the client sends when creating a new player
public class CreatePlayerDto
{
    [Required(ErrorMessage = "Player name is required")]
    [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    // total score accumulated across all game sessions
    public int TotalScore { get; set; } = 0;

    // how many game sessions this player has played
    public int SessionsPlayed { get; set; } = 0;

    // highest score achieved in a single session
    public int HighScore { get; set; } = 0;
}

// what the client sends when updating an existing player
public class UpdatePlayerDto
{
    [Required(ErrorMessage = "Player name is required")]
    [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    public int TotalScore { get; set; }

    public int SessionsPlayed { get; set; }

    public int HighScore { get; set; }
}

// what the API returns to the client after Create/Get operations
// includes Id and timestamp so client knows when player was created
public class PlayerResponseDto
{
    // database-generated unique identifier
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public int TotalScore { get; set; }

    public int SessionsPlayed { get; set; }

    public int HighScore { get; set; }

    // when this player record was first created
    public DateTime CreatedAt { get; set; }
}
