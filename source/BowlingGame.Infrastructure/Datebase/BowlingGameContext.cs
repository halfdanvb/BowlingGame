using BowlingGame.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BowlingGame.Infrastructure.Datebase;
public class BowlingGameContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("BownlingGameDb");
    }

    public DbSet<Frame> Frames { get; set; }
    public DbSet<Row> Rows { get; set; }
    public DbSet<Game> Games { get; set; }
}
