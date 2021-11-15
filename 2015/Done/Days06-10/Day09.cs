namespace Advent2015;

public partial class Day09 : Advent.Day
{
    private struct Move { public string Town; public int DistanceSoFar; public List<string> Visited; }
    public override void DoWork()
    {
        List<string> towns = new();
        Dictionary<(string,string), int> distances = new();
        int shortestDistance = int.MaxValue;
        int longestDistance = 0;

        foreach (string set in InputSplit)
        {
            string[] parts = set.Split(' ');
            if (!towns.Contains(parts[0])) towns.Add(parts[0]);
            if (!towns.Contains(parts[2])) towns.Add(parts[2]);
            distances.Add((parts[0], parts[2]), int.Parse(parts[4]));
            distances.Add((parts[2], parts[0]), int.Parse(parts[4]));
        }

        Stack dfs = new();
        foreach(string town in towns)
            dfs.Push(new Move { Town = town, DistanceSoFar = 0, Visited = new List<string>() });

        do
        {
            Move move = (Move)dfs.Pop();
            List<string> visited = new(move.Visited) { move.Town };
            if (visited.Count == towns.Count)
            {
                shortestDistance = Math.Min(shortestDistance, move.DistanceSoFar);
                longestDistance = Math.Max(longestDistance, move.DistanceSoFar);
            }
            else
                foreach(string town in towns)
                {
                    if (town == move.Town) continue;
                    if (visited.Contains(town)) continue;
                    if (WhichPart == 1 && move.DistanceSoFar + distances[(move.Town, town)] > shortestDistance) continue;
                    dfs.Push(new Move { Town = town, DistanceSoFar = move.DistanceSoFar + distances[(move.Town, town)], Visited = visited });
                }
        } while (dfs.Count > 0);
        Output = (WhichPart == 1 ? shortestDistance : longestDistance).ToString();
    }
}
