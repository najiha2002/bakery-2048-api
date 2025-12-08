using Bakery2048.API.Models;
using Xunit;

namespace Bakery2048.Tests;

public class UserTests
{
    [Fact]
    public void User_Creation_SetsPropertiesCorrectly()
    {
        // Arrange & Act
        var user = new User
        {
            Username = "testuser",
            Email = "test@example.com",
            PasswordHash = "hashedpassword",
            Role = "Player"
        };

        // Assert
        Assert.Equal("testuser", user.Username);
        Assert.Equal("test@example.com", user.Email);
        Assert.Equal("hashedpassword", user.PasswordHash);
        Assert.Equal("Player", user.Role);
    }

    [Theory]
    [InlineData("admin", "Admin")]
    [InlineData("player1", "Player")]
    [InlineData("moderator", "Player")]
    public void User_DifferentRoles_AssignCorrectly(string username, string role)
    {
        // Arrange & Act
        var user = new User
        {
            Username = username,
            Email = $"{username}@example.com",
            PasswordHash = "hash",
            Role = role
        };

        // Assert
        Assert.Equal(role, user.Role);
    }

    [Fact]
    public void User_DefaultRole_IsPlayer()
    {
        // Arrange & Act
        var user = new User
        {
            Username = "newuser",
            Email = "new@example.com",
            PasswordHash = "hash"
        };

        // Assert
        Assert.Equal("Player", user.Role);
    }
}
