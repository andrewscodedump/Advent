namespace Advent2021;

public partial class Day06 : Advent.Day
{
    public override void DoWork()
    {
        int targetDays = Part2 ? 256 : TestMode && CurrentInput == 0 ? 18 : 80;
        long[] counts = new long[9];
        foreach (long fish in InputNumbers[0])
            counts[fish]++;

        for(int day = 0; day < targetDays; day++)
        {
            // Every day: 8s=0s; 6s=0s+7s;others=other+1s
            long zeroes = counts[0];
            for (int i = 1; i <= 8; i++)
                counts[i - 1] = counts[i];
            counts[6] += zeroes;
            counts[8] = zeroes;
        }
        Output = counts.Sum().ToString();
    }
}
