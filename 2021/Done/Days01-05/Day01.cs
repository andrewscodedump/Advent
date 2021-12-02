namespace Advent2021;

public class Day01 : Advent.Day
{
    public override void DoWork()
    {
        int curr, prev = int.MaxValue, increases = 0;

        for (int i=0; i<InputSplitInt.Length; i++)
        {
            if (WhichPart == 2 && i > InputSplitInt.Length - 3) break;
            curr = WhichPart == 1 ? InputSplitInt[i] : InputSplitInt[i] + InputSplitInt[i + 1] + InputSplitInt[i + 2];
            if (curr > prev) increases++;
            prev = curr;
        }

        Output = increases.ToString();
    }
}
