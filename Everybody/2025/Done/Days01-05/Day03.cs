namespace Everybody2025;

public class Day03 : Advent.Day
{
    public override void DoWork()
    {
        switch (WhichPart)
        {
            case 1:
                Output = InputNumbers[0].Distinct().Sum().ToString();
                break;
            case 2:
                Output = InputNumbers[0].Distinct().Order().Take(20).Sum().ToString();
                break;
            case 3:
                Output = InputNumbers[0].GroupBy(n => n).Max(g => g.Count()).ToString();
                break;
        }
    }
}
