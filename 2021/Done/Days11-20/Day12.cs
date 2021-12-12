namespace Advent2021;

public partial class Day12 : Advent.Day
{
    public override void DoWork()
    {
        Dictionary<string, List<string>> options = new();
        Queue<(string, List<string>, HashSet<string>, bool)> explore = new();
        List<List<string>> paths = new();

        foreach(string input in InputSplit)
        {
            string start = input.Split('-')[0], end = input.Split('-')[1];
            if (!options.ContainsKey(start)) options[start] = new();
            if (!options.ContainsKey(end)) options[end] = new();
            if (start != "end" && end != "start") options[start].Add(end);
            if (start != "start" && end != "end") options[end].Add(start);
        }
        explore.Enqueue(("start", new(), new(), false));

        do
        {
            (string pos, List<string> path, HashSet<string> visited, bool twoVisits) = explore.Dequeue();
            path.Add(pos);
            if (pos == "end")
            {
                paths.Add(path);
                continue;
            }
            visited.Add(pos);
            foreach (string nextPos in options[pos])
            {
                bool nextSmall = nextPos[0] > 96 && nextPos!= "end", nextTwo = twoVisits;
                if (visited.Contains(nextPos) && nextSmall)
                {
                    if (WhichPart == 1 || twoVisits) continue;
                    nextTwo = true;
                }
                explore.Enqueue((nextPos, new(path), new(visited), nextTwo));
            }

        } while (explore.Count > 0);
        
        Output = paths.Count.ToString();
    }
}
