namespace Advent2021;

public partial class Day09 : Advent.Day
{
    public override void DoWork()
    {
        int height = Inputs.Length, width = Input.Length, result = 0;
        List<(int, int)> Offsets = new() { (0, 1), (1, 0), (0, -1), (-1, 0) };
        Dictionary<(int, int), char> map = new();
        Dictionary<(int, int), int> minima = new();

        for (int y = -1; y <= height; y++)
            for (int x = -1; x <= width; x++)
                map[(x, y)] = y == -1 || x == -1 || y == height || x == width ? '9' : Inputs[y][x];

        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                char test = map[(x, y)];
                if (Offsets.Where(nbr => map[(x + nbr.Item1, y + nbr.Item2)] > test).Count() == 4)
                {
                    result += int.Parse(test.ToString()) + 1;
                    minima[(x, y)] = 1;
                }
            }

        if (Part2)
        {
            Dictionary<(int x, int y), (int mx, int my)> processed = new();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bool foundMinimum = false;
                    if (map[(x, y)] == '9' || minima.ContainsKey((x, y))) continue;
                    (int tx, int ty) = (x, y);
                    do
                    {
                        (int lx, int ly) = (x, y);
                        foreach ((int dx, int dy) in Offsets)
                        {
                            if (map[(tx + dx, ty + dy)] > map[(lx, ly)]) continue;
                            (lx, ly) = (tx + dx, ty + dy);
                        }
                        if (processed.ContainsKey((lx, ly)))
                        {
                            minima[processed[(lx, ly)]]++;
                            foundMinimum = true;
                        }
                        else if (minima.ContainsKey((tx, ty)))
                        {
                            processed[(x, y)] = (tx, ty);
                            minima[(tx, ty)]++;
                            foundMinimum = true;
                        }
                        (tx, ty) = (lx, ly);
                    } while (!foundMinimum);
                }
            }
            result = minima.Values.OrderByDescending(v => v).Take(3).Aggregate(1, (acc, val) => acc * val);
        }

        Output = result.ToString();
    }
}
