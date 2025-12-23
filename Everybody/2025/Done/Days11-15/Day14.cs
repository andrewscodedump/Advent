namespace Everybody2025;

public class Day14 : Advent.Day
{
    public override void DoWork()
    {
        DrawMaps = true;
        PopulateMapFromInputWithBorders('x');
        long rounds = WhichPart switch { 1 => 10, 2 => 2025, _ => 1000000000 };
        switch (WhichPart)
        {
            case 1:
            case 2:
                Output = Enumerable.Range(0, (int)rounds).Sum(x => PlayRound()).ToString();
                break;
            case 3:
                CreateEmptyMap();
                long count = 0;
                Dictionary<string, long> seen = [];
                bool loopFound = false;
                for (long i = 1; i <= rounds; i++)
                {
                    int localCount = PlayRound();
                    if (CheckCentre())
                        count += localCount;
                    string hash = string.Join("", SimpleMap.Values);
                    if (!loopFound && !seen.TryAdd(hash, i))
                    {
                        // This only works because the repeat starts at 1.  Would need more work if it didn't.
                        loopFound = true;
                        long totalCycles = rounds / (i - seen[hash]);
                        count *= totalCycles;
                        i = totalCycles * (i - seen[hash]);
                    }
                }
                Output = count.ToString();
                break;
        }
    }

    private readonly (int, int)[] diagonals = [(-1, -1), (1, 1), (-1, 1), (1, -1)];

    private int PlayRound()
    {
        StartingMap = new(SimpleMap);
        foreach (KeyValuePair<(int x, int y), char> kvp in StartingMap)
        {
            if (kvp.Value == 'x') continue;
            int neighbours = 0;
            foreach ((int dx, int dy) in diagonals)
                neighbours += (StartingMap[(kvp.Key.x + dx, kvp.Key.y + dy)] == '#') ? 1 : 0;
            if (neighbours % 2 == 0)
                SimpleMap[kvp.Key] = kvp.Value == '.' ? '#' : '.';
        }
        return SimpleMap.Values.Count(v => v == '#');
    }

    private void CreateEmptyMap()
    {
        for (int x = -1; x < 35; x++)
            for (int y = -1; y < 35; y++)
                if (x == -1 || y == -1 || x == 34 || y == 34) SimpleMap[(x, y)] = 'x';
                else SimpleMap[(x, y)] = '.';
    }

    private bool CheckCentre()
    {
        for (int x = 0; x < 8; x++)
            for (int y = 0; y < 8; y++)
                if (Inputs[x][y] != SimpleMap[(x + 13, y + 13)]) return false;
        return true;
    }
}
