using System.ComponentModel.DataAnnotations;
using Bakery2048.API.Models;

namespace Bakery2048.API.DTOs;

// what the client sends when creating a new power-up
public class CreatePowerUpDto
{
    [Required(ErrorMessage = "Power-up name is required")]
    [MaxLength(100)]
    public string PowerUpName { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Power-up type is required")]
    public PowerUpType PowerUpType { get; set; }

    public bool IsUnlocked { get; set; } = false;

    [MaxLength(500)]
    public string? IconUrl { get; set; }

    public int UsageCount { get; set; } = 0;
}

// what the client sends when updating an existing power-up
public class UpdatePowerUpDto
{
    [Required(ErrorMessage = "Power-up name is required")]
    [MaxLength(100)]
    public string PowerUpName { get; set; } = string.Empty;

    [MaxLength(500)]
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
    public Guid Id { get; set; }

    public string PowerUpName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public PowerUpType PowerUpType { get; set; }

    public bool IsUnlocked { get; set; }

    public string? IconUrl { get; set; }

    public int UsageCount { get; set; }

    public DateTime DateCreated { get; set; }
}
