namespace Advent2022;

public partial class Day12 : Advent.Day
{
    public override void DoWork()
    {
        Dictionary<(int, int), int> bestSteps = [];
        PopulateMapFromInput(out int maxX, out int maxY);
        (int, int) end = SimpleMap.Keys.First(k => SimpleMap[k] == 'E');
        SimpleMap[end] = '{';
        (int, int) start = SimpleMap.Keys.First(k => SimpleMap[k] == 'S');

        Queue<(List<(int, int)>, int)> bfs = new();
        bfs.Enqueue(([start], 0));
        if (Part2)
            foreach ((int, int) a in SimpleMap.Keys.Where(k => SimpleMap[k] == 'a'))
                bfs.Enqueue(([a], 0));

        do
        {
            (List<(int, int)> path, int steps) = bfs.Dequeue();
            (int oldX, int oldY)= path[^1];
            steps++;
            foreach((int dx, int dy) in DirectNeighbours)
            {
                if (oldX + dx < 0 || oldX + dx > maxX) continue;
                if (oldY + dy < 0 || oldY + dy > maxY) continue;
                (int, int) newPos = (oldX + dx, oldY + dy);
                if (!SimpleMap.TryGetValue(newPos, out char newHeight)) newHeight = '{';
                if (SimpleMap[(oldX, oldY)] != 'S' && newHeight - SimpleMap[(oldX, oldY)] > 1) continue;
                if (bestSteps.TryGetValue(newPos, out int value) && value <= steps) continue;
                if (path.Contains(newPos)) continue;
                bestSteps[newPos] = steps;
                List<(int, int)> newPath = new(path) { newPos };
                bfs.Enqueue((newPath, steps));
            }
        } while(bfs.Count > 0);

        Output = bestSteps[end].ToString();
    }
}
