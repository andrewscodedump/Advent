namespace Advent2024;

public partial class Day20 : Advent.Day
{
    public override void DoWork()
    {
        PopulateMapFromInput();
        Dictionary<(int, int), int> route = tracert();
        long limit = TestMode ? 50 : 100, number = 0, cheatLength = Part1 ? 2 : 20;

        foreach ((int x, int y) in route.Keys)
        {
            HashSet<(int, int)> cheatsUsed = [];
            for (int cx = 0; cx <= cheatLength; cx++)
            {
                for (int cy = 0; cy <= cheatLength; cy++)
                {
                    if (cx + cy > cheatLength) continue;
                    List<(int, int)> combos = [(-1, 1), (1, 1), (-1, -1), (1, -1)];
                    foreach ((int dx, int dy) in combos)
                    {
                        (int x, int y) newPos = (x + (cx * dx), y + (cy * dy));
                        if (!SimpleMap.ContainsKey(newPos)) continue;
                        if (route.TryGetValue(newPos, out int oldValue))
                        {
                            int saving = oldValue - route[(x, y)] - cx - cy;
                            if (saving >= limit && !cheatsUsed.Contains(newPos)) number++;
                            cheatsUsed.Add(newPos);
                        }
                    }
                }
            }
        }
        Output = number.ToString();
    }

    Dictionary<(int, int), int> tracert()
    {
        (int x, int y) pos = SimpleMap.First(p => p.Value == 'S').Key, end = SimpleMap.First(p => p.Value == 'E').Key;
        SimpleMap[end] = '.';
        int steps = 1;
        Dictionary<(int, int), int> route = new() { { pos, 1 } };
        do
        {
            steps++;
            foreach ((int dx, int dy) in DirectNeighbours)
            {
                (int, int) newPos = (pos.x + dx, pos.y + dy);
                if (SimpleMap[newPos] == '.' && !route.ContainsKey(newPos))
                {
                    pos = newPos;
                    route[pos] = steps;
                    break;
                }
            }
        } while (pos != end);

        return route;
    }
}
