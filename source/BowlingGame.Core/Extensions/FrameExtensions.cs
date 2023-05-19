using BowlingGame.Core.Entities;

namespace BowlingGame.Core.Extensions;
public static class FrameExtensions
{
    public static bool IsSpare(this Frame frame)
    {
        var frameScore = frame.PointsFirstThrow + frame.PointsSecondThrow;

        return frame.PointsFirstThrow != 10 && frameScore == 10;
    }

    public static bool IsStrike(this Frame frame)
    {
        return frame.PointsFirstThrow == 10;
    }

    public static bool ThrowsUsed(this Frame frame)
    {
        return frame.PointsFirstThrow.HasValue && frame.PointsSecondThrow.HasValue;
    }

    public static int FirstThrowValue(this Frame frame)
    {
        var firstThrowValue = frame.PointsFirstThrow ?? 0;
        return firstThrowValue;
    }

    public static int SecondThrowValue(this Frame frame)
    {
        var secondThrowValue = frame.PointsSecondThrow ?? 0;
        return secondThrowValue;
    }

    public static int BonusThrowValue(this Frame frame)
    {
        var bonusThrowValue = frame.PointsExtraThrow ?? 0;
        return bonusThrowValue;
    }

    public static int BonusValue(this Frame frame)
    {
        var bonusValue = frame.PointsBonus ?? 0;
        return bonusValue;
    }
}
