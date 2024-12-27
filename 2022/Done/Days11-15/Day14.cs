namespace Advent2022;

public partial class Day14 : Advent.Day
{
    public override void DoWork()
    {
        Dictionary<(int x, int y), char> map = DoScan(Inputs);
        int minX = map.Keys.Select(k => k.x).Min();
        int maxX = map.Keys.Select(k => k.x).Max();
        int maxY = map.Keys.Select(k => k.y).Max() + 2;
        bool ended, debug = false;
        int grainCount = 0;
        if (debug) DrawMap(map);
        do
        {
            ended = DropSand(map, minX, maxX, maxY);
            if (debug) DrawMap(map);
            if (!ended || Part2) grainCount++;
        } while (!ended);

        Output = grainCount.ToString();
    }

    private static Dictionary<(int, int), char> DoScan(string[] input)
    {
        Dictionary<(int, int), char> map = [];
        foreach (string s in input)
        {
            string[] vertices = s.Split(" -> ");
            for (int v = 0; v < vertices.Length - 1; v++)
            {
                DrawLine(map, vertices[v], vertices[v + 1]);
            }
        }
        return map;
    }

    private static void DrawLine(Dictionary<(int, int), char> map, string from, string to)
    {
        int startX = int.Parse(from.Split(',')[0]), startY = int.Parse(from.Split(',')[1]);
        int endX = int.Parse(to.Split(',')[0]), endY = int.Parse(to.Split(',')[1]);
        (startX, endX) = (Math.Min(startX, endX), Math.Max(startX, endX));
        (startY, endY) = (Math.Min(startY, endY), Math.Max(startY, endY));
        if (startX == endX)
        {
            for (; startY <= endY; startY++)
                map[(startX, startY)] = '#';
        }
        else if (startY == endY)
        {
            for (; startX <= endX; startX++)
                map[(startX, startY)] = '#';
        }
        else
        {
            // Unexpected input
            Debugger.Break();
        }
    }

    private bool DropSand(Dictionary<(int, int), char> map, int minX, int maxX, int maxY)
    {
        (int x, int y) = (500, 0);
        bool ended = false, stopped = false;
        do
        {
            if (!map.TryGetValue((x, y + 1), out char below)) below = '.';
            if (!map.TryGetValue((x - 1, y + 1), out char left)) left = '.';
            if (!map.TryGetValue((x + 1, y + 1), out char right)) right = '.';
            if (Part1)
            {
                if (x == minX) left = 'a';
                if (x == maxX) right = 'a';
            }
            if (Part2 && y + 1 == maxY)
            {
                left = '#'; below = '#'; right = '#';
            }
            if (below == '.')
                y++;
            else if (left == '.')
            {
                y++;
                x--;
            }
            else if (right == '.')
            {
                y++;
                x++;
            }
            else if (left == 'a' || right == 'a')
                ended = true;
            else
            {
                map[(x, y)] = 'o';
                stopped = true;
                if (Part2 && y == 0) ended = true;
            }
            if (Part1 && y > maxY) ended = true;
        } while (!ended && !stopped);

        return ended;

    }
    private void DrawMap(Dictionary<(int, int), char> map)
    {
        int minX = map.Keys.Select(x => x.Item1).Min();
        int maxX = map.Keys.Select(x => x.Item1).Max();
        int maxY = map.Keys.Select(y => y.Item2).Max();
        if (Part2) maxY += 1;
        StringBuilder drawing = new();
        for (int y = 0; y <= maxY; y++)
        {
            for (int x = minX; x <= maxX; x++)
            {
                if (!map.TryGetValue((x, y), out char item)) item = '.';
                if (Part2 && y == maxY) item = '#';
                drawing.Append(item);
            }
            drawing.AppendLine();
        }
        Debug.Print(drawing.ToString());
    }
}
