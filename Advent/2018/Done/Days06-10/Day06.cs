namespace Advent2018;

public partial class Day06 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs
        int minX = int.MaxValue, minY = int.MaxValue;
        int maxX = 0, maxY = 0;
        int maxArea = 0;
        int distanceLimit = TestMode ? 32 : 10000;
        char[] splitter = [',', ' '];

        Dictionary<(int x, int y), int> points = [];
        for (int i = 0; i < Inputs.Length; i++)
        {
            string[] coords = Inputs[i].Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            int x = int.Parse(coords[0]), y = int.Parse(coords[1]);
            minX = Math.Min(x, minX); minY = Math.Min(y, minY); maxX = Math.Max(x, maxX); maxY = Math.Max(y, maxY);
            points.Add((x, y), 0);
        }
        #endregion Setup Variables and Parse Inputs

        Dictionary<(int, int), (int dist, (int, int) point)> grid = [];
        for (int x = minX - 1; x <= maxX + 1; x++)
            for (int y = minY - 1; y <= maxY + 1; y++)
            {
                if (Part1)
                {
                    grid.Add((x, y), (int.MaxValue, (-1, -1)));
                    foreach ((int ptx, int pty) p in points.Keys)
                    {
                        int distance = Math.Abs(x - p.ptx) + Math.Abs(y - p.pty);
                        if (grid[(x, y)].dist == distance)
                            grid[(x, y)] = (distance, (-1, -1));
                        else if (distance < grid[(x, y)].dist)
                            grid[(x, y)] = (distance, p);
                        if (distance == 0) break;
                    }
                }
                else
                {
                    int distSoFar = 0;
                    foreach ((int px, int py) in points.Keys)
                        if ((distSoFar += Math.Abs(x - px) + Math.Abs(y - py)) >= distanceLimit) break;
                    if (distSoFar < distanceLimit) maxArea++;
                }
            }

        if (Part1)
            foreach (KeyValuePair<(int x, int y), (int, (int, int) point)> kvp in grid)
            {
                if (kvp.Value.point == (-1, -1) || points[kvp.Value.point] == -1) continue;
                if (kvp.Key.x < minX || kvp.Key.x > maxX || kvp.Key.y < minY || kvp.Key.y > maxY)
                {
                    points[kvp.Value.point] = -1;
                    continue;
                }
                maxArea = Math.Max(maxArea, ++points[kvp.Value.point]);
            }

        Output = maxArea.ToString();
    }
}
