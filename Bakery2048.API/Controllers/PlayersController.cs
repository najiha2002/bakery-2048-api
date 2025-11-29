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
    public async Task<ActionResult<IEnumerable<Player>>> GetPlayers([FromQuery] int? top = null)   
    {
        var query = _context.Players.AsQueryable();
        
        // If 'top' parameter provided, sort by high score and limit results
        if (top.HasValue)
        {
            query = query
                .OrderByDescending(p => p.HighScore)
                .ThenByDescending(p => p.TotalScore)
                .Take(top.Value);
        }
        
        return await query.ToListAsync();
    }   

    // GET: api/players/{id} - returns a specific player by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Player>> GetPlayer(int id)
    {
        var player = await _context.Players.FindAsync(id);

        if (player == null)
        {
            return NotFound();
        }

        return player;
    }

    // POST: api/players - creates a new player
    [HttpPost]
    public async Task<ActionResult<Player>> CreatePlayer(CreatePlayerDto createPlayerDto)
    {
        var player = new Player
        {
            Name = createPlayerDto.Name,
            Email = createPlayerDto.Email,
            TotalScore = createPlayerDto.TotalScore,
            SessionsPlayed = createPlayerDto.SessionsPlayed,
            HighScore = createPlayerDto.HighScore
        };
        _context.Players.Add(player);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPlayer), new { id = player.Id }, player);
    }

    // PUT: api/players/{id} - updates an existing player
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePlayer(int id, UpdatePlayerDto updatePlayerDto)
    {
        var player = await _context.Players.FindAsync(id);
        if (player == null)
        {
            return NotFound();  
        }
        player.Name = updatePlayerDto.Name;
        player.Email = updatePlayerDto.Email;
        player.TotalScore = updatePlayerDto.TotalScore;
        player.SessionsPlayed = updatePlayerDto.SessionsPlayed;
        player.HighScore = updatePlayerDto.HighScore;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/players/{id} - deletes a player
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlayer(int id)
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