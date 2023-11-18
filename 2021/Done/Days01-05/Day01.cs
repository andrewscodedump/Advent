namespace Advent2021;

public class Day01 : Advent.Day
{
    public override void DoWork()
    {
        long curr, prev = long.MaxValue, increases = 0;

        for (int i=0; i<InputNumbers.Count; i++)
        {
            if (Part2 && i > InputNumbers.Count - 3) break;
            curr = Part1 ? InputNumbers[i][0] : InputNumbers[i][0] + InputNumbers[i + 1][0] + InputNumbers[i + 2][0];
            if (curr > prev) increases++;
            prev = curr;
        }

        Output = increases.ToString();
    }
}
