namespace Advent2024;

public partial class Day16 : Advent.Day
{
    public override void DoWork()
    {
        PopulateMapFromInput();
        (int x, int y) start = SimpleMap.Keys.First(k => SimpleMap[k] == 'S');
        (int x, int y) end = SimpleMap.Keys.First(k => SimpleMap[k] == 'E');
        SimpleMap[start] = '.'; SimpleMap[end] = '.';
        HashSet<(int, int)> goodSeats = [];

        int lowestScore = int.MaxValue;
        Dictionary<((int, int), char), long> visited = new() { { (start, 'E'), int.MaxValue } };

        PriorityQueue<((int, int), char, int, HashSet<(int, int)>), int> q = new();
        q.Enqueue((start, 'S', 1000, [start, end]), 0);
        q.Enqueue((start, 'N', 1000, [start, end]), 0);
        q.Enqueue((start, 'E', 0, [start, end]), 0);
        do
        {
            ((int x, int y) pos, char heading, int score, HashSet<(int, int)> path) = q.Dequeue();
            if (score > lowestScore) continue;
            if (score == lowestScore && (pos != end || Part1)) continue;
            if (SimpleMap[pos] == '#') continue;
            HashSet<(int, int)> newPath = new(path)
            {
                pos
            };
            if (pos == end)
            {
                if (score < lowestScore)
                {
                    lowestScore = score;
                    goodSeats = new(path);
                }
                else if (Part2 && score == lowestScore)
                {
                    goodSeats.UnionWith(path);
                    continue;
                }
                else
                    continue;
            }
            if (!visited.TryGetValue((pos, heading), out long bestSoFar))
            {
                visited.Add((pos, heading), score);
                bestSoFar = score;
            }
            else if (score > bestSoFar) continue;
            else if (score == bestSoFar && Part1) continue;
            visited[(pos, heading)] = score;
            (int dx, int dy) = DirectionsYDown[heading];
            (int x, int y) newPos = (pos.x + dx, pos.y + dy);
            q.Enqueue((newPos, heading, score + 1, newPath), score + newPath.Count + 1);
            q.Enqueue((pos, turns[(heading, 'R')], score + 1000, newPath), score + newPath.Count + 1000);
            q.Enqueue((pos, turns[(heading, 'L')], score + 1000, newPath), score + newPath.Count + 1000);
        } while (q.Count > 0);

        Output = (Part1 ? lowestScore : goodSeats.Count).ToString();
    }
}
