using Microsoft.AspNetCore.Mvc;
using Bakery2048.API.DTOs;
using Bakery2048.API.Services;

namespace Bakery2048.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly PlayerService _playerService;

    public PlayersController(PlayerService playerService)
    {
        _playerService = playerService;
    }

    // GET: api/players - returns all players
    // GET: api/players?top=10 - returns top N players by high score
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlayerResponseDto>>> GetPlayers([FromQuery] int? top = null)   
    {
        var players = top.HasValue 
            ? await _playerService.GetTopPlayers(top.Value)
            : await _playerService.GetAllPlayers();
        
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
        var player = await _playerService.GetPlayerById(id);

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
        try
        {
            var player = await _playerService.CreatePlayer(createPlayerDto.Username, createPlayerDto.Email);

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
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // PUT: api/players/{id} - updates an existing player
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePlayer(Guid id, UpdatePlayerDto updatePlayerDto)
    {
        try
        {
            var player = await _playerService.UpdatePlayer(
                id, 
                updatePlayerDto.Username, 
                updatePlayerDto.Email, 
                updatePlayerDto.HighestScore, 
                updatePlayerDto.CurrentScore, 
                updatePlayerDto.GamesPlayed
            );
            
            if (player == null)
            {
                return NotFound();  
            }

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // DELETE: api/players/{id} - deletes a player
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlayer(Guid id)
    {
        var result = await _playerService.DeletePlayer(id);
        
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}