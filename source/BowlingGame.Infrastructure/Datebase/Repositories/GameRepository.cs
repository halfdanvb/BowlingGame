using BowlingGame.Core.DomainModels;
using BowlingGame.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BowlingGame.Infrastructure.Datebase.Repositories;
public class GameRepository : IGameRepository
{
    private BowlingGameContext _context;

    public GameRepository(BowlingGameContext context)
    {
        _context = context;
    }

    public void Add(GameDomain game)
    {
        _context.Games.Add(game.State);
    }

    public async Task<GameDomain> GetByLane(int lane)
    {
        var game = await _context.Games
            .Include(g => g.Rows)
            .ThenInclude(r => r.Frames)
            .Where(g => g.Lane == lane)
            .Where(g => g.IsOngoing)
            .SingleAsync();

        return new GameDomain(game);
    }
}
