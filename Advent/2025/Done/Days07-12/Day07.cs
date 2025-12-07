namespace Advent2025;

public class Day07 : Advent.Day
{
    public override void DoWork()
    {
        int splits = 0, width = Inputs[0].Length;
        long[] currCounts = new long[width], prevCounts;
        foreach (string line in Inputs)
        {
            prevCounts = (long[])currCounts.Clone();
            currCounts = new long[width];
            for (int c = 0; c < line.Length; c++)
                if (line[c] == 'S')
                    currCounts[c] = 1;
                else if (prevCounts[c] != 0)
                    if (line[c] == '.')
                        currCounts[c] += prevCounts[c];
                    else
                    {
                        splits++;
                        currCounts[c - 1] += prevCounts[c];
                        currCounts[c + 1] += prevCounts[c];
                    }
        }
        Output = WhichPart == 1 ? splits : currCounts.Sum();
    }
}
