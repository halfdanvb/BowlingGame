using BowlingGame.Core.Entities;
using BowlingGame.Core.Extensions;

namespace BowlingGame.Core.Domain.Helpers;
public static class TotalScoreCalculator
{
    public static void CalculateTotalScore(Row row)
    {
        var totalScore = row.Frames.Sum(f => f.FirstThrowValue() + f.SecondThrowValue() + f.BonusValue());

        if (row.Frames.Last().IsStrike())
        {
            totalScore -= row.Frames.Last().SecondThrowValue();
        }

        row.TotalScore = totalScore;
    }
}
