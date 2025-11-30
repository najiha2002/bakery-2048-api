using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bakery2048.API.Data;
using Bakery2048.API.DTOs;
using Bakery2048.Models;

namespace Bakery2048.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PowerUpsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    // Constructor injection — gives the controller access to the database
    public PowerUpsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/powerups - returns all power-ups
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PowerUpResponseDto>>> GetPowerUps()
    {
        var powerUps = await _context.PowerUps.ToListAsync();
        
        var powerUpDtos = powerUps.Select(p => new PowerUpResponseDto
        {
            Id = p.Id,
            PowerUpName = p.PowerUpName,
            Description = p.Description,
            PowerUpType = p.PowerUpType,
            IsUnlocked = p.IsUnlocked,
            IconUrl = p.IconUrl,
            UsageCount = p.UsageCount,
            DateCreated = p.DateCreated
        }).ToList();
        
        return Ok(powerUpDtos);
    }

    // GET: api/powerups/{id} - returns a specific power-up by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<PowerUpResponseDto>> GetPowerUp(Guid id)
    {
        var powerUp = await _context.PowerUps.FindAsync(id);

        if (powerUp == null)
        {
            return NotFound();
        }

        var powerUpDto = new PowerUpResponseDto
        {
            Id = powerUp.Id,
            PowerUpName = powerUp.PowerUpName,
            Description = powerUp.Description,
            PowerUpType = powerUp.PowerUpType,
            IsUnlocked = powerUp.IsUnlocked,
            IconUrl = powerUp.IconUrl,
            UsageCount = powerUp.UsageCount,
            DateCreated = powerUp.DateCreated
        };

        return Ok(powerUpDto);
    }

    // POST: api/powerups - creates a new power-up
    [HttpPost]
    public async Task<ActionResult<PowerUpResponseDto>> CreatePowerUp(CreatePowerUpDto createPowerUpDto)
    {
        var powerUp = new PowerUp
        {
            PowerUpName = createPowerUpDto.PowerUpName,
            Description = createPowerUpDto.Description,
            PowerUpType = createPowerUpDto.PowerUpType,
            IsUnlocked = createPowerUpDto.IsUnlocked,
            IconUrl = createPowerUpDto.IconUrl,
            UsageCount = createPowerUpDto.UsageCount
        };
        _context.PowerUps.Add(powerUp);
        await _context.SaveChangesAsync();

        var powerUpDto = new PowerUpResponseDto
        {
            Id = powerUp.Id,
            PowerUpName = powerUp.PowerUpName,
            Description = powerUp.Description,
            PowerUpType = powerUp.PowerUpType,
            IsUnlocked = powerUp.IsUnlocked,
            IconUrl = powerUp.IconUrl,
            UsageCount = powerUp.UsageCount,
            DateCreated = powerUp.DateCreated
        };

        return CreatedAtAction(nameof(GetPowerUp), new { id = powerUp.Id }, powerUpDto);
    }

    // PUT: api/powerups/{id} - updates an existing power-up
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePowerUp(Guid id, UpdatePowerUpDto updatePowerUpDto)
    {
        var powerUp = await _context.PowerUps.FindAsync(id);
        if (powerUp == null)        
        {
            return NotFound();
        }
        powerUp.PowerUpName = updatePowerUpDto.PowerUpName;
        powerUp.Description = updatePowerUpDto.Description;
        powerUp.PowerUpType = updatePowerUpDto.PowerUpType;
        powerUp.IsUnlocked = updatePowerUpDto.IsUnlocked;
        powerUp.IconUrl = updatePowerUpDto.IconUrl;
        powerUp.UsageCount = updatePowerUpDto.UsageCount;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/powerups/{id} - deletes a power-up
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePowerUp(Guid id)
    {
        var powerUp = await _context.PowerUps.FindAsync(id);
        if (powerUp == null)
        {
            return NotFound();
        }
        _context.PowerUps.Remove(powerUp);
        await _context.SaveChangesAsync();

        return NoContent();
    }   
}