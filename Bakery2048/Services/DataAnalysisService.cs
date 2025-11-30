using Bakery2048.Models;
using Bakery2048.Utilities;

namespace Bakery2048.Services;

public class DataAnalysisService
{
    private readonly PlayerService _playerService;
    private readonly TileService _tileService;
    private readonly PowerUpService _powerUpService;

    public DataAnalysisService(PlayerService playerService, TileService tileService, PowerUpService powerUpService)
    {
        _playerService = playerService;
        _tileService = tileService;
        _powerUpService = powerUpService;
    }

    public void ShowAnalysisMenu()
    {
        while (true)
        {
            Console.Clear();
            ConsoleUI.ShowTitle("📊 LINQ Data Analysis");
            ConsoleUI.MenuOption("1", "Player Analytics");
            ConsoleUI.MenuOption("2", "Power-Up Analytics");
            ConsoleUI.MenuOption("3", "Cross-Entity Analysis");
            ConsoleUI.MenuOption("4", "Advanced Queries");
            ConsoleUI.MenuOption("0", "Back to Main Menu");
            Console.WriteLine();

            string? choice = ConsoleUI.Prompt("Enter your choice");

            switch (choice)
            {
                case "1":
                    PlayerAnalytics();
                    break;
                case "2":
                    PowerUpAnalytics();
                    break;
                case "3":
                    CrossEntityAnalysis();
                    break;
                case "4":
                    AdvancedQueries();
                    break;
                case "0":
                    return;
                default:
                    ConsoleUI.Error("Invalid choice. Please try again.");
                    ConsoleUI.PauseForUser();
                    break;
            }
        }
    }

    private void PlayerAnalytics()
    {
        Console.Clear();
        ConsoleUI.ShowTitle("👥 Player Analytics (LINQ)");

        var playersField = typeof(BaseService<Player>).GetField("items",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var players = (List<Player>)playersField!.GetValue(_playerService)!;

        if (players.Count == 0)
        {
            ConsoleUI.Warning("No player data available for analysis.");
            ConsoleUI.PauseForUser();
            return;
        }

        Console.WriteLine();
        ConsoleUI.Header("📈 Score Distribution");
        
        // Players by score range
        var scoreRanges = new[]
        {
            new { Range = "0-1,000", Count = players.Count(p => p.HighestScore < 1000) },
            new { Range = "1,000-5,000", Count = players.Count(p => p.HighestScore >= 1000 && p.HighestScore < 5000) },
            new { Range = "5,000-10,000", Count = players.Count(p => p.HighestScore >= 5000 && p.HighestScore < 10000) },
            new { Range = "10,000-20,000", Count = players.Count(p => p.HighestScore >= 10000 && p.HighestScore < 20000) },
            new { Range = "20,000+", Count = players.Count(p => p.HighestScore >= 20000) }
        };

        foreach (var range in scoreRanges)
        {
            ConsoleUI.KeyValue($"  {range.Range}", $"{range.Count} players");
        }

        Console.WriteLine();
        ConsoleUI.Header("🎮 Player Activity");

        // Group by games played
        var activityLevels = players.GroupBy(p => 
            p.GamesPlayed switch
            {
                0 => "Never Played",
                <= 10 => "Casual (1-10)",
                <= 50 => "Regular (11-50)",
                <= 100 => "Active (51-100)",
                _ => "Hardcore (100+)"
            })
            .Select(g => new { Level = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count);

        foreach (var level in activityLevels)
        {
            ConsoleUI.KeyValue($"  {level.Level}", $"{level.Count} players");
        }

        Console.WriteLine();
        ConsoleUI.Header("⏱️ Engagement Metrics");

        var activePlayers = players.Where(p => p.GamesPlayed > 0).ToList();
        if (activePlayers.Any())
        {
            var avgPlayTime = activePlayers.Average(p => p.TotalPlayTime.TotalHours);
            var avgGames = activePlayers.Average(p => p.GamesPlayed);
            var avgMoves = activePlayers.Average(p => p.TotalMoves);

            ConsoleUI.KeyValue("  Avg Play Time", $"{avgPlayTime:F2} hours");
            ConsoleUI.KeyValue("  Avg Games Played", $"{avgGames:F1}");
            ConsoleUI.KeyValue("  Avg Total Moves", $"{avgMoves:F0}");
        }

        Console.WriteLine();
        ConsoleUI.Header("🏆 Top Performers");

        // Most efficient players (highest score per move)
        var efficient = players.Where(p => p.TotalMoves > 0)
            .OrderByDescending(p => (double)p.HighestScore / p.TotalMoves)
            .Take(5);

        Console.WriteLine("  Most Efficient (Score/Move):");
        foreach (var player in efficient)
        {
            double efficiency = (double)player.HighestScore / player.TotalMoves;
            Console.WriteLine($"    {player.Username,-20} {efficiency:F2}");
        }

        Console.WriteLine();
        ConsoleUI.Header("📅 Retention Analysis");

        var recentlyActive = players.Count(p => p.LastPlayed >= DateTime.Now.AddDays(-7));
        var dormant = players.Count(p => p.GamesPlayed > 0 && p.LastPlayed < DateTime.Now.AddDays(-30));

        ConsoleUI.KeyValue("  Active (Last 7 Days)", recentlyActive.ToString());
        ConsoleUI.KeyValue("  Dormant (30+ Days)", dormant.ToString());

        Console.WriteLine();
        ConsoleUI.PauseForUser();
    }

    private void PowerUpAnalytics()
    {
        Console.Clear();
        ConsoleUI.ShowTitle("⚡ Power-Up Analytics (LINQ)");

        var powerUpsField = typeof(BaseService<PowerUp>).GetField("items",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var powerUps = (List<PowerUp>)powerUpsField!.GetValue(_powerUpService)!;

        if (powerUps.Count == 0)
        {
            ConsoleUI.Warning("No power-up data available for analysis.");
            ConsoleUI.PauseForUser();
            return;
        }

        Console.WriteLine();
        ConsoleUI.Header("📊 Usage Statistics");

        var totalUsage = powerUps.Sum(p => p.UsageCount);
        var avgUsage = powerUps.Average(p => p.UsageCount);

        ConsoleUI.KeyValue("  Total Power-Ups Used", totalUsage.ToString());
        ConsoleUI.KeyValue("  Average Usage per Power-Up", $"{avgUsage:F1}");

        Console.WriteLine();
        ConsoleUI.Header("🔥 Most Popular Power-Ups");

        var topUsed = powerUps.OrderByDescending(p => p.UsageCount).Take(5);
        int rank = 1;
        foreach (var powerUp in topUsed)
        {
            Console.WriteLine($"  {rank}. {powerUp.PowerUpName,-25} {powerUp.UsageCount} uses");
            rank++;
        }

        Console.WriteLine();
        ConsoleUI.Header("💰 Cost Analysis");

        var costGroups = powerUps.GroupBy(p => 
            p.Cost switch
            {
                < 200 => "Cheap (<200)",
                <= 300 => "Medium (200-300)",
                <= 400 => "Expensive (301-400)",
                _ => "Premium (400+)"
            })
            .Select(g => new { Category = g.Key, Count = g.Count(), AvgUsage = g.Average(p => p.UsageCount) });

        foreach (var group in costGroups)
        {
            ConsoleUI.KeyValue($"  {group.Category}", $"{group.Count} power-ups, Avg: {group.AvgUsage:F1} uses");
        }

        Console.WriteLine();
        ConsoleUI.Header("🎯 Power-Up Types");

        var byType = powerUps.GroupBy(p => p.PowerUpType)
            .Select(g => new { Type = g.Key, Count = g.Count(), TotalUsage = g.Sum(p => p.UsageCount) })
            .OrderByDescending(x => x.TotalUsage);

        foreach (var type in byType)
        {
            ConsoleUI.KeyValue($"  {type.Type}", $"{type.Count} power-ups, {type.TotalUsage} total uses");
        }

        Console.WriteLine();
        ConsoleUI.Header("🔓 Unlock Status");

        var unlocked = powerUps.Count(p => p.IsUnlocked);
        var locked = powerUps.Count(p => !p.IsUnlocked);

        ConsoleUI.KeyValue("  Unlocked", $"{unlocked} ({(double)unlocked / powerUps.Count * 100:F1}%)");
        ConsoleUI.KeyValue("  Locked", $"{locked} ({(double)locked / powerUps.Count * 100:F1}%)");

        Console.WriteLine();
        ConsoleUI.PauseForUser();
    }

    private void CrossEntityAnalysis()
    {
        Console.Clear();
        ConsoleUI.ShowTitle("🔗 Cross-Entity Analysis (LINQ)");

        var playersField = typeof(BaseService<Player>).GetField("items",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var players = (List<Player>)playersField!.GetValue(_playerService)!;

        var powerUpsField = typeof(BaseService<PowerUp>).GetField("items",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var powerUps = (List<PowerUp>)powerUpsField!.GetValue(_powerUpService)!;

        if (players.Count == 0 || powerUps.Count == 0)
        {
            ConsoleUI.Warning("Insufficient data for cross-entity analysis.");
            ConsoleUI.PauseForUser();
            return;
        }

        Console.WriteLine();
        ConsoleUI.Header("💎 Power-Up Impact Analysis");

        var activePlayers = players.Where(p => p.GamesPlayed > 0).ToList();

        if (activePlayers.Any())
        {
            // Players who use power-ups vs those who don't
            var powerUpUsers = activePlayers.Where(p => p.PowerUpsUsed > 0).ToList();
            var nonUsers = activePlayers.Where(p => p.PowerUpsUsed == 0).ToList();

            if (powerUpUsers.Any() && nonUsers.Any())
            {
                var avgScoreWithPowerUps = powerUpUsers.Average(p => p.HighestScore);
                var avgScoreWithoutPowerUps = nonUsers.Average(p => p.HighestScore);

                ConsoleUI.KeyValue("  Players Using Power-Ups", powerUpUsers.Count.ToString());
                ConsoleUI.KeyValue("  Avg Score (With Power-Ups)", $"{avgScoreWithPowerUps:F0}");
                ConsoleUI.KeyValue("  Players Not Using Power-Ups", nonUsers.Count.ToString());
                ConsoleUI.KeyValue("  Avg Score (Without)", $"{avgScoreWithoutPowerUps:F0}");
                
                var difference = avgScoreWithPowerUps - avgScoreWithoutPowerUps;
                var percentIncrease = (difference / avgScoreWithoutPowerUps) * 100;
                ConsoleUI.KeyValue("  Score Increase", $"+{difference:F0} (+{percentIncrease:F1}%)");
            }
        }

        Console.WriteLine();
        ConsoleUI.Header("🎮 Player Progression");

        // Players who reached 2048 tile
        var winners = players.Where(p => p.BestTileAchieved >= 2048).ToList();
        var winRate = winners.Count > 0 ? (double)winners.Count / players.Count(p => p.GamesPlayed > 0) * 100 : 0;

        ConsoleUI.KeyValue("  Players Reached 2048+", winners.Count.ToString());
        ConsoleUI.KeyValue("  Win Rate", $"{winRate:F1}%");

        if (winners.Any())
        {
            var avgGamesToWin = winners.Average(p => p.GamesPlayed);
            var avgPowerUpsUsedByWinners = winners.Average(p => p.PowerUpsUsed);
            
            ConsoleUI.KeyValue("  Avg Games to Win", $"{avgGamesToWin:F1}");
            ConsoleUI.KeyValue("  Avg Power-Ups Used by Winners", $"{avgPowerUpsUsedByWinners:F1}");
        }

        Console.WriteLine();
        ConsoleUI.Header("🏅 Elite Players Analysis");

        // Top 10% of players
        var topPercent = (int)Math.Ceiling(players.Count * 0.1);
        var elitePlayers = players.OrderByDescending(p => p.HighestScore).Take(topPercent).ToList();

        if (elitePlayers.Any())
        {
            var eliteAvgPowerUps = elitePlayers.Average(p => p.PowerUpsUsed);
            var regularAvgPowerUps = players.Except(elitePlayers).Average(p => p.PowerUpsUsed);

            ConsoleUI.KeyValue("  Elite Players (Top 10%)", elitePlayers.Count.ToString());
            ConsoleUI.KeyValue("  Avg Power-Ups (Elite)", $"{eliteAvgPowerUps:F1}");
            ConsoleUI.KeyValue("  Avg Power-Ups (Regular)", $"{regularAvgPowerUps:F1}");
        }

        Console.WriteLine();
        ConsoleUI.PauseForUser();
    }

    private void AdvancedQueries()
    {
        Console.Clear();
        ConsoleUI.ShowTitle("🔬 Advanced LINQ Queries");

        var playersField = typeof(BaseService<Player>).GetField("items",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var players = (List<Player>)playersField!.GetValue(_playerService)!;

        var powerUpsField = typeof(BaseService<PowerUp>).GetField("items",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var powerUps = (List<PowerUp>)powerUpsField!.GetValue(_powerUpService)!;

        Console.WriteLine();
        ConsoleUI.Header("🎯 Complex Aggregations");

        // Standard deviation of scores
        if (players.Any(p => p.GamesPlayed > 0))
        {
            var scores = players.Where(p => p.GamesPlayed > 0).Select(p => p.HighestScore).ToList();
            var avg = scores.Average();
            var variance = scores.Select(s => Math.Pow(s - avg, 2)).Average();
            var stdDev = Math.Sqrt(variance);

            ConsoleUI.KeyValue("  Average Score", $"{avg:F0}");
            ConsoleUI.KeyValue("  Std Deviation", $"{stdDev:F0}");
            ConsoleUI.KeyValue("  Score Range", $"{scores.Min()} - {scores.Max()}");
        }

        Console.WriteLine();
        ConsoleUI.Header("📊 Percentile Analysis");

        var sortedPlayers = players.Where(p => p.GamesPlayed > 0).OrderBy(p => p.HighestScore).ToList();
        if (sortedPlayers.Count > 0)
        {
            var p25 = sortedPlayers.ElementAt((int)(sortedPlayers.Count * 0.25));
            var p50 = sortedPlayers.ElementAt((int)(sortedPlayers.Count * 0.50));
            var p75 = sortedPlayers.ElementAt((int)(sortedPlayers.Count * 0.75));
            var p90 = sortedPlayers.ElementAt((int)(sortedPlayers.Count * 0.90));

            ConsoleUI.KeyValue("  25th Percentile", p25.HighestScore.ToString());
            ConsoleUI.KeyValue("  50th Percentile (Median)", p50.HighestScore.ToString());
            ConsoleUI.KeyValue("  75th Percentile", p75.HighestScore.ToString());
            ConsoleUI.KeyValue("  90th Percentile", p90.HighestScore.ToString());
        }

        Console.WriteLine();
        ConsoleUI.Header("🔍 Correlation Insights");

        // Power-up usage vs score correlation
        var powerUpCorrelation = players.Where(p => p.GamesPlayed > 0 && p.PowerUpsUsed > 0).ToList();
        if (powerUpCorrelation.Count >= 2)
        {
            var avgPowerUpsPerGame = powerUpCorrelation.Average(p => (double)p.PowerUpsUsed / p.GamesPlayed);
            var avgScorePerGame = powerUpCorrelation.Average(p => (double)p.HighestScore / p.GamesPlayed);

            ConsoleUI.KeyValue("  Avg Power-Ups per Game", $"{avgPowerUpsPerGame:F2}");
            ConsoleUI.KeyValue("  Avg Score per Game", $"{avgScorePerGame:F0}");
        }

        Console.WriteLine();
        ConsoleUI.Header("🎲 Uncommon Patterns");

        // Players with perfect efficiency (never inactive, high win streak)
        var perfectPlayers = players.Where(p => 
            p.GamesPlayed > 10 && 
            p.WinStreak >= 5 && 
            p.IsActive
        ).ToList();

        ConsoleUI.KeyValue("  'Perfect' Players (10+ games, 5+ streak)", perfectPlayers.Count.ToString());

        // Power-ups that are never used despite being unlocked
        var unusedUnlocked = powerUps.Where(p => p.IsUnlocked && p.UsageCount == 0).ToList();
        ConsoleUI.KeyValue("  Unlocked but Unused Power-Ups", unusedUnlocked.Count.ToString());

        if (unusedUnlocked.Any())
        {
            Console.WriteLine("    " + string.Join(", ", unusedUnlocked.Select(p => p.PowerUpName)));
        }

        Console.WriteLine();
        ConsoleUI.PauseForUser();
    }
}
