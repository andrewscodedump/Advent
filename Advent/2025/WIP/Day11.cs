namespace Advent2025;

public class Day11 : Advent.Day
{
    public override void DoWork()
    {
        GenerateDotFile();

        long paths = 0;
        Dictionary<string, HashSet<string>> branches = [];
        foreach (string line in Inputs)
        {
            string[] bits = line.Split([' ', ':'], StringSplitOptions.RemoveEmptyEntries);
            branches[bits[0]] = [.. bits[1..]];
        }

        if (WhichPart == 1)
            paths = CountPaths(branches, "you", "out", []);
        else
        {
            /* Key nodes
                "svr"
                    "dsf", "xcv", "jpb", "ltb", "vbm"
                    "igi", "acq", "yrn", "mjr"
                ("fft")
                    "thz", "nse", "crg"
                    "kfe", "gmk", "boy", "nnn"
                    "hpy", "qrw", "jjm", "qzh", "jbj"
                ("dac")
                    "ywc", "fpm", "mkd", "dtl", "you"
                "out"
            */
            CountPaths(branches, ["svr"], ["igi", "acq", "yrn", "mjr"]);
            CountPaths(branches, ["igi", "acq", "yrn", "mjr"], "fft", ["thz", "nse", "crg"]);
            CountPaths(branches, ["fft"], ["thz", "nse", "crg"]);
            CountPaths(branches, ["thz", "nse", "crg"], ["kfe", "gmk", "boy", "nnn"]);
            CountPaths(branches, ["kfe", "gmk", "boy", "nnn"], ["hpy", "qrw", "jjm", "qzh", "jbj"]);
            CountPaths(branches, ["hpy", "qrw", "jjm", "qzh", "jbj"], "dac", ["ywc", "fpm", "mkd", "dtl", "you"]);
            CountPaths(branches, ["dac"], ["ywc", "fpm", "mkd", "dtl", "you"]);
            CountPaths(branches, ["ywc", "fpm", "mkd", "dtl", "you"], ["out"]);
        }
        Output = paths;
    }

    private static void CountPaths(Dictionary<string, HashSet<string>> branches, string[] froms, string to, string[] guard)
    {
        foreach (string from in froms)
            CountPaths(branches, from, to, guard);
    }

    private static void CountPaths(Dictionary<string, HashSet<string>> branches, string[] froms, string[] tos)
    {
        foreach (string from in froms)
            foreach (string to in tos)
                CountPaths(branches, from, to, tos);
    }

    private static long CountPaths(Dictionary<string, HashSet<string>> branches, string from, string to, string[] guard)
    {
        int paths = 0;

        Queue<string> q = [];
        q.Enqueue(from);
        do
        {
            string node = q.Dequeue();
            if (node == to)
            {
                paths++;
                continue;
            }
            else if (guard.Contains(node)) continue;
            else if (!branches.TryGetValue(node, out HashSet<string> value)) continue;
            else
            {
                foreach (string output in value)
                    q.Enqueue(output);
            }
        } while (q.Count > 0);

        Debug.Print($"{from} - {to} : {paths}");
        return paths;
    }

    private void GenerateDotFile()
    {
        HashSet<string> components = [];
        List<(string, string)> connections = [];
        StringBuilder sb = new("strict graph\r\n{\r\n");
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
        Debug.Print(sb.ToString());
    }
}
