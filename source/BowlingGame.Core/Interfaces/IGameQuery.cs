using BowlingGame.Core.Dto;

namespace BowlingGame.Core.Interfaces;
public interface IGameQuery
{
    Task<GameDto> GetByLaneQuery(int lane);
}
