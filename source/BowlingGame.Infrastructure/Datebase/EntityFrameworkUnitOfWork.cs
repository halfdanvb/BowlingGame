using BowlingGame.Core.Interfaces;

namespace BowlingGame.Infrastructure.Datebase;
public class EntityFrameworkUnitOfWork : IUnitOfWork
{
    private readonly BowlingGameContext _context;
    public EntityFrameworkUnitOfWork(BowlingGameContext context)
    {
        _context = context;
    }
    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}
