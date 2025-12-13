namespace Advent2023;

public partial class Day23 : Advent.Day
{
    public override void DoWork()
    {
        char[] directions = ['^', '<', '>', 'v'];
        PopulateMapFromInputWithBorders('#', out int width, out int height);
        if (Part2) SimpleMap.Keys.Where(p => directions.Contains(SimpleMap[p])).ForEach(p => SimpleMap[p] = '.'); // Open up all the slopes
        int longestDistance = 0;
        (int x, int y) start = (1, 0), end = (width - 2, height - 1);

        List<(int x, int y)> junctions = [start];
        Dictionary<((int x, int y) from, (int x, int y) to), int> edges = [];

        //Get all junctions
        SimpleMap.Keys.Where(p => SimpleMap[p] == '.').ForEach(p =>
            {
                if (CountDirectNeighbours(SimpleMap, p.Item1, p.Item2, '#') <= 1)
                    junctions.Add((p.Item1, p.Item2));
            });
        junctions.Add(end);

        // for each junction, get the distance to each of the next junctions
        foreach ((int sx, int sy) in junctions)
        {
            if ((sx, sy) == end) continue;
            foreach (char direction in directions)
            {
                bool found = false;
                int length = 0;
                (int x, int y) = (sx, sy);
                (int px, int py) = (sx, sy);
                (int dx, int dy) = DirectionsYDown[direction];
                if (x + dx == px && y + dy == py) continue;
                char nextSquare = SimpleMap[(x + dx, y + dy)];
                if (nextSquare == '#') continue;
                if (directions.Contains(nextSquare) && nextSquare != direction) continue;
                length++;
                (px, py) = (x, y);
                (x, y) = (x + dx, y + dy);
                do
                {
                    foreach (char dirn in directions)
                    {
                        (int ddx, int ddy) = DirectionsYDown[dirn];
                        if (x + ddx == px && y + ddy == py) continue;
                        nextSquare = SimpleMap[(x + ddx, y + ddy)];
                        if (nextSquare == '#') continue;
                        if (directions.Contains(nextSquare) && nextSquare != dirn) continue;
                        length++;
                        (px, py) = (x, y);
                        (x, y) = (x + ddx, y + ddy);
                        if (junctions.Contains((x, y)))
                        {
                            found = true;
                            break;
                        }
                    }
                } while (!found);
                edges[((sx, sy), (x, y))] = length;
            }
        }
        (int, int) secondFromEnd = edges.Keys.First(k=>k.to==end).from;

        Queue<((int, int), int, Dictionary<(int, int), int>, List<(int, int)>)> queue = new();
        queue.Enqueue((start, 0, new() { { start, 0 } }, [start]));
        do
        {
            ((int x, int y) source, int distance, Dictionary<(int, int), int> visited, List<(int, int)> path) = queue.Dequeue();
            foreach ((int, int) target in edges.Keys.Where(k => k.from == source).Select(k => k.to))
            {
                if (visited.ContainsKey(target)) continue;
                if (source == secondFromEnd && target != end) continue;
                int edgeLength = edges[(source, target)];
                Dictionary<(int, int), int> newVisited = new(visited) { [target] = distance + edgeLength };
                if (target == end)
                {
                    longestDistance = Math.Max(longestDistance, distance + edgeLength);
                    continue;
                }
                List<(int, int)> newPath = new(path) { target };
                queue.Enqueue((target, distance + edgeLength, newVisited, newPath));
            }
        } while (queue.Count > 0);

        Output = longestDistance.ToString();
    }
}