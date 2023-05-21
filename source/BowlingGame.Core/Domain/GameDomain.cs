using BowlingGame.Core.Domain.Strategies;
using BowlingGame.Core.Entities;
using BowlingGame.Core.Extensions;
using BowlingGame.Core.Interfaces;

namespace BowlingGame.Core.Domain;
public class GameDomain
{
    public Game GameState { get; private set; }
    private bool _isLastTurn => GameState.Turn == 10;
    private ITurnStrategy _turnStrategy;

    public GameDomain(Game game)
    {
        GameState = game;
    }

    public (bool Valid, string ErrorMessage) ValidateGame()
    {
        if (GameState.Lane <= 0)
        {
            return (false, "Lane number invalid");
        }

        if (GameState.Rows.Count == 0)
        {
            return (false, "Game has no players");
        }

        if (GameState.Rows.Any(r => r.Frames.Count != 10))
        {
            return (false, "Frame count invalid");
        }

        if (GameState.Rows.SelectMany(r => r.Frames).Any(f => f.FirstThrowValue() + f.SecondThrowValue() > 10) && _isLastTurn == false)
        {
            return (false, "Frame score too high");
        }

        if (GameState.Rows.SelectMany(r => r.Frames).Any(f => f.FirstThrowValue() + f.SecondThrowValue() < 0))
        {
            return (false, "Frame score too low");
        }

        return (true, "");
    }

    public void StartGame()
    {
        GameState.Turn = 1;
        GameState.Rows.First().HasTurn = true;
    }

    public void ExecuteTurn(int score)
    {
        var rowWithTurn = GameState.Rows
            .Where(r => r.HasTurn)
            .Single();

        var activeFrame = rowWithTurn.Frames
            .Where(f => f.Order == GameState.Turn)
            .Single();

        if (_isLastTurn)
        {
            _turnStrategy = new LastTurnStrategy();
        }
        else
        {
            _turnStrategy = new RegularTurnStrategy();
        }

        _turnStrategy.AddScore(score, rowWithTurn, activeFrame);
        _turnStrategy.CalculateBonus(rowWithTurn, activeFrame);
        _turnStrategy.MoveTurn(GameState, rowWithTurn, activeFrame);
        _turnStrategy.CalculateTotalScore(rowWithTurn);
    }
}
