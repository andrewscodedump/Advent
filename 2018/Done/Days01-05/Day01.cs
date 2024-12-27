namespace Advent2018;

public partial class Day01 : Advent.Day
{
    public override void DoWork()
    {
        int total = 0; int doubleValue = 0;
        bool doubleFound = false;
        List<int> totals = [];
        HashSet<int> found = [];

        foreach (string numString in Inputs)
        {
            total += int.Parse(numString);
            doubleValue = total;
            doubleFound = found.Contains(doubleValue);
            if (doubleFound && Part2)
                break;
            totals.Add(total);
            found.Add(total);
        }

        if (Part2)
            do
                for (int pos = 0; !doubleFound && pos < totals.Count; pos++)
                {
                    totals[pos] += total;
                    doubleValue = totals[pos];
                    doubleFound = found.Contains(doubleValue);
                    if (!doubleFound)
                        found.Add(totals[pos]);
                }
            while (!doubleFound);
        Output = (Part1 ? total : doubleValue).ToString();
    }
}
