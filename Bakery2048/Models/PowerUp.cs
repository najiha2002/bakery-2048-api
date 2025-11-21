public class PowerUp
{
    public required string Name { get; set; }
    public int Cooldown { get; set; }        // turns before can be reused
    public int UsesRemaining { get; set; }
}
