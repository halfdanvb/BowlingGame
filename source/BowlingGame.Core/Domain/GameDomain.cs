using BowlingGame.Core.Entities;
using BowlingGame.Core.Extensions;

namespace BowlingGame.Core.DomainModels;
public class GameDomain
{
    public Game State { get; private set; }
    private bool IsLastTurn => State.Turn == 10;

    public GameDomain(Game state)
    {
        State = state;
    }

    public (bool Valid, string ErrorMessage) ValidateGame()
    {
        if (State.IsOngoing == false)
        {
            return (false, "Game is not ongoing");
        }

        if (State.Lane <= 0)
        {
            return (false, "Lane number invalid");
        }

        if (State.Rows.Count == 0)
        {
            return (false, "Game has no players");
        }

        if (State.Rows.Any(r => r.Frames.Count != 10))
        {
            return (false, "Frame count invalid");
        }

        return (true, "");
    }

    public void StartGame()
    {
        State.Turn = 1;
        State.Rows.First().HasTurn = true;
    }

    public void AddScore(int score)
    {
        var rowWithTurn = State.Rows
            .Where(r => r.HasTurn)
            .Single();

        var activeFrame = rowWithTurn.Frames
            .Where(f => f.Order == State.Turn)
            .Single();

        if (activeFrame.PointsFirstThrow.HasValue == false)
        {
            activeFrame.PointsFirstThrow = score;
            CalcuteBonusForPreviousFrames(rowWithTurn, activeFrame);
        }
        else if (activeFrame.PointsSecondThrow.HasValue == false)
        {
            activeFrame.PointsSecondThrow = score;
            CalcuteBonusForPreviousFrames(rowWithTurn, activeFrame);
        }
        else if (IsLastTurn)
        {
            activeFrame.PointsExtraThrow = score;
            CalculateBonusForTenthFrame(activeFrame);
        }

        if (activeFrame.IsStrike() || activeFrame.ThrowsUsed())
        {
            MoveTurn(rowWithTurn, activeFrame);
        }

        CalculateTotalScoreForRow(rowWithTurn);
    }

    private void CalcuteBonusForPreviousFrames(Row rowWithTurn, Frame activeFrame)
    {
        var previousFrame = rowWithTurn.Frames
            .SingleOrDefault(f => f.Order == activeFrame.Order - 1);

        if (previousFrame == null)
        {
            return;
        }

        if (previousFrame.IsSpare())
        {
            previousFrame.PointsBonus = activeFrame.FirstThrowValue();
        }

        if (previousFrame.IsStrike())
        {
            previousFrame.PointsBonus = activeFrame.FirstThrowValue() + activeFrame.SecondThrowValue();

            var framebeforePrevious = rowWithTurn.Frames
                .SingleOrDefault(f => f.Order == activeFrame.Order - 2);

            var isTenthFrameBonusThrow = IsLastTurn && activeFrame.IsStrike() && activeFrame.PointsSecondThrow.HasValue;

            if (framebeforePrevious != null && framebeforePrevious.IsStrike() && isTenthFrameBonusThrow == false)
            {
                framebeforePrevious.PointsBonus += activeFrame.FirstThrowValue();
            }
        }
    }

    private void CalculateBonusForTenthFrame(Frame activeFrame)
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


    private void CalculateTotalScoreForRow(Row rowWithTurn)
    {
        var totalScore = rowWithTurn.Frames.Sum(f => f.FirstThrowValue() + f.SecondThrowValue() + f.BonusValue());

        if (rowWithTurn.Frames.Last().IsStrike())
        {
            totalScore -= rowWithTurn.Frames.Last().SecondThrowValue();
        }

        rowWithTurn.TotalScore = totalScore;
    }

    private void MoveTurn(Row rowWithTurn, Frame activeFrame)
    {
        if (IsLastTurn && (activeFrame.IsSpare() || activeFrame.IsStrike()) && activeFrame.PointsExtraThrow.HasValue == false)
        {
            return;
        }

        var nextRow = State.Rows
            .SingleOrDefault(r => r.Order == rowWithTurn.Order + 1);

        if (nextRow != null)
        {
            rowWithTurn.HasTurn = false;
            nextRow.HasTurn = true;
        }
        else if (IsLastTurn == false)
        {
            State.Turn++;
            rowWithTurn.HasTurn = false;
            State.Rows.First().HasTurn = true;
        }
        else
        {
            State.IsOngoing = false;
        }
    }
}
