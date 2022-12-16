namespace Advent2019;

public partial class Day17 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs

        IntCode code = new(Input);
        Dictionary<(int x, int y), char> map = new();
        long result = 0;

        #endregion Setup Variables and Parse Inputs

        if (WhichPart == 1)
        {
            int x = 0, y = 0;
            do
            {
                code.RunCodeWithNoReset();
                if (code.CodeComplete) continue;
                long asc = code.Output;
                if (asc == 10 && x != 0)
                {
                    x = 0;
                    y++;
                }
                else
                    map[(x++, y)] = (char)asc;
            } while (!code.CodeComplete);
            DrawMap(map);

            int maxX = map.Keys.Max(x => x.x), maxY = map.Keys.Max(x => x.y);
            for (int x1 = 1; x1 < maxX - 1; x1++)
                for (int y1 = 1; y1 < maxY - 1; y1++)
                    if (IsIntersection((x1, y1), map))
                        result += x1 * y1;
        }

        else
        {
            long[] instructions = new long[] { 65, 44, 66, 44, 65, 44, 67, 44, 66, 44, 65, 44, 67, 44, 65, 44, 67, 44, 66, 10, 76, 44, 49, 50, 44, 76, 44, 56, 44, 76, 44, 56, 10, 76, 44, 49, 50, 44, 82, 44, 52, 44, 76, 44, 49, 50, 44, 82, 44, 54, 10, 82, 44, 52, 44, 76, 44, 49, 50, 44, 76, 44, 49, 50, 44, 82, 44, 54, 10, 110, 10 };
            do code.RunCodeWithNoReset(instructions); while (!code.CodeComplete);
            result = code.Output;
        }

        Output = result.ToString();
    }

    #region Private Classes and Methods[

    private static void DrawMap(Dictionary<(int x, int y), char> map) //, (int x, int y) pos)
    {
        Debug.Print("---------------------------------------------------------------------");
        int maxX = map.Keys.Max(x => x.x), maxY = map.Keys.Max(x => x.y);
        int minX = map.Keys.Min(x => x.x), minY = map.Keys.Min(x => x.y);
        for (int y = minY; y <= maxY; y++)
        {
            StringBuilder s = new(y.ToString("d3") + ": ");
            for (int x = minX; x <= maxX; x++)
            {
                s.Append(map.ContainsKey((x, y)) ? map[(x, y)].ToString() : " ");
            }
            Debug.Print(s.ToString());
        }
    }

    private bool IsIntersection((int x, int y) pos, Dictionary<(int x, int y), char> map)
    {
        if (map[(pos.x, pos.y)] != '#') return false;
        int neighbours = 0;
        foreach ((int, int) offset in DirectNeighbours)
            neighbours += map[(pos.x + offset.Item1, pos.y + offset.Item2)] == '#' ? 1 : 0;
        return neighbours == 4;
    }
    #endregion Private Classes and Methods
}
