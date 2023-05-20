using System.Text;

namespace BowlingGame.Core.Dto;

public class GameDto
{
    public int Lane { get; set; }
    public int Turn { get; set; }
    public bool IsOngoing { get; set; }
    public List<RowDto> Rows { get; set; } = null!;

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine($"Lane: {Lane}, Turn: {Turn}");

        Rows.ForEach(r => stringBuilder.AppendLine(r.ToString()));

        return stringBuilder.ToString();
    }
}
