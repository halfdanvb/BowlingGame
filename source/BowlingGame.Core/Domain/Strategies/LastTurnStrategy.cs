using BowlingGame.Core.Domain.Helpers;
using BowlingGame.Core.Entities;
using BowlingGame.Core.Extensions;
using BowlingGame.Core.Interfaces;

namespace BowlingGame.Core.Domain.Strategies;
public class LastTurnStrategy : ITurnStrategy
{
    public void AddScore(int score, Row row, Frame frame)
    {
        if (frame.PointsFirstThrow.HasValue == false)
        {
            frame.PointsFirstThrow = score;
        }
        else if (frame.PointsSecondThrow.HasValue == false)
        {
            frame.PointsSecondThrow = score;
        }
        else if (frame.PointsExtraThrow.HasValue == false)
        {
            frame.PointsExtraThrow = score;
        }
    }

    public void CalculateBonus(Row row, Frame frame)
    {
        BonusCalculator.CalcuteBonusForPreviousFrames(row, frame, true);
        BonusCalculator.CalculateBonusForTenthFrame(frame);
    }

    public void CalculateTotalScore(Row row)
    {
        TotalScoreCalculator.CalculateTotalScore(row);
    }

    public void MoveTurn(Game game, Row row, Frame frame)
    {
        if ((frame.IsSpare() || frame.IsStrike()) && frame.PointsExtraThrow.HasValue == false)
        {
            return;
        }

        if (frame.IsStrike() || frame.ThrowsUsed())
        {
            var nextRow = game.Rows
                .SingleOrDefault(r => r.Order == row.Order + 1);

            if (nextRow != null)
            {
                row.HasTurn = false;
                nextRow.HasTurn = true;
            }
            else
            {
                game.IsOngoing = false;
            }
        }
    }
}
