using BowlingGame.Core.DomainModels;
using BowlingGame.Core.Factories;
using static System.Formats.Asn1.AsnWriter;

namespace BowlingGame.Test;
public class GameDomainTests
{
    [Test]
    public void AddScore_FirstThrow_RowAndFrameInCorrectState()
    {
        //Arrange 
        var gameDomain = CreateGameDomain();
        var score = 5;

        //Act
        gameDomain.StartGame();
        gameDomain.AddScore(score);

        //Assert
        var firstRow = gameDomain.State.Rows.First();
        var firstFrame = firstRow.Frames.First();

        Assert.That(firstFrame.PointsFirstThrow.HasValue, Is.True);
        Assert.That(firstFrame.PointsFirstThrow.Value, Is.EqualTo(score));
        Assert.That(firstRow.TotalScore, Is.EqualTo(score));
        Assert.That(firstRow.HasTurn, Is.True);
    }

    [Test]
    public void AddScore_SecondThrow_RowAndFrameInCorrectState()
    {
        //Arrange 
        var gameDomain = CreateGameDomain();
        var firstScore = 5;
        var secondScore = 2;

        //Act
        gameDomain.StartGame();
        gameDomain.AddScore(firstScore);
        gameDomain.AddScore(secondScore);

        //Assert
        var firstRow = gameDomain.State.Rows.First();
        var firstFrame = firstRow.Frames.First();

        Assert.That(firstFrame.PointsSecondThrow.HasValue, Is.True);
        Assert.That(firstFrame.PointsSecondThrow.Value, Is.EqualTo(secondScore));
        Assert.That(firstRow.TotalScore, Is.EqualTo(firstScore + secondScore));
        Assert.That(firstRow.HasTurn, Is.False);
    }

    [Test]
    public void AddScore_GetsStrike_RowAndFrameInCorrectState()
    {
        //Arrange
        var gameDomain = CreateGameDomain();
        var score = 10;

        //Act
        gameDomain.StartGame();
        gameDomain.AddScore(score);

        //Assert
        var firstRow = gameDomain.State.Rows.First();
        var firstFrame = firstRow.Frames.First();

        Assert.That(firstFrame.PointsFirstThrow.HasValue, Is.True);
        Assert.That(firstFrame.PointsFirstThrow.Value, Is.EqualTo(score));
        Assert.That(firstFrame.PointsSecondThrow.HasValue, Is.False);
        Assert.That(firstRow.HasTurn, Is.False);
    }

    [Test]
    public void AddScore_TenthGameGetsSpare_RowAndFrameInCorrectState()
    {
        //Arrange
        var gameDomain = SetupTenthGame();
        var firstScore = 5;
        var secondScore = 5;
        var bonusScore = 5;

        //Act
        gameDomain.AddScore(firstScore);
        gameDomain.AddScore(secondScore);
        gameDomain.AddScore(bonusScore);

        //Assert
        var lastRow = gameDomain.State.Rows.Last();
        var lastFrame = lastRow.Frames.Last();

        Assert.That(lastFrame.PointsExtraThrow.HasValue, Is.True);
        Assert.That(lastFrame.PointsExtraThrow.Value, Is.EqualTo(bonusScore));
        Assert.That(gameDomain.State.IsOngoing, Is.False);
    }

    private GameDomain CreateGameDomain()
    {
        var playerNames = new List<string>() { "Hans", "Maria", "Peter" };
        var lane = 1;

        var gameDomain = GameFactory.CreateGame(playerNames, lane);

        return gameDomain;
    }

    private GameDomain SetupTenthGame()
    {
        var playerNames = new List<string>() { "Hans" };
        var lane = 1;

        var gameDomain = GameFactory.CreateGame(playerNames, lane);
        gameDomain.StartGame();

        for (int i = 0; i < 9; i++)
        {
            gameDomain.AddScore(5);
            gameDomain.AddScore(5);
        }

        return gameDomain;
    }
}
