using BowlingGame.Core.Entities;

namespace BowlingGame.Core.Interfaces;
internal interface ITurnStrategy
{
    void AddScore(int score, Row row, Frame frame);
    void CalculateBonus(Row row, Frame frame);
    void CalculateTotalScore(Row row);
    void MoveTurn(Game game, Row row, Frame frame);
}
