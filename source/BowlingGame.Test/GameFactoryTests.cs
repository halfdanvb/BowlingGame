using BowlingGame.Core.Factories;

namespace BowlingGame.Test;

public class GameFactoryTests
{
    [Test]
    public void CreateGame_GivenValidInput_CreatesValidGame()
    {
        //Arrange 
        var playerNames = new List<string>() { "Hans", "Maria", "Peter" };
        var lane = 1;

        //Act
        var gameDomain = GameFactory.CreateGame(playerNames, lane);
        var validationResult = gameDomain.ValidateGame();

        // Assert
        Assert.That(validationResult.Valid, Is.True);
        Assert.That(gameDomain.State.Rows.Count, Is.EqualTo(playerNames.Count));
    }
}