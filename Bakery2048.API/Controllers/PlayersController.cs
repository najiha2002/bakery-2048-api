using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Bakery2048.API.DTOs;
using Bakery2048.API.Services;

namespace Bakery2048.API.Controllers;

/// <summary>
/// Player management endpoints
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly PlayerService _playerService;

    public PlayersController(PlayerService playerService)
    {
        _playerService = playerService;
    }

    /// <summary>
    /// Gets all players or top N players by highest score
    /// </summary>
    /// <param name="top">Optional: Number of top players to return</param>
    /// <returns>List of players</returns>
    /// <response code="200">Returns the list of players</response>
    /// <response code="401">If user is not authenticated</response>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PlayerResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

    /// <summary>
    /// Gets a specific player by ID
    /// </summary>
    /// <param name="id">The player's unique identifier</param>
    /// <returns>Player details</returns>
    /// <response code="200">Returns the player</response>
    /// <response code="401">If user is not authenticated</response>
    /// <response code="404">If player is not found</response>
    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PlayerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    /// <summary>
    /// Updates an existing player's game statistics
    /// </summary>
    /// <param name="id">The player's unique identifier</param>
    /// <param name="updatePlayerDto">Updated player statistics</param>
    /// <returns>Updated player information</returns>
    /// <response code="200">Player successfully updated</response>
    /// <response code="400">If validation fails</response>
    /// <response code="401">If user is not authenticated</response>
    /// <response code="404">If player is not found</response>
    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePlayer(Guid id, UpdatePlayerDto updatePlayerDto)
    {
        try
        {
            var player = await _playerService.UpdatePlayer(id, updatePlayerDto);
            
            if (player == null)
            {
                return NotFound();  
            }

            return Ok(player);
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

    /// <summary>
    /// Deletes a player (Admin only)
    /// </summary>
    /// <param name="id">The player's unique identifier</param>
    /// <returns>No content</returns>
    /// <response code="204">Player successfully deleted</response>
    /// <response code="401">If user is not authenticated</response>
    /// <response code="403">If user is not an admin</response>
    /// <response code="404">If player is not found</response>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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