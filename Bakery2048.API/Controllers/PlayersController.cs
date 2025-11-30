using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bakery2048.API.Data;
using Bakery2048.API.DTOs;
using Bakery2048.Models;

namespace Bakery2048.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    // Constructor injection — gives the controller access to the database
    public PlayersController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/players - returns all players
    // GET: api/players?top=10 - returns top N players by high score
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlayerResponseDto>>> GetPlayers([FromQuery] int? top = null)   
    {
        var query = _context.Players.AsQueryable();
        
        // If 'top' parameter provided, sort by highest score and limit results
        if (top.HasValue)
        {
            query = query
                .OrderByDescending(p => p.HighestScore)
                .Take(top.Value);
        }
        
        var players = await query.ToListAsync();
        
        // Convert to DTOs
        var playerDtos = players.Select(p => new PlayerResponseDto
        {
            Id = p.Id,
            Username = p.Username,
            Email = p.Email,
            HighestScore = p.HighestScore,
            CurrentScore = p.CurrentScore,
            GamesPlayed = p.GamesPlayed,
            DateCreated = p.DateCreated
        }).ToList();
        
        return Ok(playerDtos);
    }   

    // GET: api/players/{id} - returns a specific player by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<PlayerResponseDto>> GetPlayer(Guid id)
    {
        var player = await _context.Players.FindAsync(id);

        if (player == null)
        {
            return NotFound();
        }

        var playerDto = new PlayerResponseDto
        {
            Id = player.Id,
            Username = player.Username,
            Email = player.Email,
            HighestScore = player.HighestScore,
            CurrentScore = player.CurrentScore,
            GamesPlayed = player.GamesPlayed,
            DateCreated = player.DateCreated
        };

        return Ok(playerDto);
    }

    // POST: api/players - creates a new player
    [HttpPost]
    public async Task<ActionResult<PlayerResponseDto>> CreatePlayer(CreatePlayerDto createPlayerDto)
    {
        var player = new Player(createPlayerDto.Username, createPlayerDto.Email);
        _context.Players.Add(player);
        await _context.SaveChangesAsync();

        var playerDto = new PlayerResponseDto
        {
            Id = player.Id,
            Username = player.Username,
            Email = player.Email,
            HighestScore = player.HighestScore,
            CurrentScore = player.CurrentScore,
            GamesPlayed = player.GamesPlayed,
            DateCreated = player.DateCreated
        };

        return CreatedAtAction(nameof(GetPlayer), new { id = player.Id }, playerDto);
    }

    // PUT: api/players/{id} - updates an existing player
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePlayer(Guid id, UpdatePlayerDto updatePlayerDto)
    {
        var player = await _context.Players.FindAsync(id);
        if (player == null)
        {
            return NotFound();  
        }
        player.Username = updatePlayerDto.Username;
        player.Email = updatePlayerDto.Email;
        player.HighestScore = updatePlayerDto.HighestScore;
        player.CurrentScore = updatePlayerDto.CurrentScore;
        player.GamesPlayed = updatePlayerDto.GamesPlayed;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/players/{id} - deletes a player
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlayer(Guid id)
    {
        var player = await _context.Players.FindAsync(id);
        if (player == null)
        {
            return NotFound();
        }

        _context.Players.Remove(player);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}