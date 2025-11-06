namespace Everybody2025;

public class Day03 : Advent.Day
{
    public override void DoWork()
    {
        Output = WhichPart switch
        {
            1 => InputNumbers[0].Distinct().Sum().ToString(),
            2 => InputNumbers[0].Distinct().Order().Take(20).Sum().ToString(),
            _ => InputNumbers[0].GroupBy(n => n).Max(g => g.Count()).ToString(),
        };
    }
}
