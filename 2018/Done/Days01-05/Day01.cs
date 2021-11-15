namespace Advent2018;

public partial class Day01 : Advent.Day
{
    public override void DoWork()
    {
        int total = 0; int doubleValue = 0;
        bool doubleFound = false;
        List<int> totals = new();
        HashSet<int> found = new();

        foreach (string numString in InputSplit)
        {
            if ((doubleFound = found.Contains(doubleValue = total += int.Parse(numString))) && WhichPart == 2) break;
            totals.Add(total);
            found.Add(total);
        }

        if (WhichPart == 2)
            do
                for (int pos = 0; !doubleFound && pos < totals.Count; pos++)
                    if (!(doubleFound = found.Contains(doubleValue = totals[pos] += total)))
                        found.Add(totals[pos]);
            while (!doubleFound);
        Output = (WhichPart == 1 ? total : doubleValue).ToString();
    }
}
