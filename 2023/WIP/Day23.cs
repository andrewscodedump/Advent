using System.Security.Cryptography.Xml;

namespace Advent2023;

public partial class Day23 : Advent.Day
{
    public override void DoWork()
    {
        PopulateMapFromInput(out int width, out int height);
        int longestDistance = 0;
        char[] directions = ['^', '<', '>', 'v'];

        if (Part1)
        {
            PriorityQueue<(int, int, int, Dictionary<(int, int), int>), (int, int)> queue = new();
            queue.Enqueue((1, 0, 0, new() { { (1, 0), 0 } }), (width + height - 4, 0));
            do
            {
                (int x, int y, int distance, Dictionary<(int, int), int> visited) = queue.Dequeue();
                foreach (char direction in directions)
                {
                    (int dx, int dy) = DirectionsYDown[direction];
                    (int x, int y) newPos = (x + dx, y + dy);
                    if (newPos.x < 0 || newPos.y < 0) continue;
                    char nextSquare = SimpleMap[newPos];
                    if (nextSquare == '#') continue;
                    if (visited.ContainsKey(newPos))
                        continue;
                    else
                        visited[newPos] = distance + 1;
                    if (directions.Contains(nextSquare) && nextSquare != direction) continue;
                    if (newPos.y == height - 1)
                    {
                        longestDistance = Math.Max(longestDistance, distance + 1);
                        continue;
                    }
                    queue.Enqueue((newPos.x, newPos.y, distance + 1, new(visited)), (width - newPos.x - 1 + height - newPos.y - 1, -distance));
                }
            } while (queue.Count > 0);
        }
        else
        {
            List<(int, int)> junctions = [(1, 0), (width - 2, height - 1)];
            Dictionary<((int x, int y) from, (int x, int y) to), int> edges = [];

            foreach ((int, int) pos in SimpleMap.Keys)
                if (directions.Contains(SimpleMap[pos]))
                    SimpleMap[pos] = '.';
            // Find all junctions
            foreach ((int x, int y) in SimpleMap.Keys.Where(p => SimpleMap[p] == '.'))
            {
                int paths = 0;
                if (y == 0 || y == height - 1) continue;
                foreach ((int dx, int dy) in DirectNeighbours)
                    if (SimpleMap[(x + dx, y + dy)] == '.') paths++;
                if (paths > 2) junctions.Add((x, y));
            }
            // for each junction, get the distance to each of the next junctions
            foreach ((int sx, int sy) in junctions)
            {
                (int x, int y) = (sx, sy);
                (int px, int py) = (x, y);
                foreach ((int dx, int dy) in DirectNeighbours)
                {
                    bool found = false;
                    int length = 0;
                    do
                    {
                        foreach ((int ddx, int ddy) in DirectNeighbours)
                        {
                            if (x + ddx == px && y + ddy == py) continue;
                            if (x + ddx < 0 || y + ddy == height) continue;
                            if (SimpleMap[(x + ddx, y + ddy)] == '#') continue;
                            (px, py) = (x, y);
                            (x, y) = (x + ddx, y + ddy);
                            if (junctions.Contains((x, y)))
                            {
                                found = true;
                                break;
                            }
                        }
                    } while (!found);
                    if (!edges.ContainsKey(((x, y), (sx, sy))))
                        edges[((sx, sy), (x, y))] = length;
                }
            }
        }
        Output = longestDistance.ToString();
    }
}
