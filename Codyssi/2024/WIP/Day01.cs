namespace Codyssi2024;

public class Day01 : Advent.Day
{
    public override void DoWork()
    {
        int freeItems = TestMode ? 2 : 20;
        long result = WhichPart switch
        {
            1 => Inputs.Select(long.Parse).Sum(),
            2 => Inputs.Select(long.Parse).OrderDescending().Skip(freeItems).Sum(),
            _ => Inputs.Select((x, i) => long.Parse(x) * ((i % 2 * -2) + 1)).Sum()
        };

        Output = result.ToString();
    }
}
