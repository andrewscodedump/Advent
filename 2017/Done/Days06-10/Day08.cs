namespace Advent2017;

public partial class Day08 : Advent.Day
{
    public override void DoWork()
    {
        Dictionary<string, int> registers = new();
        int maxRunVal = 0;

        foreach (string instruction in Inputs)
            maxRunVal = Math.Max(DoOperation(registers, instruction), maxRunVal);

        Output = (Part1 ? registers.Values.Max() : maxRunVal).ToString();
    }

    private static int DoOperation(Dictionary<string, int> registers, string instruction)
    {
        string[] words = instruction.Split(' ');
        string destReg = words[0];
        if (!registers.TryGetValue(destReg, out _))
            registers.Add(destReg, 0);
        if (!registers.TryGetValue(words[4], out int sourceCurr))
            registers.Add(words[4], 0);

        registers[destReg] += GetValue(sourceCurr, words[5], int.Parse(words[6]), int.Parse(words[2]), words[1]);

        return registers[destReg];
    }

    private static int GetValue(int source, string op, int comparator, int value, string action)
    {
        return op switch
        {
            "==" => source == comparator ? action == "dec" ? -value : value : 0,
            "!=" => source != comparator ? action == "dec" ? -value : value : 0,
            "<" => source < comparator ? action == "dec" ? -value : value : 0,
            ">" => source > comparator ? action == "dec" ? -value : value : 0,
            "<=" => source <= comparator ? action == "dec" ? -value : value : 0,
            ">=" => source >= comparator ? action == "dec" ? -value : value : 0,
            _ => 0,
        };
    }

}
