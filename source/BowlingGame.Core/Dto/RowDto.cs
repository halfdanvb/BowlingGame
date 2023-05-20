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
        var perfektMarking  = "";
        var gutterMarking = "";

        if (Frames.Last().PointsSecondThrow.HasValue)
        {
            perfektMarking = TotalScore == 300 ? " - Perfekt Game" : "";
            gutterMarking = TotalScore == 0 ? " - Gutter Game" : "";
        }

        return $"{turnMarking}{PlayerName} : {string.Join('|', Frames.OrderBy(f => f.Order))} Total score: {TotalScore}{perfektMarking}{gutterMarking}";
    }
}
