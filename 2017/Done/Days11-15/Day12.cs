namespace Advent2017;

public partial class Day12 : Advent.Day
{
    public override void DoWork()
    {
        Dictionary<string, string[]> pipes = new();
        List<string> foundPrograms = new();
        int numberInZeroGroup = 0, numberOfGroups = 0;
        string groupStart = "0";

        foreach (string pipe in InputSplit)
            pipes.Add(pipe.Split(new string[] { " <-> " }, StringSplitOptions.RemoveEmptyEntries)[0], pipe.Split(" <-> ")[1].Split(", "));

        while (!string.IsNullOrEmpty(groupStart))
        {
            numberOfGroups++;
            AddProgs(groupStart, pipes, foundPrograms);
            if (groupStart == "0")
                numberInZeroGroup = foundPrograms.Count;

            groupStart = "";
            foreach (string program in pipes.Keys)
                if (!foundPrograms.Contains(program))
                {
                    groupStart = program;
                    break;
                }
        }
        Output = WhichPart == 1 ? numberInZeroGroup.ToString() : numberOfGroups.ToString();
    }

    private void AddProgs(string key, Dictionary<string, string[]> pipes, List<string> found)
    {
        found.Add(key);
        foreach (string program in pipes[key])
            if (!found.Contains(program))
                AddProgs(program, pipes, found);
    }
}
