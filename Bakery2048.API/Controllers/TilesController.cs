using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Bakery2048.API.DTOs;
using Bakery2048.API.Services;

namespace Bakery2048.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TilesController : ControllerBase
{
    private readonly TileService _tileService;

    // constructor injection of TileService
    public TilesController(TileService tileService)
    {
        _tileService = tileService;
    }

    // GET: api/tiles - returns all tiles
    /// <summary>
    /// Gets all tiles in the game
    /// </summary>
    /// <returns>List of all tiles with their properties</returns>
    /// <response code="200">Returns the list of tiles</response>
    /// <response code="401">If user is not authenticated</response>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TileResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<TileResponseDto>>> GetTiles()
    {
        var tiles = await _tileService.GetAllTiles();
        
        var tileDtos = tiles.Select(t => new TileResponseDto
        {
            Id = t.Id,
            ItemName = t.ItemName,
            TileValue = t.TileValue,
            Color = t.Color,
            Icon = t.Icon,
            DateCreated = t.DateCreated
        }).ToList();
        
        // return a 200 OK response with the tile data
        return Ok(tileDtos);
    }

    // GET: api/tiles/{id} - returns a specific tile by ID
    /// <summary>
    /// Gets a specific tile by ID
    /// </summary>
    /// <param name="id">The tile's unique identifier</param>
    /// <returns>Tile details</returns>
    /// <response code="200">Returns the tile</response>
    /// <response code="401">If user is not authenticated</response>
    /// <response code="404">If tile is not found</response>
    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TileResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TileResponseDto>> GetTile(Guid id)
    {
        var tile = await _tileService.GetTileById(id);

        if (tile == null)
        {
            return NotFound();
        }

        var tileDto = new TileResponseDto
        {
            Id = tile.Id,
            ItemName = tile.ItemName,
            TileValue = tile.TileValue,
            Color = tile.Color,
            Icon = tile.Icon,
            DateCreated = tile.DateCreated
        };
        
        // return a 200 OK response with the tile data
        return Ok(tileDto);
    }

    // POST: api/tiles - creates a new tile
    /// <summary>
    /// Creates a new tile (Admin only)
    /// </summary>
    /// <param name="createTileDto">The tile data to create</param>
    /// <returns>The newly created tile</returns>
    /// <response code="201">Returns the newly created tile</response>
    /// <response code="400">If the tile data is invalid</response>
    /// <response code="401">If user is not authenticated</response>
    /// <response code="403">If user is not an Admin</response>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(typeof(TileResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<TileResponseDto>> CreateTile(CreateTileDto createTileDto) // receives createTileDto of type CreateTileDto object
    {
        try
        {
            // save the new tile into database using the service
            var tile = await _tileService.CreateTile(
                createTileDto.ItemName, 
                createTileDto.TileValue, 
                createTileDto.Color, 
                createTileDto.Icon
            );

            // create a TileResponseDto to return as response
            var tileDto = new TileResponseDto
            {
                Id = tile.Id,
                ItemName = tile.ItemName,
                TileValue = tile.TileValue,
                Color = tile.Color,
                Icon = tile.Icon,
                DateCreated = tile.DateCreated
            };

            // return a 201 Created response with the location of the new tile
            return CreatedAtAction(nameof(GetTile), new { id = tile.Id }, tileDto);
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

    // PUT: api/tiles/{id} - updates an existing tile
    /// <summary>
    /// Updates an existing tile (Admin only)
    /// </summary>
    /// <param name="id">The tile's unique identifier</param>
    /// <param name="updateTileDto">The updated tile data</param>
    /// <returns>No content</returns>
    /// <response code="204">If the tile was successfully updated</response>
    /// <response code="401">If user is not authenticated</response>
    /// <response code="403">If user is not an Admin</response>
    /// <response code="404">If tile is not found</response>
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTile(Guid id, UpdateTileDto updateTileDto)
    {
        // update the tile in the database using the service
        var tile = await _tileService.UpdateTile(
            id, 
            updateTileDto.ItemName, 
            updateTileDto.TileValue, 
            updateTileDto.Color, 
            updateTileDto.Icon
        );
        
        if (tile == null)
        {
            return NotFound();
        }

        // successful update, return 204 No Content
        return NoContent();
    }

    // DELETE: api/tiles/{id} - deletes a tile
    /// <summary>
    /// Deletes a tile (Admin only)
    /// </summary>
    /// <param name="id">The tile's unique identifier</param>
    /// <returns>No content</returns>
    /// <response code="204">If the tile was successfully deleted</response>
    /// <response code="401">If user is not authenticated</response>
    /// <response code="403">If user is not an Admin</response>
    /// <response code="404">If tile is not found</response>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTile(Guid id)
    {
        var result = await _tileService.DeleteTile(id);
        
        if (!result)
        {
            return NotFound();
        }

        // successful deletion, return 204 No Content
        return NoContent();
    }
}