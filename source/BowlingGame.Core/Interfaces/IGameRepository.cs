using BowlingGame.Core.DomainModels;

namespace BowlingGame.Core.Interfaces;
public interface IGameRepository
{
    void Add(GameDomain game);
    Task<GameDomain> GetByLane(int lane);
}
