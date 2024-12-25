namespace Advent2024;

public partial class Day24 : Advent.Day
{
    public override void DoWork()
    {
        long result = 0;
        Dictionary<string, bool> values = [];
        List<string[]> gates = [];
        string[] separator = [" ", "-", ">"];

        foreach (string s in Inputs.Where(i => i.Contains(':')))
            values[s.Split(": ")[0]] = s.Split(": ")[1] == "1";
        foreach (string s in Inputs.Where(i => i.Contains('-')))
        {
            gates.Add(s.Split(separator, StringSplitOptions.RemoveEmptyEntries));
        }

        bool didSomething = false;
        do
        {
            for (int i = gates.Count - 1; i >= 0; i--)
            {
                string[] gate = gates[i];
                if (values.TryGetValue(gate[0], out bool i1) && values.TryGetValue(gate[2], out bool i2))
                {
                    values[gate[3]] = gate[1] switch
                    {
                        "XOR" => i1 ^ i2,
                        "AND" => i1 && i2,
                        "OR" => i1 || i2,
                        _ => throw (new InvalidOperationException()),

                    };
                    if (gate[3].StartsWith('z') && values[gate[3]])
                        result += (long)Math.Pow(2, long.Parse(gate[3][1..].ToString()));
                    gates.RemoveAt(i);
                    didSomething = true;
                }
            }

        } while (didSomething && gates.Count > 0);

        Output = result.ToString();
    }
}
