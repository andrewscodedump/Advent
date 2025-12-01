namespace Advent2019;

public partial class Day15 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs

        IntCode baseCode = new(InputNumbers[0]);
        Queue<((int x, int y) pos, int dirn, int dist, IntCode code, HashSet<(int, int)>)> bfs = new();
        Dictionary<(int, int), (bool wall, int dist)> map = new() { { (0, 0), (false, 0) } };
        int bestDist = Int32.MaxValue;

        #endregion Setup Variables and Parse Inputs

        for (int d = 1; d <= 4; d++)
            bfs.Enqueue(((0, 0), d, 0, baseCode.Clone(), new HashSet<(int, int)>() { (0, 0) }));
        do
        {
            ((int x, int y) oldPos, int dirn, int dist, IntCode code, HashSet<(int, int)> beenThere) = bfs.Dequeue();
            (int x, int y) pos = (oldPos.x + dirns[dirn].x, oldPos.y + dirns[dirn].y);
            if (!map.TryGetValue(pos, out (bool wall, int prevDist) state))
                map[pos] = (false, Int32.MaxValue);
            int newDist = dist + 1;
            code.RunCodeWithNoReset(dirn);
            switch (code.Output)
            {
                case 0:
                    map[pos] = (state.wall, newDist);
                    break;
                case 1:
                    map[pos] = (true, newDist);
                    DrawMap(map, pos);
                    continue;
                case 2:
                    bestDist = Math.Min(newDist, bestDist);
                    continue;
            }
            DrawMap(map, pos);
            for (int newDirn = 1; newDirn <= 4; newDirn++)
            {
                if (dirn + newDirn == 3 || dirn + newDirn == 7) continue;
                (int x, int y) newPos = (pos.x + dirns[dirn].x, pos.y + dirns[dirn].y);
                if (!map.ContainsKey(newPos))
                    map[newPos] = (false, Int32.MaxValue);
                if (map[newPos].wall || newDist >= map[newPos].dist || newDist > bestDist || beenThere.Contains(newPos))
                    continue;
                bfs.Enqueue((pos, newDirn, newDist, code.Clone(), new HashSet<(int, int)>(beenThere) { pos }));
            }

        } while (bfs.Count > 0);

        Output = bestDist.ToString();
    }

    #region Private Classes and Methods
    protected readonly Dictionary<int, (int x, int y)> dirns = new() { { 1, (0, 1) }, { 2, (0, -1) }, { 3, (-1, 0) }, { 4, (1, 0) } };

    private static void DrawMap(Dictionary<(int x, int y), (bool wall, int dist)> map, (int x, int y) pos)
    {
        Debug.Print("---------------------------------------------------------------------");
        int maxX = Math.Max(pos.x, map.Keys.Max(x => x.x)), maxY = Math.Max(pos.y, map.Keys.Max(x => x.y));
        int minX = Math.Min(pos.x, map.Keys.Min(x => x.x)), minY = Math.Min(pos.y, map.Keys.Min(x => x.y));
        for (int y = maxY; y >= minY; y--)
        {
            StringBuilder s = new();
            for (int x = minX; x <= maxX; x++)
            {
                s.Append((x, y) == pos ? "." : map.ContainsKey((x, y)) && map[(x, y)].wall ? "#" : " ");
            }
            Debug.Print(s.ToString());
        }
    }
    #endregion Private Classes and Methods
}
