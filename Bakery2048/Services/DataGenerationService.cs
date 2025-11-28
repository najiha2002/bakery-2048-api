using Bakery2048.Models;
using Bakery2048.Utilities;

namespace Bakery2048.Services;

public class DataGenerationService
{
    private readonly PlayerService _playerService;
    private readonly TileService _tileService;
    private readonly PowerUpService _powerUpService;
    private readonly Random _random;

    private static readonly string[] FirstNames = {
        "Baker", "Chef", "Pastry", "Cookie", "Cake", "Sweet", "Sugar", "Flour",
        "Butter", "Cream", "Frost", "Candy", "Choco", "Vanilla", "Cinnamon",
        "Honey", "Maple", "Berry", "Apple", "Cherry", "Lemon", "Orange",
        "Mint", "Caramel", "Mocha", "Espresso", "Cocoa", "Truffle", "Tart"
    };

    private static readonly string[] Suffixes = {
        "Master", "King", "Queen", "Pro", "Legend", "Guru", "Wizard", "Ninja",
        "Boss", "Hero", "Star", "Ace", "Champion", "Elite", "Supreme",
        "Prime", "Ultimate", "Grand", "Royal", "Divine", "Epic", "Mega"
    };

    public DataGenerationService(PlayerService playerService, TileService tileService, PowerUpService powerUpService)
    {
        _playerService = playerService;
        _tileService = tileService;
        _powerUpService = powerUpService;
        _random = new Random();
    }

    public void GenerateAll()
    {
        Console.Clear();
        ConsoleUI.ShowTitle("🎲 Generate Random Data");
        Console.WriteLine();

        ConsoleUI.Info("This will generate 50-100 random players with game sessions.");
        Console.Write("Continue? (y/n): ");
        if (Console.ReadLine()?.ToLower() != "y")
        {
            ConsoleUI.Warning("Data generation cancelled.");
            ConsoleUI.PauseForUser();
            return;
        }

        Console.WriteLine();
        ConsoleUI.Info("Generating random data... Please wait.");
        Console.WriteLine();

        var startTime = DateTime.Now;
        
        int playersGenerated = GeneratePlayers();
        int sessionsGenerated = GenerateGameSessions();

        var duration = DateTime.Now - startTime;

        Console.WriteLine();
        ConsoleUI.Success("Data generation complete!");
        Console.WriteLine();
        ConsoleUI.KeyValue("Players Generated", playersGenerated.ToString());
        ConsoleUI.KeyValue("Game Sessions Simulated", sessionsGenerated.ToString());
        ConsoleUI.KeyValue("Time Taken", $"{duration.TotalSeconds:F2} seconds");
        Console.WriteLine();

        ConsoleUI.PauseForUser();
    }

    private int GeneratePlayers()
    {
        int count = _random.Next(50, 76); // Generate 50-75 players (reduced from 101)
        Console.Write($"Generating {count} players... ");

        // Access the protected items list through reflection
        var playersField = typeof(BaseService<Player>).GetField("items", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var playersList = (List<Player>)playersField!.GetValue(_playerService)!;

        for (int i = 0; i < count; i++)
        {
            string username = GenerateUsername();
            string email = GenerateEmail(username);

            var player = new Player(username, email);

            // Give some players initial stats to make data more realistic
            if (_random.Next(100) < 70) // 70% of players have played at least once
            {
                player.HighestScore = _random.Next(100, 50000);
                player.GamesPlayed = _random.Next(1, 150);
                player.Level = (player.HighestScore / 1000) + 1;
                player.BestTileAchieved = GetRandomTileValue();
                player.TotalMoves = player.GamesPlayed * _random.Next(50, 300);
                player.PowerUpsUsed = _random.Next(0, player.GamesPlayed * 3);
                player.TotalPlayTime = TimeSpan.FromHours(_random.NextDouble() * player.GamesPlayed * 0.5);
                player.WinStreak = _random.Next(0, 10);

                if (player.GamesPlayed > 0)
                {
                    player.AverageScore = player.HighestScore / _random.Next(2, 5);
                    player.CurrentScore = _random.Next(0, player.HighestScore);
                }

                // Random last played date (within last 60 days)
                player.LastPlayed = DateTime.Now.AddDays(-_random.Next(0, 60));
            }

            playersList.Add(player);
        }

        // Save to file
        var saveMethod = typeof(BaseService<Player>).GetMethod("SaveToFile",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        saveMethod!.Invoke(_playerService, null);

        Console.WriteLine("✓");
        return count;
    }

    private int GenerateGameSessions()
    {
        // Access players list using reflection
        var playersField = typeof(BaseService<Player>).GetField("items",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var playersList = (List<Player>)playersField!.GetValue(_playerService)!;
        
        var activePlayers = playersList.Where(p => p.GamesPlayed > 0).ToList();
        if (activePlayers.Count == 0)
        {
            ConsoleUI.Warning("No active players to generate sessions for.");
            return 0;
        }

        int sessionCount = 0;
        Console.Write($"Simulating game sessions for {activePlayers.Count} players... ");

        // Access power-ups list using reflection
        var powerUpsField = typeof(BaseService<PowerUp>).GetField("items",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var powerUpsList = (List<PowerUp>)powerUpsField!.GetValue(_powerUpService)!;

        foreach (var player in activePlayers)
        {
            // Generate 1-3 sessions per player (reduced from 1-6)
            int sessions = _random.Next(1, 4);
            
            for (int i = 0; i < sessions; i++)
            {
                int finalScore = _random.Next(500, 20000);
                int bestTile = GetRandomTileValue();
                int moves = _random.Next(50, 500);
                TimeSpan duration = TimeSpan.FromMinutes(_random.Next(5, 45));
                bool won = _random.Next(100) < 20; // 20% win rate

                // Randomly select power-ups used in this session
                var availablePowerUps = powerUpsList.Where(p => p.IsUnlocked).ToList();
                int powerUpsInSession = _random.Next(0, Math.Min(5, availablePowerUps.Count));
                
                for (int j = 0; j < powerUpsInSession; j++)
                {
                    var powerUp = availablePowerUps[_random.Next(availablePowerUps.Count)];
                    powerUp.UsageCount++;
                }

                // Update player stats directly
                player.CurrentScore = finalScore;
                if (finalScore > player.HighestScore)
                {
                    player.HighestScore = finalScore;
                }
                if (bestTile > player.BestTileAchieved)
                {
                    player.BestTileAchieved = bestTile;
                }
                player.GamesPlayed++;
                player.TotalMoves += moves;
                player.TotalPlayTime += duration;
                player.PowerUpsUsed += powerUpsInSession;
                player.LastPlayed = DateTime.Now;
                player.CalculateLevelFromScore();
                
                if (won)
                {
                    player.IncrementWinStreak();
                }
                else if (_random.Next(100) < 30)
                {
                    player.ResetWinStreak();
                }

                // Recalculate average
                if (player.GamesPlayed > 0)
                {
                    player.AverageScore = player.HighestScore / _random.Next(2, 4);
                }

                sessionCount++;
            }
        }

        // Save both services
        var savePlayerMethod = typeof(BaseService<Player>).GetMethod("SaveToFile",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        savePlayerMethod!.Invoke(_playerService, null);

        var savePowerUpMethod = typeof(BaseService<PowerUp>).GetMethod("SaveToFile",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        savePowerUpMethod!.Invoke(_powerUpService, null);

        Console.WriteLine("✓");
        return sessionCount;
    }

    private string GenerateUsername()
    {
        string first = FirstNames[_random.Next(FirstNames.Length)];
        string suffix = Suffixes[_random.Next(Suffixes.Length)];
        int number = _random.Next(1, 1000);

        return _random.Next(3) switch
        {
            0 => $"{first}{suffix}",
            1 => $"{first}{number}",
            _ => $"{first}{suffix}{number}"
        };
    }

    private string GenerateEmail(string username)
    {
        string[] domains = { "gmail.com", "yahoo.com", "bakery.com", "chef.net", "cooking.io", "pastry.org" };
        string domain = domains[_random.Next(domains.Length)];
        return $"{username.ToLower()}@{domain}";
    }

    private int GetRandomTileValue()
    {
        // Common tile values in 2048: 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096
        int[] tileValues = { 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096 };
        
        // Weight towards lower values (more common in gameplay)
        int weightedIndex = (int)Math.Floor(Math.Pow(_random.NextDouble(), 2) * tileValues.Length);
        return tileValues[Math.Min(weightedIndex, tileValues.Length - 1)];
    }
}
