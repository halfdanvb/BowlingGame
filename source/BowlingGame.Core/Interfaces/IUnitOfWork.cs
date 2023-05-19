namespace BowlingGame.Core.Interfaces;
public interface IUnitOfWork
{
    Task SaveChanges();
}
