using Microsoft.AspNetCore.Mvc;
using Bakery2048.API.DTOs;
using Bakery2048.API.Services;

namespace Bakery2048.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PowerUpsController : ControllerBase
{
    private readonly PowerUpService _powerUpService;

    public PowerUpsController(PowerUpService powerUpService)
    {
        _powerUpService = powerUpService;
    }

    // GET: api/powerups - returns all power-ups
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PowerUpResponseDto>>> GetPowerUps()
    {
        var powerUps = await _powerUpService.GetAllPowerUps();
        
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
        var powerUp = await _powerUpService.GetPowerUpById(id);

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
        var powerUp = await _powerUpService.CreatePowerUp(
            createPowerUpDto.PowerUpName,
            createPowerUpDto.Description,
            createPowerUpDto.PowerUpType,
            createPowerUpDto.IsUnlocked,
            createPowerUpDto.IconUrl
        );

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
        var powerUp = await _powerUpService.UpdatePowerUp(
            id,
            updatePowerUpDto.PowerUpName,
            updatePowerUpDto.Description,
            updatePowerUpDto.PowerUpType,
            updatePowerUpDto.IsUnlocked,
            updatePowerUpDto.IconUrl,
            updatePowerUpDto.UsageCount
        );
        
        if (powerUp == null)        
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/powerups/{id} - deletes a power-up
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePowerUp(Guid id)
    {
        var result = await _powerUpService.DeletePowerUp(id);
        
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }   
}