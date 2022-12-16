namespace Advent2018;

public partial class Day20 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs
        string route = Input;
        Queue<((int x, int y), int ptr, int len, int nextPos)> q = new();
        Dictionary<(int x, int y), (char state, int dist)> map = new() { { (0, 0), ('X', int.MaxValue) }, { (-1, 1), ('#', 0) }, { (0, 1), ('?', int.MaxValue) }, { (1, 1), ('#', 0) }, { (-1, 0), ('?', int.MaxValue) }, { (1, 0), ('?', int.MaxValue) }, { (-1, -1), ('#', 0) }, { (0, -1), ('?', int.MaxValue) }, { (1, -1), ('#', 0) } };
        q.Enqueue(((0, 0), 0, 0, 0));
        #endregion Setup Variables and Parse Inputs

        do
        {
            ((int x, int y) pos, int ptr, int len, int nextPos) = q.Dequeue();
            (int x, int y) newPos = (0, 0);

            if (route[ptr] != '$')
                if (route[ptr] == '^')
                    q.Enqueue((pos, ++ptr, len, 0));
                else if (map.ContainsKey((pos.x, pos.y)))
                    switch (route[ptr])
                    {
                        case 'N':
                        case 'S':
                        case 'E':
                        case 'W':
                            do
                            {
                                (int x, int y) inc = Directions[route[ptr]];
                                char door = "NS".Contains(route[ptr]) ? '-' : '|';
                                newPos = (pos.x + inc.x, pos.y + inc.y);
                                map[newPos] = (door, int.MaxValue);
                                newPos = (newPos.x + inc.x, newPos.y + inc.y);
                                for (int x = -1; x < 2; x++)
                                    for (int y = -1; y < 2; y++)
                                        if (x == 0 && y == 0)
                                            map[(newPos.x + x, newPos.y + y)] = ('.', int.MaxValue);
                                        else if (x == 0 || y == 0)
                                            if (!map.ContainsKey((newPos.x + x, newPos.y + y)))
                                                map.Add((newPos.x + x, newPos.y + y), ('?', int.MaxValue));
                                            else { }
                                        else
                                            if (!map.ContainsKey((newPos.x + x, newPos.y + y)))
                                            map.Add((newPos.x + x, newPos.y + y), ('#', 0));
                                pos = newPos;
                                ptr++;
                            } while ("NSEW".Contains(route[ptr]));
                            q.Enqueue("(".Contains(route[ptr]) ? (newPos, ptr, len, 0) : (newPos, nextPos == 0 ? ptr : nextPos, len, 0));
                            break;
                        case '|':
                            if (nextPos != 0) q.Enqueue((newPos, nextPos, len, 0));
                            break;
                        case ')':
                            break;
                        case '(':
                            List<((int, int), int, int, int)> tbq = new();
                            int offset = 0;
                            newPos = (pos.x, pos.y);
                            tbq.Add((newPos, ++ptr, len, 0));
                            int brackets = 1;
                            bool qNext = false;
                            do
                            {
                                offset++;
                                if (route[ptr + offset] == '|' && brackets == 1)
                                    qNext = true;
                                else if (qNext)
                                {
                                    tbq.Add((newPos, ptr + offset, len, 0));
                                    qNext = false;
                                }
                                if (route[ptr + offset] == '(') brackets++;
                                if (route[ptr + offset] == ')') brackets--;
                            } while (brackets > 0);
                            ptr++;
                            for (int i = 0; i < tbq.Count; i++)
                            {
                                ((int, int), int, int, int ni) qi = tbq[i];
                                qi.ni = ptr + offset;
                                q.Enqueue(qi);
                            }
                            break;
                    }

        } while (q.Count > 0);

        List<(int x, int y)> keys = new(map.Keys);
        foreach ((int x, int y) pos in keys)
        {
            map[pos] = (map[pos].state == '?' ? '#' : map[pos].state, map[pos].dist);
        }

        //printIt(map);

        Queue<((int, int) pos, int dist, HashSet<(int, int)> beenThere)> bfs = new();
        bfs.Enqueue(((0, 0), 0, new HashSet<(int, int)>()));
        do
        {
            ((int x, int y) pos, int dist, HashSet<(int, int)> beenThere) = bfs.Dequeue();
            if (beenThere.Contains(pos)) continue;
            if (dist < map[pos].dist)
                map[pos] = (map[pos].state, dist);
            foreach ((int x, int y) in DirectNeighbours)
                if (map[(pos.x + x, pos.y + y)].state != '#')
                    bfs.Enqueue(((pos.x + (2 * x), pos.y + (2 * y)), dist + 1, new(beenThere) { pos }));
        } while (bfs.Count > 0);

        int maxLen = 0;
        int longRoutes = 0;
        foreach ((char state, int dist) in map.Values)
        {
            if (state == '.')
            {
                maxLen = Math.Max(maxLen, dist);
                if (dist >= 1000) longRoutes++;
            }
        }

        Output = (WhichPart == 1 ? maxLen : longRoutes).ToString();
    }

    #region Private Classes and Methods
    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "For Debugging")]
    private static void PrintIt(Dictionary<(int x, int y), (char state, int dist)> map)
    {
        for (int y = map.Keys.Max(n => n.y); y >= map.Keys.Min(n => n.y); y--)
        {
            StringBuilder line = new();
            for (int x = map.Keys.Min(n => n.x); x <= map.Keys.Max(n => n.x); x++)
                line.Append(map.ContainsKey((x, y)) ? map[(x, y)].state : ' ');
            Debug.WriteLine(line);
        }
        Debug.WriteLine("");
    }
    #endregion Private Classes and Methods
}
