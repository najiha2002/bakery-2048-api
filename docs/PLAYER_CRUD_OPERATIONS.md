# Player CRUD Operations - Bakery 2048

## Overview
Players are the users who play the Bakery 2048 game. The system manages player accounts, game sessions, statistics, and progression through CRUD operations.

---

## CREATE - Register New Player

### Use Case
Register a new player account in the system

### Example
```
=== Player Registration ===
Enter your username (or type 'cancel' to exit): Alice
Enter your email: alice@bakery.com

✓ Welcome to Bakery 2048, Alice!
Player ID: 7f8a3c2d-1e4b-5a9c-8d7e-6f5a4b3c2d1e
Registration Date: 2025-11-27 14:30
Starting Rank: Kitchen Helper

Do you want to record a game session now? (yes/no): 
```

### Features
- **Retry on Error**: If validation fails, user can try again without restarting
- **Cancel Option**: Type 'cancel' to exit registration
- **Duplicate Check**: Prevents registering existing usernames
- **Immediate Session**: Option to record first game session after registration

### Default Values on Registration
- `PlayerId`: Auto-generated GUID
- `Level`: 1
- `HighestScore`: 0
- `GamesPlayed`: 0
- `DateRegistered`: Current timestamp
- `IsActive`: true
- `Rank`: Kitchen Helper (< 1000 points)

### Main Menu Options
```
🍰 Bakery 2048 - Data Management System 🍰
[1] Manage Players
[2] Manage Tiles  
[3] Manage Power-Ups
[4] Generate Random Data
[5] Run Data Analysis (LINQ)
[6] Exit
```

### Player Management Submenu
```
🎮 Player Management
[1] Register New Player
[2] View All Players
[3] Search Player
[4] Update Player
[5] Delete Player
[6] View Player Statistics
[0] Back to Main Menu
```

### When Needed
- New user signs up
- Player creates account to save progress
- Migrating from guest to registered user

---

## READ - View/Search Players

### 1. View All Players
Display list of all registered players

```
=== All Players ===
Username             Level    High Score   Games Played    Status    
------------------------------------------------------------------------
Alice                5        12450        23              Active    
Bob                  12       45230        87              Active    
Charlie              3        5670         15              Inactive  
```

### 2. Search Player by Username
Find specific player by name

```
Enter player name to search: Alice

=== Found 1 Player(s) ===

Player: Alice
ID: 7f8a3c2d-1e4b-5a9c-8d7e-6f5a4b3c2d1e
Email: alice@bakery.com
Level: 5
Highest Score: 12450
Average Score: 8320.50
Games Played: 23
Best Tile: 2048
Win Streak: 5
Total Moves: 2340
Power-Ups Used: 45
Total Play Time: 12.50 hours
Date Registered: 2025-11-15
Last Played: 2025-11-27
Status: Active
Rank: Apprentice Baker
```

### 3. View Player Statistics
Overall statistics across all players

```
=== Player Statistics ===
Total Players: 150
Active Players: 132
Inactive Players: 18
Average Score: 8543.25
Highest Score: 95430
Total Games Played: 3,450

🏆 Top Player: BobTheBaker
   Score: 95430
   Rank: Master Baker

=== Top 5 Leaderboard ===
1. BobTheBaker - Level 25 - Score: 95430 - Master Baker
2. Alice - Level 5 - Score: 12450 - Apprentice Baker
3. Charlie - Level 3 - Score: 5670 - Baker
4. Diana - Level 8 - Score: 18920 - Pastry Chef
5. Eve - Level 15 - Score: 34560 - Head Baker
```

---

## UPDATE - Modify Player Information

### Use Cases

#### 1. Update Email
Change contact information
```
Enter new email: alice.baker@bakery.com
✓ Email updated.
```

#### 2. Update Score (Manual)
Manually adjust player score
```
Enter new score: 15000
✓ Score updated. New high score: 15000
```

#### 3. Update Level (Admin)
Adjust player level manually
```
Enter new level: 6
✓ Level updated.
```

#### 4. Toggle Active Status
Activate or deactivate player account
```
✓ Player deactivated.
```

#### 5. Add Play Time
Track additional gameplay time
```
Enter hours played: 2.5
✓ Play time added. Total: 15.00 hours
```

### Update Menu Example
```
Updating player: Alice
1. Update Email
2. Update Score
3. Update Level
4. Toggle Active Status
5. Add Play Time
6. Cancel
Select option: 
```

---

## DELETE - Remove Player

### Use Case
Remove player account from system

### Example
```
Enter player name to delete: TestPlayer

Are you sure you want to delete player 'TestPlayer'? (yes/no): yes
✓ Player 'TestPlayer' deleted successfully.
```

### When Needed
- User requests account deletion
- Removing test/dummy accounts
- GDPR compliance (right to be forgotten)
- Cleaning up inactive accounts

---

## Special Operations

### 1. Record Game Session

#### Purpose
Track a complete game session with all statistics

#### Input Required
```
=== Recording Game Session for Alice ===

Enter final score: 3450
Enter best tile achieved: 2048
Enter number of moves made: 156
Enter play duration in minutes: 12.5
```

#### What Gets Updated
- `CurrentScore`: Set to final score
- `HighestScore`: Updated if new high score
- `BestTileAchieved`: Updated if higher tile reached
- `GamesPlayed`: Incremented by 1
- `LastPlayed`: Set to current timestamp
- `TotalMoves`: Added to total
- `TotalPlayTime`: Added to total
- `AverageScore`: Recalculated
- `WinStreak`: Incremented or reset based on win
- `Level`: Auto-calculated from score

#### Output
```
✓ Game session recorded successfully!
High Score: 12450
Best Tile Ever: 2048
Games Played: 24
Average Score: 8456.25
Current Level: 6
Rank: Apprentice Baker
Win Streak: 6
```

---

## DATA GENERATION - Generate Random Test Data

### Purpose
Automatically generate realistic test data including players, game sessions, and power-up usage statistics.

### Menu Option
`[4] Generate Random Data`

### Features
- Generates 50-75 random players with varied stats
- Simulates 1-3 game sessions per active player
- Updates power-up usage counts realistically
- Execution time: ~0.03 seconds

### Example Output
```
🎲 Generate Random Data

This will generate 50-100 random players with game sessions.
Continue? (y/n): y

ℹ Generating random data... Please wait.

Generating 56 players... ✓
Simulating game sessions for 31 players... 
🔍 Debug: powerUpsList.Count = 5
   First power-up: Double Score

🔍 Before save - PowerUps in list: 5
🔍 After player save - PowerUps in list: 5
✓

✓ Data generation complete!

Players Generated: 56
Game Sessions Simulated: 65
Time Taken: 0.03 seconds
```

### Generated Player Data
- **Usernames**: Combination of names and suffixes (e.g., BakerMaster123, ChefKing42)
- **Email**: Auto-generated (e.g., bakermaster123@bakery.com)
- **Scores**: 100-50,000 points
- **Games Played**: 1-150 games
- **Activity**: 70% have played at least once
- **Play Time**: Realistic durations
- **Last Played**: Within last 60 days

### Session Simulation
For each active player:
- Final score: 500-20,000 points
- Best tile: Random tile values (2-4096)
- Moves: 50-500 moves per session
- Duration: 5-45 minutes
- Win rate: 20% reach win condition
- Power-up usage: 0-5 power-ups per session

### Implementation Details
```csharp
// Uses reflection to access protected service lists
var playersField = typeof(BaseService<Player>).GetField("items",
    BindingFlags.NonPublic | BindingFlags.Instance);
var playersList = (List<Player>)playersField!.GetValue(playerService)!;

// Updates player stats directly
player.CurrentScore = finalScore;
player.HighestScore = Math.Max(player.HighestScore, finalScore);
player.GamesPlayed++;
player.TotalMoves += moves;
player.TotalPlayTime += duration;

// Tracks power-up usage
foreach (var powerUp in selectedPowerUps)
{
    powerUp.UsageCount++;
}
```

---

## DATA ANALYSIS (LINQ) - Advanced Analytics

### Purpose
Analyze player behavior, power-up effectiveness, and game statistics using comprehensive LINQ queries.

### Menu Option
`[5] Run Data Analysis (LINQ)`

### Analysis Categories

#### 1. Player Analytics
```
📊 Player Analytics

Score Distribution:
0-1K           12        
1K-5K          23        
5K-10K         15        
10K-20K        8         
20K+           4         

Activity Levels:
Casual (< 10 games):      18 players
Regular (10-50 games):    25 players
Active (50-100 games):    10 players
Hardcore (100+ games):    3 players

Engagement Metrics:
Average Games Played:     32.5
Average Play Time:        8.3 hours
Most Active Player:       BakerKing42 (127 games)

Efficiency Rankings (Score per Move):
1. ChefMaster89         142.5 points/move
2. PastryPro123         138.2 points/move
3. SweetLegend          135.7 points/move
```

**LINQ Queries Used:**
```csharp
// Score distribution
var count = players.Count(p => 
    p.HighestScore >= min && p.HighestScore < max);

// Activity levels  
var casual = players.Count(p => p.GamesPlayed < 10);
var regular = players.Count(p => p.GamesPlayed >= 10 && p.GamesPlayed < 50);

// Efficiency rankings
var efficient = players
    .OrderByDescending(p => p.HighestScore / (double)p.TotalMoves)
    .Take(10);
```

#### 2. Power-Up Analytics
```
⚡ Power-Up Analytics

Usage Statistics:
Total Usage:              362 times
Average per Power-Up:     72.4 uses

Top 5 Most Popular:
1. Undo Move              101 uses (27.9%)
2. Tile Swap              95 uses (26.2%)
3. Time Freeze            88 uses (24.3%)
4. Double Score           78 uses (21.5%)
5. Triple Score           0 uses (0.0%) [LOCKED]

Cost Analysis:
Budget (< 200):           2 power-ups, Avg usage: 89.5
Mid-tier (200-300):       2 power-ups, Avg usage: 83.0
Premium (300+):           1 power-ups, Avg usage: 88.0

By Type:
ScoreBoost:               2 power-ups, Total: 78 uses
TimeExtension:            1 power-ups, Total: 88 uses
Undo:                     1 power-ups, Total: 101 uses
SwapTiles:                1 power-ups, Total: 95 uses
```

**LINQ Queries Used:**
```csharp
// Top power-ups
var topPowerUps = powerUps
    .OrderByDescending(p => p.UsageCount)
    .Take(5);

// Group by type
var byType = powerUps
    .GroupBy(p => p.PowerUpType)
    .Select(g => new {
        Type = g.Key,
        Count = g.Count(),
        TotalUsage = g.Sum(p => p.UsageCount)
    });

// Cost analysis
var midTier = powerUps.Where(p => p.Cost >= 200 && p.Cost < 300);
var avgUsage = midTier.Any() ? midTier.Average(p => p.UsageCount) : 0;
```

#### 3. Cross-Entity Analysis
```
🔗 Cross-Entity Analysis

Power-Up Impact on Scores:
Players using power-ups:    48 players
Average score (with):       12,450
Average score (without):    5,230
Score improvement:          138.2%

Win Rate Analysis:
With power-ups:             25.0% (12/48 players)
Without power-ups:          12.5% (1/8 players)
Win rate improvement:       100.0%

Elite Player Behavior (Top 10%):
Elite players:              6 players
Average power-ups used:     42.3
Average games played:       89.5
Preferred power-up:         Undo Move
```

**LINQ Queries Used:**
```csharp
// Power-up impact
var withPowerUps = players.Where(p => p.PowerUpsUsed > 0);
var without = players.Where(p => p.PowerUpsUsed == 0);
var improvement = (avgWith - avgWithout) / avgWithout * 100;

// Elite players (top 10%)
var top10Count = Math.Max(1, (int)(players.Count * 0.1));
var elite = players
    .OrderByDescending(p => p.HighestScore)
    .Take(top10Count);
```

#### 4. Advanced Queries
```
🔬 Advanced Queries

Statistical Analysis:
Mean Score:               8,543.2
Standard Deviation:       12,345.6
Variance:                 152,413,853.4

Percentile Analysis:
25th percentile:          2,450 points
50th percentile (Median): 6,780 points
75th percentile:          14,230 points
90th percentile:          28,900 points

Correlation Insights:
Power-up users score 2.4x higher on average
Elite players use 3.2x more power-ups
Players with 50+ games have 89% higher scores

Uncommon Patterns:
Perfect players (no power-ups, high score): 2 found
Unused power-ups:                           1 found (Triple Score)
Inactive power-up users:                    3 players
```

**LINQ Queries Used:**
```csharp
// Standard deviation
var avgScore = players.Average(p => p.HighestScore);
var variance = players.Average(p => Math.Pow(p.HighestScore - avgScore, 2));
var stdDev = Math.Sqrt(variance);

// Percentiles
var sortedScores = players
    .Select(p => p.HighestScore)
    .OrderBy(s => s)
    .ToList();
var percentile25 = sortedScores[(int)(sortedScores.Count * 0.25)];

// Uncommon patterns
var perfectPlayers = players
    .Where(p => p.PowerUpsUsed == 0 && p.HighestScore > 10000);
```

### Benefits of LINQ Analytics
1. **Data-Driven Decisions**: Understand player behavior patterns
2. **Game Balance**: Identify overpowered/underpowered features
3. **Player Retention**: Track engagement and activity metrics
4. **Monetization**: Analyze power-up popularity for pricing
5. **Performance Metrics**: Efficiency and skill analysis

---

## Player Statistics & Analytics

### Data Analysis with LINQ

#### Top 10 Players (Leaderboard)
```csharp
var leaderboard = players.OrderByDescending(p => p.HighestScore)
                         .Take(10);
```

#### Average Score Across All Players
```csharp
var avgScore = players.Average(p => p.HighestScore);
```

#### Total Active Players
```csharp
var activeCount = players.Count(p => p.IsActive);
```

#### Players Registered in Last 30 Days
```csharp
var recentPlayers = players.Where(p => 
    (DateTime.Now - p.DateRegistered).Days <= 30);
```

#### Players Grouped by Level
```csharp
var byLevel = players.GroupBy(p => p.Level)
                     .Select(g => new { 
                         Level = g.Key, 
                         Count = g.Count() 
                     });
```

#### Players with Most Games Played
```csharp
var mostActive = players.OrderByDescending(p => p.GamesPlayed)
                        .Take(10);
```

#### Players by Rank Category
```csharp
var byRank = players.GroupBy(p => p.GetRankCategory())
                    .Select(g => new { 
                        Rank = g.Key, 
                        Count = g.Count() 
                    });
```

#### Inactive Players (Not Played in 30 Days)
```csharp
var inactive = players.Where(p => p.IsInactive());
```

#### Players with Win Streaks
```csharp
var streakers = players.Where(p => p.WinStreak > 0)
                       .OrderByDescending(p => p.WinStreak);
```

#### Average Play Time per Player
```csharp
var avgPlayTime = players.Average(p => p.TotalPlayTime.TotalHours);
```

#### Players by Efficiency (Score per Move)
```csharp
var efficient = players.OrderByDescending(p => p.GetEfficiency())
                       .Take(10);
```

---

## Player Rank System

Players are automatically ranked based on their highest score:

| Rank | Score Range | Description |
|------|-------------|-------------|
| **Kitchen Helper** | 0 - 999 | Beginner player |
| **Apprentice Baker** | 1,000 - 4,999 | Learning the basics |
| **Baker** | 5,000 - 9,999 | Competent player |
| **Pastry Chef** | 10,000 - 19,999 | Skilled player |
| **Head Baker** | 20,000 - 49,999 | Expert player |
| **Master Baker** | 50,000+ | Elite player |

### Auto Level-Up
```csharp
public void CalculateLevelFromScore()
{
    Level = (HighestScore / 1000) + 1;
}
```

---

## Player Methods

### Core Methods

#### UpdateScore
```csharp
public void UpdateScore(int newScore)
```
- Updates current score
- Updates highest score if beaten
- Recalculates average score

#### IncrementGamesPlayed
```csharp
public void IncrementGamesPlayed()
```
- Increments game counter
- Updates last played timestamp
- Recalculates average score

#### RecordGameSession
```csharp
public void RecordGameSession(int finalScore, int bestTileAchieved, 
    int movesMade, TimeSpan playDuration, bool reachedWinCondition)
```
- Comprehensive game session recording
- Updates all relevant statistics
- Handles win streak logic
- Auto-levels up player

### Streak Methods

#### IncrementWinStreak / ResetWinStreak
```csharp
public void IncrementWinStreak()
public void ResetWinStreak()
```
- Track consecutive wins
- Reset on loss

### Account Management

#### Deactivate / Activate
```csharp
public void Deactivate()
public void Activate()
```
- Manage account status

### Statistics Methods

#### GetDaysSinceRegistration
```csharp
public int GetDaysSinceRegistration()
```
- Calculate account age

#### GetDaysSinceLastPlayed
```csharp
public int GetDaysSinceLastPlayed()
```
- Check player activity

#### IsInactive
```csharp
public bool IsInactive()
```
- Returns true if no activity in 30+ days

#### GetEfficiency
```csharp
public double GetEfficiency()
```
- Calculate score per move ratio

#### GetRankCategory
```csharp
public string GetRankCategory()
```
- Get player's rank title

#### GetLeaderboardEntry
```csharp
public string GetLeaderboardEntry()
```
- Format for leaderboard display

---

## Practical Workflow Examples

### New Player Journey
1. **CREATE** player account (registration)
2. Player starts playing
3. **UPDATE** via RecordGameSession after each game
4. **READ** player stats to view progress
5. Automatic level-up and rank promotion

### Admin Moderation
1. **READ** all players
2. **SEARCH** for specific player by username
3. **UPDATE** player info (correct errors)
4. **DELETE** if duplicate/test account

### Analytics & Reporting
1. **READ** all players
2. **ANALYZE** with LINQ queries
3. Generate reports (top players, engagement metrics)
4. Make game balance decisions

### Player Support
1. **SEARCH** player by username
2. **READ** full player statistics
3. **UPDATE** to resolve issues (restore score, etc.)
4. **DEACTIVATE** temporarily if needed

---

## Data Persistence

### File Storage
- **File**: `players.json`
- **Format**: JSON array of Player objects
- **Auto-save**: After every modification (Register, Update, Delete, RecordGameSession)
- **Auto-load**: On service initialization

### Example JSON Structure
```json
[
  {
    "PlayerId": "7f8a3c2d-1e4b-5a9c-8d7e-6f5a4b3c2d1e",
    "Username": "Alice",
    "Email": "alice@bakery.com",
    "HighestScore": 12450,
    "CurrentScore": 3450,
    "BestTileAchieved": 2048,
    "Level": 5,
    "GamesPlayed": 23,
    "AverageScore": 8320.5,
    "DateRegistered": "2025-11-15T10:30:00",
    "LastPlayed": "2025-11-27T14:45:00",
    "IsActive": true,
    "TotalPlayTime": "12:30:00",
    "WinStreak": 5,
    "TotalMoves": 2340,
    "PowerUpsUsed": 45,
    "FavoriteItem": "Rainbow Cake"
  }
]
```

---

## Benefits of Player CRUD System

1. **Complete Player Management**
   - Full lifecycle from registration to deletion
   - Comprehensive statistics tracking

2. **Game Progression**
   - Automatic level-up system
   - Rank-based progression
   - Win streak tracking

3. **Player Engagement**
   - Leaderboards for competition
   - Statistics to show improvement
   - Achievement tracking (ranks)

4. **Analytics & Insights**
   - Player behavior analysis
   - Retention metrics (inactive players)
   - Performance analytics (efficiency)

5. **Data Integrity**
   - Persistent JSON storage
   - Automatic backups via file system
   - Easy data migration/export

6. **Scalability**
   - Supports multiple players
   - Efficient LINQ queries
   - JSON format for easy integration

---

## Integration with Other Systems

### With Tiles
- Track favorite bakery items
- Link tile progression to player level

### With PowerUps
- Track `PowerUpsUsed` count
- Correlate power-up usage with scores
- Unlock power-ups based on level

### Cross-System Analytics
```csharp
// Players who reached 2048 tile
var winners = players.Where(p => p.BestTileAchieved >= 2048);

// Players who never used power-ups
var purePlayers = players.Where(p => p.PowerUpsUsed == 0);

// High efficiency players (masters of the game)
var efficient = players.Where(p => p.GetEfficiency() > 100);
```

---

## Future Enhancements

### Potential Features
- Friend system (social connections)
- Achievements/badges system
- Daily challenges tracking
- Season pass progression
- Tournament participation
- Player profiles (avatar, bio)
- Privacy settings
- Email notifications
- Password authentication
- Multi-device sync

### Advanced Analytics
- Player retention cohorts
- Churn prediction
- Lifetime value (LTV) calculation
- A/B testing groups
- Engagement scoring
