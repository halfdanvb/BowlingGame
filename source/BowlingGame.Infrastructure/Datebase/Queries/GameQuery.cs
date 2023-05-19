using BowlingGame.Core.Dto;
using BowlingGame.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BowlingGame.Infrastructure.Datebase.Queries;
public class GameQuery : IGameQuery
{
    private readonly BowlingGameContext _context;
    public GameQuery(BowlingGameContext context)
    {
        _context = context;
    }
    public async Task<GameDto> GetByLaneQuery(int lane)
    {
        var game = await _context.Games
            .Where(g => g.IsOngoing)
            .Where(g => g.Lane == lane)
            .Select(g => new GameDto
            {
                Lane = g.Lane,
                Turn = g.Turn,
                Rows = g.Rows.Select(r => new RowDto
                {
                    PlayerName = r.PlayerName,
                    Order = r.Order,
                    HasTurn = r.HasTurn,
                    TotalScore = r.TotalScore,
                    Frames = r.Frames.Select(f => new FrameDto
                    {
                        Order = f.Order,
                        PointsFirstThrow = f.PointsFirstThrow,
                        PointsSecondThrow = f.PointsSecondThrow,
                        PointsExtraThrow = f.PointsExtraThrow,
                        PointsBonus = f.PointsBonus
                    }).ToList()
                }).ToList()
            })
            .AsNoTracking()
            .SingleAsync();

        return game;
    }
}
