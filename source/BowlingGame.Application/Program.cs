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

        var players = new List<string>() { "Hans", "Greate" };
        var lane = 1;

        await gameService.CreateAndStart(players, lane);

        for (int i = 0; i < 24; i++)
        {
            await gameService.AddScore(lane, 10);
            var scoreBoard = await gameService.GetScoreBoard(lane);

            Console.Clear();
            Console.WriteLine($"{scoreBoard.ToString()}");
            Thread.Sleep(100);
        }

        Console.WriteLine("Game over. Press any key");
        Console.ReadLine();
    }
}
