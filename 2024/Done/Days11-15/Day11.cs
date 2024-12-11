namespace Advent2024;

public partial class Day11 : Advent.Day
{
    readonly Dictionary<long, List<long>> knownReplacements = [];
    readonly Dictionary<(long, long), long> knownCounts = [];

    public override void DoWork()
    {
        int rounds = Part1 ? 25 : 75;
        long result = InputNumbers[0].Sum(n => GetCount(n, 1, rounds));

        Output = result.ToString();
    }

    private long GetCount(long input, long countIn, int remaining)
    {
        if (remaining == 0) return countIn;
        if (!knownCounts.TryGetValue((input, remaining), out long count))
        {
            List<long> replacement = GetReplacements(input);
            count = GetCount(replacement[0], countIn, remaining - 1);
            if (replacement.Count == 2)
                count += GetCount(replacement[1], countIn, remaining - 1);
            knownCounts[(input, remaining)] = count;
        }
        return count;
    }

    private List<long> GetReplacements(long current)
    {
        if (!knownReplacements.TryGetValue(current, out List<long> replacements))
        {
            if (current == 0)
                replacements = [1];
            else
            {
                int len = (int)Math.Floor(Math.Log10(current) + 1);
                replacements = len % 2 == 0
                    ? [(long)(current / Math.Pow(10, len / 2.0)), (long)(current % Math.Pow(10, len / 2.0))]
                    : [current * 2024];
            }
            knownReplacements.Add(current, replacements);
        }
        return replacements;
    }
}
