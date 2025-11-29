using System.ComponentModel.DataAnnotations;
using Bakery2048.Models;

namespace Bakery2048.API.DTOs;

// what the client sends when creating a new power-up
public class CreatePowerUpDto
{
    [Required(ErrorMessage = "Power-up name is required")]
    [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "Description cannot be longer than 500 characters")]
    public string Description { get; set; } = string.Empty;

    // type of power-up: 0=ScoreBoost, 1=TimeExtension, 2=Undo, 3=SwapTiles
    [Required(ErrorMessage = "Power-up type is required")]
    public PowerUpType PowerUpType { get; set; }

    // whether this power-up is available to use in the game
    public bool IsUnlocked { get; set; } = false;

    // optional: URL or emoji for power-up icon
    [MaxLength(500)]
    public string? IconUrl { get; set; }

    // how many times this power-up has been used by players
    public int UsageCount { get; set; } = 0;
}

// what the client sends when updating an existing power-up
public class UpdatePowerUpDto
{
    [Required(ErrorMessage = "Power-up name is required")]
    [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "Description cannot be longer than 500 characters")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Power-up type is required")]
    public PowerUpType PowerUpType { get; set; }

    public bool IsUnlocked { get; set; }

    [MaxLength(500)]
    public string? IconUrl { get; set; }

    public int UsageCount { get; set; }
}

// what the API returns to the client
public class PowerUpResponseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    // Will be returned as number (0,1,2,3) in JSON
    public PowerUpType PowerUpType { get; set; }

    public bool IsUnlocked { get; set; }

    public string? IconUrl { get; set; }

    public int UsageCount { get; set; }

    public DateTime CreatedAt { get; set; }
}
