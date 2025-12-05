namespace Advent2025;

public class Day05 : Advent.Day
{
    public override void DoWork()
    {
        List<long[]> fresh = [.. InputNumbersPositive.Where(l => l.Length == 2).OrderBy(l => l[0])];
        long[] ingredients = [.. InputNumbers.Where(l => l.Length == 1).Select(l => l[0])];

        if (WhichPart == 2)
            for (int i = 0; i < fresh.Count - 1; i++)
            {
                if (fresh[i][1] >= fresh[i + 1][0])
                {
                    if (fresh[i + 1][1] > fresh[i][1]) // For the case where the next range is completely subsumed by this one
                        fresh[i][1] = fresh[i + 1][1];
                    fresh.RemoveAt(i + 1);
                    i--; // For the case where multiple subsequent ranges all overlap this one
                }
            }

        Output = WhichPart switch {
            1 => ingredients.Where(i => fresh.Exists(f => i >= f[0] && i <= f[1])).Count(),
            _ => fresh.Sum(f => f[1] - f[0] + 1)
        };
    }
}
