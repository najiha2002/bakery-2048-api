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
    public async Task<ActionResult<IEnumerable<Tile>>> GetTiles()
    {
        return await _context.Tiles.ToListAsync();
    }

    // GET: api/tiles/{id} - returns a specific tile by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Tile>> GetTile(int id)
    {
        var tile = await _context.Tiles.FindAsync(id);

        if (tile == null)
        {
            return NotFound();
        }

        return tile;
    }

    // POST: api/tiles - creates a new tile
    [HttpPost]
    public async Task<ActionResult<Tile>> CreateTile(CreateTileDto createTileDto)
    {
        var tile = new Tile
        {
            Name = createTileDto.Name,
            Value = createTileDto.Value,
            HexColor = createTileDto.HexColor,
            IconUrl = createTileDto.IconUrl
        };
        _context.Tiles.Add(tile);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTile), new { id = tile.Id }, tile);
    }

    // PUT: api/tiles/{id} - updates an existing tile
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTile(int id, UpdateTileDto updateTileDto)
    {
        var tile = await _context.Tiles.FindAsync(id);
        if (tile == null)
        {
            return NotFound();
        }
        tile.Name = updateTileDto.Name;
        tile.Value = updateTileDto.Value;
        tile.HexColor = updateTileDto.HexColor;
        tile.IconUrl = updateTileDto.IconUrl;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/tiles/{id} - deletes a tile
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTile(int id)
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