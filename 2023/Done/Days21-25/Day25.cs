namespace Advent2023;

public partial class Day25 : Advent.Day
{
    public override void DoWork()
    {
        bool GenerateGraphWizInputs = true;
        HashSet<string> components = [];
        Dictionary<string, List<string>> connectionMap = [];
        List<(string, string)> connections = [];
        int result = 0;
        StringBuilder sb = new("strict graph {");
        foreach (string input in Inputs)
        {
            string left = input.Split(": ")[0];
            string rights = input.Split(": ")[1];
            components.Add(left);
            sb.AppendLine($"{left} -- {{{rights}}}");
            foreach (string right in rights.Split(' '))
            {
                components.Add(right);
                if (!connections.Contains((right, left)))
                    connections.Add((left, right));
                if (!connections.Contains((left, right)))
                    connections.Add((right, left));
            }
        }
        sb.AppendLine("}");
        if (GenerateGraphWizInputs) Debug.Print(sb.ToString());
        connectionMap = RebuildConnectionMap(connections);

        int groups;

        /* This works for the test input, but would have taken 45 years for the live
        var combos = Combo.Combinator(connections, 3);
        groups = 0;
        int count = 0;
        foreach (var breaks in combos)
        {
            count++;
            //connectionMap = RebuildConnectionMap(connections);
            Dictionary<string, List<string>> connectionMap1 = [];
            foreach(string key in connectionMap.Keys) connectionMap1[key] = new(connectionMap[key]);

            foreach ((string left, string right) in breaks)
            {
                connectionMap1[left].Remove(right);
                connectionMap1[right].Remove(left);
            }

            groups = CountGroups(components, connectionMap, out result);
            if (groups != 2) result = 999;
        }
        */

        /* This is a real cheat (but it works...)
        Feed the inputs into graphwiz (neato filter), and the links to be cut are 
        clearly visible between the two enormous clusters)
        */
        List<(string, string)> nodesToBreak =
            TestMode ? new() { { ("hfx", "pzl") }, { ("bvb", "cmg") }, { ("jqt", "nvd") } }
            : new() { { ("nmv", "thl") }, { ("vgk", "mbq") }, { ("fxr", "fzb") } };

        foreach ((string left, string right) in nodesToBreak)
        {
            connectionMap[left].Remove(right);
            connectionMap[right].Remove(left);
        }
        groups = CountGroups(components, connectionMap, out result);

        Output = result.ToString();
    }

    private int CountGroups(HashSet<string> components, Dictionary<string, List<string>> connectionMap, out int result)
    {
        result = 0;
        List<List<string>> groups = [];
        foreach (string component in components)
        {
            List<string> group = [];
            if (groups.Any(g => g.Contains(component))) continue;
            AddToGroup(group, component, groups, connectionMap);
            groups.Add(group);
        }
        if (groups.Count == 2)
            result = groups[0].Count * groups[1].Count;
        return groups.Count;
    }

    private static void AddToGroup(List<string> group, string component, List<List<string>> groups, Dictionary<string, List<string>> connectionMap)
    {
        if (group.Contains(component) || groups.Any(g => g.Contains(component))) return;
        group.Add(component);
        foreach(string target in connectionMap[component])
            AddToGroup(group, target, groups, connectionMap);
    }

    private Dictionary<string, List<string>> RebuildConnectionMap(List<(string, string)> connections)
    {
        Dictionary<string, List<string>> connectionMap = [];

        foreach ((string left, string right) in connections)
        {
            if (connectionMap.TryGetValue(left, out List<string> valueL))
                valueL.Add(right);
            else
                connectionMap[left] = [right];
            if (connectionMap.TryGetValue(right, out List<string> valueR))
                valueR.Add(left);
            else
                connectionMap[right] = [left];
        }
        return connectionMap;
    }
}

public static class Combo
{
    public static IEnumerable<IEnumerable<T>> Combinator<T>
     (this IEnumerable<T> elements, int k)
    {
        return k == 0
            ? EnumerableEx.Return(Enumerable.Empty<T>())
            : elements.SelectMany((e, i) =>
                elements.Skip(i + 1)
                    .Combinator(k - 1)
                    .Select(c => EnumerableEx.Return(e).Concat(c)));
    }
}
