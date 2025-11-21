public class Tile
{
    public int Level { get; set; }           // 2, 4, 8, 16...
    public required string Name { get; set; }         // "Dough", "Bread", etc.
    public int Value { get; set; }           // points or coin value
    public int MergeCost { get; set; }     // cost to merge two tiles
}
