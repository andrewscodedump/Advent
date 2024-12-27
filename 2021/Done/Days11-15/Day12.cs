namespace Advent2021;

public partial class Day12 : Advent.Day
{
    public override void DoWork()
    {
        Dictionary<string, HashSet<string>> options = [];
        Queue<(string, List<string>, HashSet<string>, bool)> explore = new();
        List<List<string>> paths = [];

        foreach (string input in Inputs)
        {
            string start = input.Split('-')[0], end = input.Split('-')[1];
            if (start == "start" || start == "end") start = start.ToUpper();
            if (end == "start" || end == "end") end = end.ToUpper();
            if (!options.ContainsKey(start)) options[start] = [];
            if (!options.ContainsKey(end)) options[end] = [];
            if (start != "END" && end != "START") options[start].Add(end);
            if (start != "START" && end != "END") options[end].Add(start);
        }
        explore.Enqueue(("START", [], [], false));

        do
        {
            (string pos, List<string> path, HashSet<string> visited, bool twoVisits) = explore.Dequeue();
            path.Add(pos);
            if (pos == "END")
            {
                paths.Add(path);
                continue;
            }
            visited.Add(pos);
            foreach (string nextPos in options[pos])
            {
                bool nextSmall = nextPos[0] > 96, nextTwo = twoVisits;
                if (visited.Contains(nextPos) && nextSmall)
                {
                    if (Part1 || twoVisits) continue;
                    nextTwo = true;
                }
                explore.Enqueue((nextPos, new(path), new(visited), nextTwo));
            }

        } while (explore.Count > 0);

        Output = paths.Count.ToString();
    }
}
