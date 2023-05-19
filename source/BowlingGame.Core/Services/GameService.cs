using BowlingGame.Core.Dto;
using BowlingGame.Core.Factories;
using BowlingGame.Core.Interfaces;

namespace BowlingGame.Core.Services;
public class GameService
{
    private readonly IGameRepository _gameRepository;
    private readonly IGameQuery _gameQuery;
    private readonly IUnitOfWork _unitOfWork;

    public GameService(IGameRepository gameRepository, IGameQuery gameQuery, IUnitOfWork unitOfWork)
    {
        _gameRepository = gameRepository;
        _gameQuery = gameQuery;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateAndStart(List<string> playerNames, int lane)
    {
        var gameDomain = GameFactory.CreateGame(playerNames, lane);

        var validationResult = gameDomain.ValidateGame();

        if (validationResult.Valid == false)
        {
            throw new Exception(validationResult.ErrorMessage);
        }

        gameDomain.StartGame();

        _gameRepository.Add(gameDomain);

        await _unitOfWork.SaveChanges();
    }

    public async Task AddScore(int lane, int score)
    {
        if (score < 0 || score > 10)
        {
            throw new ArgumentOutOfRangeException(nameof(score));
        }

        var gameDomain = await _gameRepository.GetByLane(lane);

        if (gameDomain.State.IsOngoing == false)
        {
            return;
        }

        gameDomain.AddScore(score);

        await _unitOfWork.SaveChanges();
    }

    public async Task<GameDto> GetScoreBoard(int lane)
    {
        var gameDto = await _gameQuery.GetByLaneQuery(lane);
        return gameDto;
    }
}
