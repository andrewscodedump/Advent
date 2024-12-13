namespace Advent2024;

public partial class Day13 : Advent.Day
{
    public override void DoWork()
    {
        decimal totalCost = 0;
        for (int i = 0; i < InputNumbers.Length; i += 3)
        {
            (decimal ax, decimal ay) = (InputNumbers[i][0], InputNumbers[i][1]);
            (decimal bx, decimal by) = (InputNumbers[i + 1][0], InputNumbers[i + 1][1]);
            (decimal px, decimal py) = (InputNumbers[i + 2][0], InputNumbers[i + 2][1]);
            if (Part2) { px += 10_000_000_000_000; py += 10_000_000_000_000; }
            decimal bp = ((ax * py) - (ay * px)) / ((ax * by) - (ay * bx));
            decimal ap = (px - (bx * bp)) / ax;
            if (ap % 1 == 0 && bp % 1 == 0) totalCost += (ap * 3) + bp;
        }

        Output = totalCost.ToString();
    }
}
