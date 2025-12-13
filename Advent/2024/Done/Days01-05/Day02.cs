namespace Advent2024;

public partial class Day02 : Advent.Day
{
    public override void DoWork()
    {
        int totalSafe = 0;
        foreach (long[] report in InputNumbers)
        {
            if (IsReportSafe(report))
                totalSafe++;
            else if (Part2)
                for (int i = 0; i < report.Length; i++)
                    if (IsReportSafe([.. report[..i], .. report[(i + 1)..]]))
                    {
                        totalSafe++;
                        break;
                    }
        }
        Output = totalSafe.ToString();
    }

    static bool IsReportSafe(long[] report)
    {
        long diff, prevDiff = 0;
        for (int i = 0; i < report.Length - 1; i++)
        {
            diff = report[i] - report[i + 1];
            if (diff == 0 || Math.Abs(diff) > 3 || diff * prevDiff < 0) return false;
            prevDiff = diff;
        }
        return true;
    }
}
