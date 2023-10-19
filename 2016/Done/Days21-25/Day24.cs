namespace Advent2016;

public partial class Day24 : Advent.Day
{
    public override void DoWork()
    {
        Dictionary<Point, char> map = new();
        Dictionary<Pair, int> pairs = new();

        PopulateMap(map, out Point startPoint, out List<Point> targets);
        GetPairs(pairs, map, targets);

        Output = GetBestTotalDist(pairs, startPoint).ToString();
    }

    private void PopulateMap(Dictionary<Point, char> map, out Point startPoint, out List<Point> targets)
    {
        startPoint = new Point();
        targets = new List<Point>();
        for (int y = 0; y < InputSplit.Length; y++)
            for (int x = 0; x < InputSplit[y].Length; x++)
            {
                Point thePoint = new(x, y);
                char type = InputSplit[y][x];
                switch (type)
                {
                    case '#':
                    case '.':
                        break;
                    case '0':
                        startPoint = new Point(x, y);
                        targets.Add(new Point(x, y));
                        break;
                    default:
                        targets.Add(new Point(x, y));
                        break;
                }
                map.Add(thePoint, type);
            }
    }

    private static void GetPairs(Dictionary<Pair, int> pairs, Dictionary<Point, char> map, List<Point> targets)
    {
        foreach (Point from in targets)
            foreach (Point to in targets)
            {
                if (from == to) continue;
                Pair pair = new() { from = from, to = to };
                Pair reversePair = new() { from = to, to = from };
                if (!pairs.ContainsKey(pair) && !pairs.ContainsKey(reversePair))
                    pairs.Add(pair, GetPairDist(pair, map));
            }
    }

    private static int GetPairDist(Pair pair, Dictionary<Point, char> map)
    {
        int bestSteps = int.MaxValue;
        Point start = pair.from;
        Point end = pair.to;
        Dictionary<Point, int> bestMoves = new();

        Stack dfs = new();
        dfs.Push(new Move { Cell = start, Visits = new List<Point>() });

        while (dfs.Count > 0)
        {
            Move move = (Move)dfs.Pop();
            Point currPoint = new(move.Cell.X, move.Cell.Y);
            List<Point> visits = visits = new(move.Visits);

            if (!bestMoves.ContainsKey(currPoint))
                // We've not been to this cell already (in any iterations), so set this as the best so far
                bestMoves.Add(currPoint, visits.Count - 1);
            else if (visits.Count - 1 >= bestMoves[currPoint])
                // We've already been to this square in the same or fewer moves, so stop trying
                continue;
            else
                // Mark that this is the fewest steps to this point (so far)
                bestMoves[currPoint] = visits.Count - 1;

            // Add a step (and exit if we're past the best so far)
            visits.Add(currPoint);
            if (visits.Count - 1 > bestSteps)
                continue;

            // I've we've got to the target, check if it's better than the best so far, and start again
            if (currPoint == end)
            {
                bestSteps = Math.Min(bestSteps, visits.Count - 1);
                continue;
            }

            Dictionary<Point, decimal> availMoves = new();
            // Get all valid moves
            for (int x = 1; x >= -1; x--)
                for (int y = -1; y <= 1; y++)
                {
                    if (Math.Abs(x) + Math.Abs(y) != 1) continue;
                    Point nextPoint = new(currPoint.X + x, currPoint.Y + y);
                    if (map[nextPoint] != '#' && !visits.Contains(nextPoint))
                        availMoves.Add(nextPoint, GetMod(nextPoint, end));
                }

            if (availMoves.Count == 0)
                // No available moves - start again
                continue;
            if (availMoves.Count == 1)
                // Only one valid move - add it to the stack
                dfs.Push(new Move { Cell = availMoves.Keys.First(), Visits = visits });
            else
            {
                // Sort all the available moves from greatest to least crow fly distance to the target (LIFO, so this means we'll process the closest ones first)
                IOrderedEnumerable<KeyValuePair<Point, decimal>> sorted = availMoves.OrderByDescending(x => x.Value);
                foreach (KeyValuePair<Point, decimal> kvp in sorted)
                    dfs.Push(new Move { Cell = kvp.Key, Visits = visits });
            }
        }

        return bestSteps;
    }

    private int GetBestTotalDist(Dictionary<Pair, int> pairs, Point start)
    {
        int bestDist = int.MaxValue;
        Queue bfs = new();
        bfs.Enqueue(new Move { Cell = start, Visits = new List<Point> { start }, Distance = 0 });

        while (bfs.Count > 0)
        {
            Move move = (Move)bfs.Dequeue();
            Point currentCell = move.Cell;
            List<Point> visits = new(move.Visits) { currentCell };
            int distSoFar = move.Distance, newDist;
            bool foundOne = false;

            // Get all moves starting at the current point where we haven't been to the end point already
            foreach (Pair pair in pairs.Keys)
            {
                newDist = distSoFar + pairs[pair];
                if (pair.to == currentCell && !visits.Contains(pair.from) && newDist < bestDist)
                {
                    foundOne = true;
                    bfs.Enqueue(new Move { Cell = pair.from, Distance = newDist, Visits = visits });
                }
                if (pair.from == currentCell && !visits.Contains(pair.to) && newDist < bestDist)
                {
                    foundOne = true;
                    bfs.Enqueue(new Move { Cell = pair.to, Distance = newDist, Visits = visits });
                }
            }
            if (!foundOne)
            {
                // We're at the end
                if (Part2)
                {
                    // Add the leg to return us back to the start from where we've ended up
                    Pair returnLeg = new() { from = currentCell, to = start };
                    if (!pairs.ContainsKey(returnLeg))
                        returnLeg = new() { from = start, to = currentCell };
                    distSoFar += pairs[returnLeg];
                }
                // Check the distance
                bestDist = Math.Min(distSoFar, bestDist);
            }
        }

        return bestDist;
    }

    private static decimal GetMod(Point from, Point to) => ((from.X - to.X) * (from.X - to.X)) + ((from.Y - to.Y) * (from.Y - to.Y));

    private struct Pair { public Point from; public Point to; }
    private class Move
    {
        public Point Cell { get; set; }
        public List<Point> Visits { get; set; }
        public int Distance { get; set; }
    }
}
