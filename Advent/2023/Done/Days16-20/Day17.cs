namespace Advent2023;

public partial class Day17 : Advent.Day
{
    readonly Dictionary<(int, int), char> directions = new() { { (1, 0), 'R' }, { (-1, 0), 'L' }, { (0, 1), 'D' }, { (0, -1), 'U' } };

    public override void DoWork()
    {
        PopulateMapFromInputWithBorders('0', out int width, out int height);
        Dictionary<(int, int), int> map = SimpleMap.ToDictionary(p => p.Key, p => p.Value - 48);
        Dictionary<(int, int, string), int> bestScores = [];
        PriorityQueue <(int x, int y, int dx, int dy, int score, string path), int> queue = new();
        int bestScore = int.MaxValue;
        queue.Enqueue((0, 0, 0, 0, 0, ""), 0);

        do
        {
            (int x, int y, int cdx, int cdy, int totalScore, string path) = queue.Dequeue();
            if (x == width - 1 && y == height - 1)
            {
                if (Part2 && path[^4..].Distinct().Count() != 1) continue;
                if (totalScore < bestScore)
                    bestScore = totalScore;
                bestScore = Math.Min(bestScore, totalScore);
                continue;
            }

            foreach ((int dx, int dy) in DirectNeighbours)
            {
                (int nx, int ny) = (x + dx, y + dy);
                // Can't go out of range
                if (map[(nx, ny)] == 0) continue;
                // Can't go backwards
                if (dx == -cdx && dy == -cdy) continue;
                // Can't go more than three in a line
                if (!BuildRecentPath(path, dx, dy, out string newPath, Part1)) continue;
                int newScore = totalScore + map[(nx, ny)];
                // Don't go past best score so far
                if (newScore >= bestScore) continue;
                // Don't go past best score for new position and recent moves
                int historyLength = Part1 ? 3 : 10;
                string history = newPath.Length <= historyLength ? newPath : newPath[^historyLength..];
                if (bestScores.ContainsKey((nx, ny, history)) && newScore >= bestScores[(nx, ny, history)]) continue;
                bestScores[(nx, ny, history)] = newScore;
                // Naive priority = distance to end plus score (weighted towards distance)
                int priority = width - nx + height - ny + (1000 * newScore);
                queue.Enqueue((nx, ny, dx, dy, newScore, newPath), priority);
            }
        } while (queue.Count > 0);

        Output = bestScore.ToString();
    }

    bool BuildRecentPath(string pathIn, int dx, int dy, out string pathOut, bool part1)
    {
        pathOut = pathIn + directions[(dx, dy)];
        if (part1)
        {
            // Must turn after 3
            if (pathOut.Length < 4) return true;
            if (pathOut[^4..].Distinct().Count() == 1) return false;
            return true;
        }
        else
        {
            // Can't turn before 4, must turn after 10
            if (pathOut.Length < 5) return pathOut.Distinct().Count() == 1;
            // We're trying to turn, and haven't had 4 in a row yet
            if(directions[(dx, dy)] != pathIn[^1] && pathIn[^4..].Distinct().Count() !=1) return false;
            // Between 5 and 10 moves - OK
            if (pathOut.Length < 11) return true;
            // We're trying to go straight ahead, but we've already had 10 in a row
            if (pathOut[^11..].Distinct().Count() == 1) return false;
            return true;
        }
    }
}
