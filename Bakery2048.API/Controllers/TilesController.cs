using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bakery2048.API.Data;
using Bakery2048.API.DTOs;
using Bakery2048.Models;

namespace Bakery2048.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TilesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    // Constructor injection — gives the controller access to the database
    public TilesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/tiles - returns all tiles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TileResponseDto>>> GetTiles()
    {
        var tiles = await _context.Tiles.ToListAsync();
        
        var tileDtos = tiles.Select(t => new TileResponseDto
        {
            Id = t.Id,
            ItemName = t.ItemName,
            TileValue = t.TileValue,
            Color = t.Color,
            Icon = t.Icon,
            DateCreated = t.DateCreated
        }).ToList();
        
        return Ok(tileDtos);
    }

    // GET: api/tiles/{id} - returns a specific tile by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<TileResponseDto>> GetTile(Guid id)
    {
        var tile = await _context.Tiles.FindAsync(id);

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

        return Ok(tileDto);
    }

    // POST: api/tiles - creates a new tile
    [HttpPost]
    public async Task<ActionResult<TileResponseDto>> CreateTile(CreateTileDto createTileDto)
    {
        var tile = new Tile(createTileDto.ItemName, createTileDto.TileValue)
        {
            Color = createTileDto.Color,
            Icon = createTileDto.Icon
        };
        _context.Tiles.Add(tile);
        await _context.SaveChangesAsync();

        var tileDto = new TileResponseDto
        {
            Id = tile.Id,
            ItemName = tile.ItemName,
            TileValue = tile.TileValue,
            Color = tile.Color,
            Icon = tile.Icon,
            DateCreated = tile.DateCreated
        };

        return CreatedAtAction(nameof(GetTile), new { id = tile.Id }, tileDto);
    }

    // PUT: api/tiles/{id} - updates an existing tile
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTile(Guid id, UpdateTileDto updateTileDto)
    {
        var tile = await _context.Tiles.FindAsync(id);
        if (tile == null)
        {
            return NotFound();
        }
        tile.ItemName = updateTileDto.ItemName;
        tile.TileValue = updateTileDto.TileValue;
        tile.Color = updateTileDto.Color;
        tile.Icon = updateTileDto.Icon;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/tiles/{id} - deletes a tile
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTile(Guid id)
    {
        var tile = await _context.Tiles.FindAsync(id);
        if (tile == null)
        {
            return NotFound();
        }
        _context.Tiles.Remove(tile);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}