namespace BowlingGame.Core.Entities;
public class Frame
{
    public int Id { get; set; }
    public int Order { get; set; }
    public int? PointsFirstThrow { get; set; }
    public int? PointsSecondThrow { get; set; }
    public int? PointsExtraThrow { get; set; }
    public int? PointsBonus { get; set; }
    public int RowId { get; set; }
    public virtual Row Row { get; set; } = null!;
}
