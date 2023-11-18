namespace Advent2021;

public partial class Day05 : Advent.Day
{
    public override void DoWork()
    {
        Dictionary<(int, int),int> map = new();

        foreach (string line in Inputs)
        {
            int[] coords = Regex.Split(line, @"[^\d]+").Select(int.Parse).ToArray();
            (int x1, int y1, int x2, int y2) = (coords[0], coords[1], coords[2], coords[3]);
            int xStep = x1 == x2 ? 0 : x1 > x2 ? -1 : 1;
            int yStep = y1 == y2 ? 0 : y1 > y2 ? -1 : 1;
            if (Part1 && xStep != 0 && yStep != 0) continue;
            (int x, int y) = (x1, y1);
            do
            {
                if (!map.ContainsKey((x, y))) map[(x, y)] = 0;
                map[(x, y)]++;
                if ((x, y) == (x2, y2)) break;
                x += xStep; y += yStep;
            } while (true);
        }

        Output = map.Count(p => p.Value > 1).ToString();
    }
}
