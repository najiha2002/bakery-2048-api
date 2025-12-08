using Bakery2048.API.Models;
using Xunit;

namespace Bakery2048.Tests;

public class PlayerTests
{
    [Fact]
    public void Player_Creation_SetsDefaultValues()
    {
        // Arrange & Act
        var player = new Player("testuser", "test@example.com");

        // Assert
        Assert.Equal("testuser", player.Username);
        Assert.Equal("test@example.com", player.Email);
        Assert.Equal(0, player.HighestScore);
        Assert.Equal(0, player.CurrentScore);
        Assert.Equal(0, player.GamesPlayed);
        Assert.Equal(0, player.WinStreak);
        Assert.True(player.IsActive);
    }

    [Fact]
    public void Player_UpdateScore_WorksCorrectly()
    {
        // Arrange
        var player = new Player("testuser", "test@example.com");

        // Act
        player.HighestScore = 5000;
        player.CurrentScore = 3000;
        player.GamesPlayed = 10;
        player.WinStreak = 5;

        // Assert
        Assert.Equal(5000, player.HighestScore);
        Assert.Equal(3000, player.CurrentScore);
        Assert.Equal(10, player.GamesPlayed);
        Assert.Equal(5, player.WinStreak);
    }

    [Theory]
    [InlineData(1000, 500, 5, 2)]
    [InlineData(8500, 7200, 25, 5)]
    [InlineData(15000, 12000, 60, 12)]
    public void Player_MultipleScenarios_UpdateCorrectly(int highScore, int currentScore, int games, int streak)
    {
        // Arrange
        var player = new Player("player", "player@example.com");

        // Act
        player.HighestScore = highScore;
        player.CurrentScore = currentScore;
        player.GamesPlayed = games;
        player.WinStreak = streak;

        // Assert
        Assert.Equal(highScore, player.HighestScore);
        Assert.Equal(currentScore, player.CurrentScore);
        Assert.Equal(games, player.GamesPlayed);
        Assert.Equal(streak, player.WinStreak);
    }
}
