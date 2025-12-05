using System.ComponentModel.DataAnnotations;

namespace Bakery2048.API.DTOs;

// Custom validation attribute to ensure tile value is a power of 2
public class PowerOfTwoAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is int tileValue)
        {
            // Check if the value is a power of 2
            // A number is a power of 2 if: value > 0 AND (value & (value - 1)) == 0
            if (tileValue > 0 && (tileValue & (tileValue - 1)) == 0)
            {
                return ValidationResult.Success;
            }
            
            return new ValidationResult("Tile value must be a power of 2 (e.g., 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, etc.)");
        }
        
        return new ValidationResult("Tile value is required");
    }
}

// what the client sends when creating a new tile (bakery item)
public class CreateTileDto
{
    [Required(ErrorMessage = "Item name is required")]
    [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
    public string ItemName { get; set; } = string.Empty;

    // point value of this tile 
    [Required(ErrorMessage = "Tile value is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than 0")]
    [PowerOfTwo]
    public int TileValue { get; set; }

    // color of the tile in hex format
    [Required(ErrorMessage = "Color is required")]
    [RegularExpression(@"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", 
        ErrorMessage = "Must be valid hex color (e.g., #FF5733)")]
    public string Color { get; set; } = string.Empty;

    // optional: URL or emoji for tile icon
    [MaxLength(500)]
    public string? Icon { get; set; }
}

// what the client sends when updating an existing tile
public class UpdateTileDto
{
    [Required(ErrorMessage = "Item name is required")]
    [MaxLength(100)]
    public string ItemName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Tile value is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than 0")]
    [PowerOfTwo]
    public int TileValue { get; set; }

    [Required(ErrorMessage = "Color is required")]
    [RegularExpression(@"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", 
        ErrorMessage = "Must be valid hex color (e.g., #FF5733)")]
    public string Color { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Icon { get; set; }
}

// what the API returns to the client
public class TileResponseDto
{
    public Guid Id { get; set; }

    public string ItemName { get; set; } = string.Empty;

    public int TileValue { get; set; }

    public string Color { get; set; } = string.Empty;

    public string? Icon { get; set; }

    public DateTime DateCreated { get; set; }
}
