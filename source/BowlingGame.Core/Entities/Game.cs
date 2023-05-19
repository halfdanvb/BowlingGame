namespace BowlingGame.Core.Entities;
public class Game
{
    public int Id { get; set; }
    public int Lane { get; set; }
    public int Turn { get; set; }
    public bool IsOngoing { get; set; }
    public virtual ICollection<Row> Rows { get; set; } = null!;
}
