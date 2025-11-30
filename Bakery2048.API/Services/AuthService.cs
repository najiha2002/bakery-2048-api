using Bakery2048.API.Data;
using Bakery2048.API.Models;
using Bakery2048.API.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bakery2048.API.Services;

public class AuthService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly PlayerService _playerService;

    public AuthService(ApplicationDbContext context, IConfiguration configuration, PlayerService playerService)
    {
        _context = context;
        _configuration = configuration;
        _playerService = playerService;
    }

    // register a new user
    public async Task<AuthResponseDto> Register(RegisterDto registerDto)
    {
        // Validate username and email format using PlayerService validation
        _playerService.ValidateUsername(registerDto.Username);
        _playerService.ValidateEmail(registerDto.Email);

        // Check if user already exists
        if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
        {
            throw new InvalidOperationException("User with this email already exists.");
        }

        if (await _context.Users.AnyAsync(u => u.Username == registerDto.Username))
        {
            throw new InvalidOperationException("Username is already taken.");
        }

        // Check if player already exists (only for non-admin registrations)
        var isAdminRegistration = !string.IsNullOrEmpty(registerDto.Role) && registerDto.Role == "Admin";
        if (!isAdminRegistration && await _context.Players.AnyAsync(p => p.Email == registerDto.Email || p.Username == registerDto.Username))
        {
            throw new InvalidOperationException("Player with this email or username already exists.");
        }

        // hash the password
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

        // create new user
        var user = new User
        {
            Username = registerDto.Username,
            Email = registerDto.Email,
            PasswordHash = passwordHash,
            Role = string.IsNullOrEmpty(registerDto.Role) ? "Player" : registerDto.Role
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Create corresponding player record only for non-admin users
        if (user.Role != "Admin")
        {
            var player = new Player(user.Username, user.Email);
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
        }

        // generate JWT token
        var token = GenerateJwtToken(user);

        return new AuthResponseDto
        {
            Token = token,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role
        };
    }

    // login existing user
    public async Task<AuthResponseDto> Login(LoginDto loginDto)
    {
        // find user by username
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username);

        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        // verify password
        if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        // Generate JWT token
        var token = GenerateJwtToken(user);

        return new AuthResponseDto
        {
            Token = token,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role
        };
    }

    // generate JWT token
    private string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? throw new InvalidOperationException("JWT Key not configured")));
        
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration["Jwt:ExpiryInMinutes"] ?? "60")),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}