using BowlingGame.Core.Domain.Helpers;
using BowlingGame.Core.Entities;
using BowlingGame.Core.Extensions;
using BowlingGame.Core.Interfaces;

namespace BowlingGame.Core.Domain.Strategies;
public class RegularTurnStrategy : ITurnStrategy
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
    }

    public void CalculateBonus(Row row, Frame frame)
    {
        BonusCalculator.CalcuteBonusForPreviousFrames(row, frame, false);
    }

    public void CalculateTotalScore(Row row)
    {
        TotalScoreCalculator.CalculateTotalScore(row);
    }

    public void MoveTurn(Game game, Row row, Frame frame)
    {
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
                game.Turn++;
                row.HasTurn = false;
                game.Rows.First().HasTurn = true;
            };
        }
    }
}
