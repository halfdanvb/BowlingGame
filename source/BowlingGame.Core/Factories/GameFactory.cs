using BowlingGame.Core.Domain;
using BowlingGame.Core.Entities;

namespace BowlingGame.Core.Factories;
public static class GameFactory
{
    private const int FrameCount = 10;

    public static GameDomain CreateGame(List<string> playerNames, int lane)
    {
        var rows = CreateRows(playerNames);

        var gameState = new Game
        {
            IsOngoing = true,
            Lane = lane,
            Rows = rows
        };

        var gameDomain = new GameDomain(gameState);
        return gameDomain;
    }

    private static List<Row> CreateRows(List<string> playerNames)
    {
        var rows = playerNames.Select((p, i) => new Row
        {
            PlayerName = p,
            Order = i + 1,
            Frames = CreateFrames()
        }).ToList();

        return rows;
    }

    private static List<Frame> CreateFrames()
    {
        var frames = new List<Frame>(FrameCount);

        for (int i = 0; i < FrameCount; i++)
        {
            frames.Add(new Frame
            {
                Order = i + 1
            });
        }

        return frames;
    }
}
