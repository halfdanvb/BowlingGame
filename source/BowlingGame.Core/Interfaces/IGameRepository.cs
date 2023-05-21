using BowlingGame.Core.Domain;

namespace BowlingGame.Core.Interfaces;
public interface IGameRepository
{
    void Add(GameDomain game);
    Task<GameDomain> GetByLane(int lane);
}
