namespace Advent2021;

public partial class Day15 : Advent.Day
{
    public override void DoWork()
    {
        Dictionary<(int, int), int> map = new(), shortestPaths = new() { { (0, 0), 0 } };
        PriorityQueue<((int, int), HashSet<(int,int)>, int), int> explore = new();
        int finalScore = int.MaxValue, width = Inputs.Length;
        int multiplier = Part1 ? 1 : 5;
        (int x, int y) endPos = ((width * multiplier) - 1, (width * multiplier) - 1);
        List<(int, int)> Offsets = new() { (0, 1), (1, 0), (0, -1), (-1, 0) };

        for (int y = 0; y < width; y++)
            for (int i = 0; i < multiplier; i++)
                for (int j = 0; j < multiplier; j++)
                    for (int x = 0; x < width; x++)
                    {
                        int val = int.Parse(Inputs[y][x].ToString());
                        map[(x + (j * width), y + (i * width))] = val + i + j - ((val + i + j - 1) / 9 * 9);
                    }
        explore.Enqueue(((0, 0), new(), 0), 0);

        do
        {
            ((int x, int y) pos, HashSet<(int, int)> visited, int score) = explore.Dequeue();

            if (pos == endPos) finalScore = score;
            foreach ((int dx, int dy) in Offsets)
            {
                (int x, int y) newPos = (pos.x + dx, pos.y + dy);
                if (!map.ContainsKey(newPos) || visited.Contains(newPos)) continue;
                HashSet<(int, int)> newVisited = new(visited)
                {
                    newPos
                };
                int newScore = score + map[newPos];
                if (newScore > finalScore || (shortestPaths.ContainsKey(newPos) && newScore >= shortestPaths[newPos])) continue;
                shortestPaths[newPos] = newScore;
                explore.Enqueue((newPos, newVisited, newScore), endPos.x - newPos.x + endPos.y - newPos.y + newScore);
            }
        } while (explore.Count > 0);

        Output = finalScore.ToString();
    }
}
