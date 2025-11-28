using System.Text.Json;
using Bakery2048.Services;
using Bakery2048.Utilities;

public class PlayerService : BaseService<Player>
{
    public PlayerService(List<Player> playerList) : base(playerList, "players.json")
    {
    }

    public void RegisterPlayer()
    {
        ConsoleUI.SimpleHeader("Player Registration");
        
        string name = ConsoleUI.Prompt("Enter your name", ConsoleColor.Cyan);

        if (string.IsNullOrWhiteSpace(name))
        {
            ConsoleUI.Error("Name cannot be empty.");
            return;
        }

        // Check if player already exists
        if (items.Any(p => p.Username.Equals(name, StringComparison.OrdinalIgnoreCase)))
        {
            ConsoleUI.Warning($"Player '{name}' already registered.");
            return;
        }

        string email = ConsoleUI.Prompt("Enter your email", ConsoleColor.Cyan);

        if (string.IsNullOrWhiteSpace(email))
        {
            ConsoleUI.Error("Email cannot be empty.");
            return;
        }

        Player newPlayer = new Player(name, email);
        items.Add(newPlayer);

        SaveToFile();

        Console.WriteLine();
        ConsoleUI.Success($"Welcome to Bakery 2048, {name}!");
        ConsoleUI.KeyValue("Player ID", newPlayer.PlayerId.ToString(), ConsoleColor.DarkGray);
        ConsoleUI.KeyValue("Registration Date", newPlayer.DateRegistered.ToString("yyyy-MM-dd HH:mm"), ConsoleColor.DarkGray);
        ConsoleUI.KeyValue("Starting Rank", newPlayer.GetRankCategory(), ConsoleColor.Yellow);
        
        Console.WriteLine();
        if (ConsoleUI.Confirm("Do you want to record a game session now?"))
        {
            RecordGameSession(newPlayer);
        }
    }

    public void ViewAllPlayers()
    {
        ConsoleUI.SimpleHeader("All Players");

        if (items.Count == 0)
        {
            ConsoleUI.Warning("No players found.");
            return;
        }

        // Header row
        string[] headers = { "Username", "Level", "High Score", "Games Played", "Status" };
        int[] widths = { 20, 8, 12, 15, 10 };
        ConsoleUI.TableRow(headers, widths, true);
        ConsoleUI.Divider('─', 75);

        foreach (var player in items)
        {
            string status = player.IsActive ? "Active" : "Inactive";
            string[] row = { 
                player.Username, 
                player.Level.ToString(), 
                player.HighestScore.ToString(), 
                player.GamesPlayed.ToString(), 
                status 
            };
            
            // Color code status
            Console.Write($"{row[0],-20} {row[1],-8} {row[2],-12} {row[3],-15} ");
            ConsoleUI.WriteLineColored(row[4], player.IsActive ? ConsoleColor.Green : ConsoleColor.DarkGray);
        }

        ConsoleUI.PauseForUser();
    }

    public void SearchPlayer()
    {
        Console.Write("\nEnter player username to search: ");
        string searchName = Console.ReadLine() ?? "";

        var foundPlayers = items.Where(p => p.Username.Contains(searchName, StringComparison.OrdinalIgnoreCase)).ToList();

        if (foundPlayers.Count == 0)
        {
            Console.WriteLine($"No players found with username containing '{searchName}'.");
            return;
        }

        Console.WriteLine($"\n=== Found {foundPlayers.Count} Player(s) ===");
        foreach (var player in foundPlayers)
        {
            Console.WriteLine($"\n{player.GetPlayerStats()}");
            Console.WriteLine($"Rank: {player.GetRankCategory()}");
            Console.WriteLine(new string('-', 50));
        }

        PauseForUser();
    }

    public void UpdatePlayer()
    {
        Console.Write("\nEnter player username to update: ");
        string searchName = Console.ReadLine() ?? "";

        var player = items.FirstOrDefault(p => p.Username.Equals(searchName, StringComparison.OrdinalIgnoreCase));

        if (player == null)
        {
            Console.WriteLine($"Player '{searchName}' not found.");
            return;
        }

        Console.WriteLine($"\nUpdating player: {player.Username}");
        Console.WriteLine("1. Update Email");
        Console.WriteLine("2. Update Score");
        Console.WriteLine("3. Update Level");
        Console.WriteLine("4. Toggle Active Status");
        Console.WriteLine("5. Add Play Time");
        Console.WriteLine("6. Cancel");
        Console.Write("Select option: ");

        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Write("Enter new email: ");
                player.Email = Console.ReadLine() ?? "";
                Console.WriteLine("✓ Email updated.");
                break;
            case "2":
                Console.Write("Enter new score: ");
                if (int.TryParse(Console.ReadLine(), out int score))
                {
                    player.UpdateScore(score);
                    player.IncrementGamesPlayed();
                    Console.WriteLine($"✓ Score updated. New high score: {player.HighestScore}");
                }
                else
                {
                    Console.WriteLine("Invalid score.");
                }
                break;
            case "3":
                Console.Write("Enter new level: ");
                if (int.TryParse(Console.ReadLine(), out int level))
                {
                    player.Level = level;
                    Console.WriteLine("✓ Level updated.");
                }
                else
                {
                    Console.WriteLine("Invalid level.");
                }
                break;
            case "4":
                if (player.IsActive)
                {
                    player.Deactivate();
                    Console.WriteLine("✓ Player deactivated.");
                }
                else
                {
                    player.Activate();
                    Console.WriteLine("✓ Player activated.");
                }
                break;
            case "5":
                Console.Write("Enter hours played: ");
                if (double.TryParse(Console.ReadLine(), out double hours))
                {
                    player.AddPlayTime(TimeSpan.FromHours(hours));
                    Console.WriteLine($"✓ Play time added. Total: {player.TotalPlayTime.TotalHours:F2} hours");
                }
                else
                {
                    Console.WriteLine("Invalid hours.");
                }
                break;
            case "6":
                Console.WriteLine("Update cancelled.");
                return;
            default:
                Console.WriteLine("Invalid option.");
                return;
        }

        SaveToFile();
        
        PauseForUser();
    }

    public void DeletePlayer()
    {
        Console.Write("\nEnter player username to delete: ");
        string searchName = Console.ReadLine() ?? "";

        var player = items.FirstOrDefault(p => p.Username.Equals(searchName, StringComparison.OrdinalIgnoreCase));

        if (player == null)
        {
            Console.WriteLine($"Player '{searchName}' not found.");
            return;
        }

        Console.Write($"Are you sure you want to delete player '{player.Username}'? (yes/no): ");
        string confirm = Console.ReadLine()?.ToLower() ?? "";

        if (confirm == "yes" || confirm == "y")
        {
            items.Remove(player);
            SaveToFile();
            Console.WriteLine($"✓ Player '{player.Username}' deleted successfully.");
        }
        else
        {
            Console.WriteLine("Deletion cancelled.");
        }

        PauseForUser();
    }

    public void ViewPlayerStatistics()
    {
        if (items.Count == 0)
        {
            ConsoleUI.Warning("No players available for statistics.");
            return;
        }

        ConsoleUI.SimpleHeader("Player Statistics");

        // Basic stats
        ConsoleUI.KeyValue("Total Players", items.Count.ToString(), ConsoleColor.Cyan);
        ConsoleUI.KeyValue("Active Players", items.Count(p => p.IsActive).ToString(), ConsoleColor.Green);
        ConsoleUI.KeyValue("Inactive Players", items.Count(p => !p.IsActive).ToString(), ConsoleColor.DarkGray);
        ConsoleUI.KeyValue("Average Score", items.Average(p => p.HighestScore).ToString("F2"), ConsoleColor.Yellow);
        ConsoleUI.KeyValue("Highest Score", items.Max(p => p.HighestScore).ToString(), ConsoleColor.Magenta);
        ConsoleUI.KeyValue("Total Games Played", items.Sum(p => p.GamesPlayed).ToString(), ConsoleColor.Blue);

        var topPlayer = items.OrderByDescending(p => p.HighestScore).FirstOrDefault();
        if (topPlayer != null)
        {
            Console.WriteLine();
            ConsoleUI.WriteLineColored("🏆 Top Player", ConsoleColor.Yellow);
            ConsoleUI.KeyValue("   Username", topPlayer.Username, ConsoleColor.White);
            ConsoleUI.KeyValue("   Score", topPlayer.HighestScore.ToString(), ConsoleColor.Magenta);
            ConsoleUI.KeyValue("   Rank", topPlayer.GetRankCategory(), ConsoleColor.Yellow);
        }

        Console.WriteLine();
        ConsoleUI.WriteLineColored("═══ Top 5 Leaderboard ═══", ConsoleColor.Cyan);
        var top5 = items.OrderByDescending(p => p.HighestScore).Take(5);
        int rank = 1;
        foreach (var player in top5)
        {
            ConsoleUI.WriteColored($"{rank}. ", ConsoleColor.Yellow);
            Console.WriteLine($"{player.GetLeaderboardEntry()} - {player.GetRankCategory()}");
            rank++;
        }

        ConsoleUI.PauseForUser();
    }

    public override void ShowMenu()
    {
        bool back = false;

        while (!back)
        {
            ConsoleUI.SimpleHeader("Player Management");
            ConsoleUI.MenuOption("1", "Register New Player");
            ConsoleUI.MenuOption("2", "View All Players");
            ConsoleUI.MenuOption("3", "Search Player by Username");
            ConsoleUI.MenuOption("4", "Update Player Info");
            ConsoleUI.MenuOption("5", "Delete Player");
            ConsoleUI.MenuOption("6", "View Player Statistics");
            ConsoleUI.MenuOption("7", "Record New Game Session");
            ConsoleUI.MenuOption("8", "Back to Main Menu");
            
            string? input = ConsoleUI.Prompt("\nSelect an option (1-8)", ConsoleColor.Yellow);

            switch (input)
            {
                case "1":
                    RegisterPlayer();
                    break;
                case "2":
                    ViewAllPlayers();
                    break;
                case "3":
                    SearchPlayer();
                    break;
                case "4":
                    UpdatePlayer();
                    break;
                case "5":
                    DeletePlayer();
                    break;
                case "6":
                    ViewPlayerStatistics();
                    break;
                case "7":
                    RecordGameSession();
                    break;
                case "8":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    public void RecordGameSession(Player? player = null)
    {
        // get player by username if not provided
        if (player == null)
        {
            Console.Write("\nEnter player username: ");
            string searchName = Console.ReadLine() ?? "";

            player = items.FirstOrDefault(p => p.Username.Equals(searchName, StringComparison.OrdinalIgnoreCase));

            if (player == null)
            {
                Console.WriteLine($"Player '{searchName}' not found.");
                return;
            }
        }

        Console.WriteLine($"\n=== Recording Game Session for {player.Username} ===");

        // get final score
        Console.Write("Enter final score: ");
        if (!int.TryParse(Console.ReadLine(), out int finalScore) || finalScore < 0)
        {
            Console.WriteLine("Invalid score.");
            return;
        }

        // get best tile achieved
        Console.Write("Enter best tile achieved (e.g., 2048, 4096): ");
        if (!int.TryParse(Console.ReadLine(), out int bestTile) || bestTile < 0)
        {
            Console.WriteLine("Invalid tile value.");
            return;
        }

        // get moves made
        Console.Write("Enter number of moves made: ");
        if (!int.TryParse(Console.ReadLine(), out int moves) || moves < 0)
        {
            Console.WriteLine("Invalid number of moves.");
            return;
        }

        // Get play duration
        Console.Write("Enter play duration in minutes: ");
        if (!double.TryParse(Console.ReadLine(), out double minutes) || minutes < 0)
        {
            Console.WriteLine("Invalid duration.");
            return;
        }

        // Get power-ups used with names
        List<string> powerUpsUsedInSession = new List<string>();
        Console.Write("Enter number of power-ups used (0 if none): ");
        if (int.TryParse(Console.ReadLine(), out int powerUpCount) && powerUpCount > 0)
        {
            Console.WriteLine("\nEnter the name of each power-up used:");
            for (int i = 1; i <= powerUpCount; i++)
            {
                Console.Write($"Power-up #{i}: ");
                string? powerUpName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(powerUpName))
                {
                    powerUpsUsedInSession.Add(powerUpName.Trim());
                }
            }
        }

        // check if reached win condition (e.g., 2048 tile)
        bool reachedWin = bestTile >= 2048 ? true : false;

        // record the session
        TimeSpan playDuration = TimeSpan.FromMinutes(minutes);
        player.RecordGameSession(finalScore, bestTile, moves, playDuration, powerUpsUsedInSession.Count > 0 ? powerUpsUsedInSession : null, reachedWin);

        SaveToFile();

        Console.WriteLine("\n✓ Game session recorded successfully!");
        Console.WriteLine($"High Score: {player.HighestScore}");
        Console.WriteLine($"Best Tile Ever: {player.BestTileAchieved}");
        Console.WriteLine($"Games Played: {player.GamesPlayed}");
        Console.WriteLine($"Average Score: {player.AverageScore:F2}");
        Console.WriteLine($"Current Level: {player.Level}");
        Console.WriteLine($"Rank: {player.GetRankCategory()}");
        Console.WriteLine($"Win Streak: {player.WinStreak}");
        Console.WriteLine($"Total Power-Ups Used: {player.PowerUpsUsed}");
        
        PauseForUser();
    }
}