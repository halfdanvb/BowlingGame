namespace BowlingGame.Core.Entities;
public class Row
{
    public int Id { get; set; }
    public string PlayerName { get; set; } = null!;
    public int Order { get; set; }
    public int GameId { get; set; }
    public bool HasTurn { get; set; }
    public int TotalScore { get; set; }
    public virtual Game Game { get; set; } = null!;
    public ICollection<Frame> Frames { get; set; } = null!;
}
