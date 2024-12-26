namespace Advent2024;

public partial class Day19 : Advent.Day
{
    private readonly Dictionary<string, long> patterns = [];
    string[] towels = [];

    public override void DoWork()
    {
        towels = Input.Split(", ");
        long count = Part1
            ? Inputs[2..].Count(p => Arrangements(p) != 0)
            : Inputs[2..].Sum(Arrangements);

        Output = count.ToString();
    }

    private long Arrangements(string remaining)
    {
        if (patterns.TryGetValue(remaining, out long count)) return count;
        foreach (string towel in towels)
        {
            if (remaining == "") return 1;
            if (remaining.StartsWith(towel))
                count += Arrangements(remaining[towel.Length..]);
        }
        patterns[remaining] = count;
        return count;
    }
}
