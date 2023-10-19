namespace Advent2017;

public partial class Day05 : Advent.Day
{
    public override void DoWork()
    {
        int curPos = 0, steps = 0;
        int[] input = InputSplit.Select(i => int.Parse(i)).ToArray();
        do
        {
            steps++;
            int oldVal = input[curPos], newPos = curPos + oldVal;
            if (newPos >= input.Length || newPos < 0) break;
            input[curPos] += Part1 ? 1 : (oldVal >= 3 ? -1 : 1);
            curPos += oldVal;
        } while (true);

        Output = steps.ToString();
    }

}
