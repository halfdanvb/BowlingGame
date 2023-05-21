using BowlingGame.Core.Entities;
using BowlingGame.Core.Extensions;

namespace BowlingGame.Core.Domain.Helpers;
public static class BonusCalculator
{
    public static void CalcuteBonusForPreviousFrames(Row row, Frame frame, bool isLastTurn)
    {
        var previousFrame = row.Frames
            .SingleOrDefault(f => f.Order == frame.Order - 1);

        if (previousFrame == null)
        {
            return;
        }

        if (previousFrame.IsSpare())
        {
            previousFrame.PointsBonus = frame.FirstThrowValue();
        }

        if (previousFrame.IsStrike())
        {
            previousFrame.PointsBonus = frame.FirstThrowValue() + frame.SecondThrowValue();

            var framebeforePrevious = row.Frames
                .SingleOrDefault(f => f.Order == frame.Order - 2);

            if (framebeforePrevious == null)
            {
                return;
            }

            var isFirstThrow = frame.PointsSecondThrow.HasValue == false;
            var isTenthFrameBonusThrow = isLastTurn && frame.IsStrike() && frame.PointsSecondThrow.HasValue;

            if (framebeforePrevious.IsStrike() && isFirstThrow && isTenthFrameBonusThrow == false)
            {
                framebeforePrevious.PointsBonus += frame.FirstThrowValue();
            }
        }
    }

    public static void CalculateBonusForTenthFrame(Frame activeFrame)
    {
        if (activeFrame.IsSpare())
        {
            activeFrame.PointsBonus = activeFrame.BonusThrowValue();
        }

        if (activeFrame.IsStrike())
        {
            activeFrame.PointsBonus = activeFrame.SecondThrowValue() + activeFrame.BonusThrowValue();
        }
    }
}
