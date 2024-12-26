namespace Advent2024;

public partial class Day25 : Advent.Day
{
    public override void DoWork()
    {
        List<List<int>> locks = [], keys = [];

        foreach (string[] block in InputBlocks)
            (block[0] == "#####" ? locks : keys).Add(Enumerable.Range(0, 5).Select(i => block[1..6].Sum(l => l[i] == '#' ? 1 : 0)).ToList());

        int validPairs = locks.SelectMany(@lock => keys.Select(key => (l: @lock, k:key))).Count(p => p.l.Zip(p.k).All(p => p.First + p.Second <= 5));

        Output = validPairs.ToString();
    }
}