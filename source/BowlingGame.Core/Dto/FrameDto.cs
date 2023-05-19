namespace BowlingGame.Core.Dto;
public class FrameDto
{
    public int Order { get; set; }
    public int? PointsFirstThrow { get; set; }
    public int? PointsSecondThrow { get; set; }
    public int? PointsExtraThrow { get; set; }
    public int? PointsBonus { get; set; }

    public override string ToString()
    {
        return $"[{PointsFirstThrow}, {PointsSecondThrow}, bonus: {PointsBonus}]";
    }
}
