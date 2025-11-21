public class Player
{
    public string Name { get; set; }
    public int HighestScore { get; set; }
    public int BestTileAchieved { get; set; }
    public int GamesPlayed { get; set; }
    public double AverageScore { get; set; }

    public Player(string name)
    {
        Name = name;
        HighestScore = 0;
        BestTileAchieved = 0;
        GamesPlayed = 0;
        AverageScore = 0.0;
    }

}