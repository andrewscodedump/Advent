namespace Advent2023;

public partial class Day08 : Advent.Day
{
    public override void DoWork()
    {
        string directions = Inputs[0];
        long steps = 0;
        Dictionary<string, (string L, string R)> map = [];
        Dictionary<int, long> found = [];

        char[] separator = [' ', '=', ',', '(', ')'];
        foreach (string line in Inputs[2..])
        {
            string[] nodes = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            map[nodes[0]] = (nodes[1], nodes[2]);
        }
        string[] nextNodes = Part1 ? ["AAA"] : map.Keys.Where(k => k[^1] == 'A').ToArray();

        do
        {
            for (int i = 0; i < nextNodes.Length; i++)
            {
                if (found.ContainsKey(i)) continue;
                nextNodes[i] = directions[(int)steps % directions.Length] == 'L' ? map[nextNodes[i]].L : map[nextNodes[i]].R;
                if (nextNodes[i][^1] == 'Z') found[i] = steps + 1;
            }
            steps++;
        } while (found.Count != nextNodes.Length);

        Output = ArrayLCM([.. found.Values]).ToString();
    }

    private static long GCF(long a, long b)
    {
        while (b != 0)
            (a, b) = (b, a % b);
        return a;
    }

    private static long LCM(long a, long b)
    {
        long gcf = GCF(a, b);
        return gcf == 0 ? 0 : a / gcf * b;
    }

    private static long ArrayLCM(long[] inputs)
    {
        long result = 1;
        for (int i = 0; i < inputs.Length; i++)
            result = LCM(result, inputs[i]);
        return result;
    }


}
