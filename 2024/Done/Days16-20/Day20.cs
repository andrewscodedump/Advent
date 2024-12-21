namespace Advent2024;

public partial class Day20 : Advent.Day
{
    public override void DoWork()
    {
        Dictionary<(int, int), int> route = tracert();
        int limit = TestMode ? 50 : 100, number = 0, cheatLength = Part1 ? 2 : 20;

        var bounds = Enumerable.Range(-cheatLength, (2 * cheatLength) + 1);
        (int x, int y)[] offsets = bounds.SelectMany(x => bounds.Select(y => (x, y)).Where(k => Math.Abs(k.x) + Math.Abs(k.y) <= cheatLength)).ToArray();
        foreach ((int x, int y) in route.Keys)
        {
            HashSet<(int, int)> cheatsUsed = [];
            foreach ((int dx, int dy) in offsets)
            {
                if (route.TryGetValue((x + dx, y + dy), out int oldValue)
                    && oldValue - route[(x, y)] - Math.Abs(dx) - Math.Abs(dy) >= limit
                    && cheatsUsed.Add((x + dx, y + dy)))
                    number++;
            }
        }
        Output = number.ToString();
    }

    Dictionary<(int, int), int> tracert()
    {
        PopulateMapFromInput();
        (int x, int y) pos = SimpleMap.First(p => p.Value == 'S').Key, end = SimpleMap.First(p => p.Value == 'E').Key;
        SimpleMap[end] = '.';
        int steps = 1;
        Dictionary<(int, int), int> route = new() { { pos, 1 } };
        do
        {
            pos = GetDirectNeighbours(pos).First(p => SimpleMap[p] == '.' && !route.ContainsKey(p));
            route[pos] = ++steps;
        } while (pos != end);

        return route;
    }
}
