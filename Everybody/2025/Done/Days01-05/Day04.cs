namespace Everybody2025;

public class Day04 : Advent.Day
{
    public override void DoWork()
    {
        double basicRatio = (double)InputNumbers[0][0] / InputNumbers[^1][0];
        Output = WhichPart switch
        {
            1 => Math.Floor(basicRatio * 2025).ToString(),
            2 => Math.Ceiling(10_000_000_000_000.0 / basicRatio).ToString(),
            _ => Math.Floor(basicRatio * InputNumbers[1..^1].Select(i => i[1] / i[0]).Aggregate(1.0, (a, b) => a * b) * 100).ToString(),
        };
    }
}
