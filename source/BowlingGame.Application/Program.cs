using BowlingGame.Core.Services;
using BowlingGame.Infrastructure.Datebase;
using BowlingGame.Infrastructure.Datebase.Queries;
using BowlingGame.Infrastructure.Datebase.Repositories;

namespace BowlingGame.Application;

internal class Program
{
    static async Task Main(string[] args)
    {
        var databaseContext = new BowlingGameContext();
        var gameRepository = new GameRepository(databaseContext);
        var gameQuery = new GameQuery(databaseContext);
        var unitOfWOrk = new EntityFrameworkUnitOfWork(databaseContext);

        var gameService = new GameService(gameRepository, gameQuery, unitOfWOrk);

        var players = new List<string>() { "Hans" };
        var lane = 1;

        await gameService.CreateAndStart(players, lane);

        var gameIsOngoing = true;

        while (gameIsOngoing == true)
        {
            Console.Clear();

            var scoreBoard = await gameService.GetScoreBoard(lane);
            Console.WriteLine($"{scoreBoard.ToString()}");

            Console.WriteLine("Enter  score:");
            var score = Console.ReadLine();

            await gameService.AddScore(lane, int.Parse(score));

            Console.Clear();

            var updatedScoreBoard = await gameService.GetScoreBoard(lane);
            Console.WriteLine($"{updatedScoreBoard.ToString()}");

            gameIsOngoing = updatedScoreBoard.IsOngoing;
        }

        Console.WriteLine("Game over. Press any key");
        Console.ReadLine();
    }
}
