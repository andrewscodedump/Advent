namespace Advent2024;

public partial class Day18 : Advent.Day
{
    public override void DoWork()
    {
        HashSet<(int, int)> corrupted = [];
        int nextBlock = 0, initialDrop = TestMode ? 12 : 1024;
        for (; nextBlock < initialDrop; nextBlock++) corrupted.Add(((int)InputNumbers[nextBlock][0], (int)InputNumbers[nextBlock][1]));
        if (Part1) Output = GetBestPath(corrupted).ToString();
        else
        {
            (int x, int y) pos;
            do
            {
                nextBlock++;
                (pos.x, pos.y) = ((int)InputNumbers[nextBlock][0], (int)InputNumbers[nextBlock][1]);
                corrupted.Add((pos.x, pos.y));
            } while (GetBestPath(corrupted) != int.MaxValue);
            Output = $"{pos.x},{pos.y}";
        }
    }

    private int GetBestPath(HashSet<(int, int)> corrupted)
    {
        Dictionary<(int, int), int> visited = new() { { (0, 0), 0 } };
        int size = TestMode ? 6 : 70, shortest = int.MaxValue;
        Queue<((int, int), int)> q = new([((1, 0), 1), ((0, 1), 1)]);
        do
        {
            ((int x, int y) pos, int steps) = q.Dequeue();
            if (steps >= shortest) continue;
            if (pos == (size, size)) { shortest = steps; continue; }
            foreach ((int dx, int dy) in DirectNeighbours)
            {
                (int x, int y) newPos = (pos.x + dx, pos.y + dy);
                if (newPos.x < 0 || newPos.y < 0 || newPos.x > size || newPos.y > size) continue;
                if (corrupted.Contains(newPos)) continue;
                if (!visited.TryGetValue(newPos, out int best)) visited.Add(newPos, steps + 1);
                else if (steps + 1 < best) visited[newPos] = Math.Min(steps + 1, best);
                else continue;
                q.Enqueue((newPos, steps + 1));
            }
        } while (q.Count > 0);
        return shortest;
    }
}
