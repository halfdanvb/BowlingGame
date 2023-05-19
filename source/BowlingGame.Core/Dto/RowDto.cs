namespace BowlingGame.Core.Dto;
public class RowDto
{
    public string PlayerName { get; set; } = null!;
    public int Order { get; set; }
    public bool HasTurn { get; set; }
    public int TotalScore { get; set; }
    public List<FrameDto> Frames { get; set; } = null!;

    public override string ToString()
    {
        var turnMarking = HasTurn ? "* " : "";
        return $"{turnMarking}{PlayerName} : {string.Join('|', Frames.OrderBy(f => f.Order))} Total score: {TotalScore}";
    }
}
