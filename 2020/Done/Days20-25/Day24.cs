namespace Advent2020;

public partial class Day24 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs

        (int x, int y) pos = (0, 0);
        Dictionary<(int x, int y), int> floor = new();
        (int minX, int minY, int maxX, int maxY) = (0, 0, 0, 0);
        Dictionary<string, (int, int)> directions = new() { { "nw", (-1, 1) }, { "ne", (1, 1) }, { "w", (-2, 0) }, { "e", (2, 0) }, { "sw", (-1, -1) }, { "se", (1, -1) } };

        #endregion Setup Variables and Parse Inputs

        foreach (string path in Inputs)
        {
            pos = (0, 0);
            for (int p = 0; p < path.Length; p++)
            {
                (int dx, int dy) = "ns".Contains(path[p]) ? directions[path[p..(p++ + 2)]] : directions[path[p].ToString()];
                pos = (pos.x + dx, pos.y + dy);
            }
            floor[pos] = floor.TryGetValue(pos, out int curValue) ? Math.Abs(curValue - 1) : 1;
        }

        if (Part2)
        {
            for (int day = 1; day <= 100; day++)
            {
                minX = floor.Keys.Select(k => k.x).Min(); maxX = floor.Keys.Select(k => k.x).Max();
                minY = floor.Keys.Select(k => k.y).Min(); maxY = floor.Keys.Select(k => k.y).Max();
                Dictionary<(int, int), int> newFloor = new(floor);
                for (int x = minX - 1; x <= maxX + 1; x++)
                    for (int y = minY - 1; y <= maxY + 1; y++)
                    {
                        int neighbours = 0;
                        // can't have even Y and odd X or vice-versa
                        if ((x + y) % 2 != 0) continue;
                        if (!floor.ContainsKey((x, y)))
                            floor[(x, y)] = 0;
                        foreach ((int dx, int dy) in directions.Values)
                            if (floor.ContainsKey((x + dx, y + dy)))
                                neighbours += floor[(x + dx, y + dy)];
                        if (floor[(x, y)] == 1 && (neighbours == 0 || neighbours > 2))
                            newFloor[(x, y)] = 0;
                        if (floor[(x, y)] == 0 && neighbours == 2)
                            newFloor[(x, y)] = 1;
                    }
                floor = new(newFloor);
            }
        }
        Output = floor.Values.Sum().ToString();
    }
}
