namespace Advent2024;

public partial class Day01 : Advent.Day
{
    public override void DoWork()
    {
        List<long> left = [], right = [];
        long sum1 = 0, sum2 = 0;

        foreach (long[] lists in InputNumbers)
        {
            left.Add(lists[0]);
            right.Add(lists[1]);
        }

        left.Sort();
        right.Sort();

        for (int i = 0; i < left.Count; i++)
        {
            sum1 += Math.Abs(left[i] - right[i]);
            sum2 += left[i] * right.Count(n => n == left[i]);
        }
        Output = (Part1 ? sum1 : sum2).ToString();
    }
}
