namespace Advent2021;

public partial class Day06 : Advent.Day
{
    public override void DoWork()
    {
        int targetDays = WhichPart == 2 ? 256 : TestMode && CurrentInput == 0 ? 18 : 80;
        long[] counts = new long[9];
        foreach (int fish in InputSplitCInt.ToList())
            counts[fish]++;

        for(int day = 0; day < targetDays; day++)
        {
            // Every day: 8s=0s; 6s=0s+7s;others=other+1s
            long[] newCounts = new long[9];

            for (int i =0;i <= 8; i++)
            {
                if (i == 0)
                {
                    newCounts[8] = counts[i];
                    newCounts[6] = counts[i];
                }
                else newCounts[i - 1] += counts[i];
            }
            counts = newCounts;
        }
        Output = counts.Sum().ToString();
    }
}
