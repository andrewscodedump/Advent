namespace Advent2015;

public partial class Day07 : Advent.Day
{
    public override void DoWork()
    {
        Dictionary<string, string> instructions = [];
        Dictionary<string, ushort> values = [];

        if (Part2) values.Add("b", 16076);
        foreach (string op in Inputs)
        {
            string[] bits = op.Split(" -> ", StringSplitOptions.RemoveEmptyEntries);
            instructions.Add(bits[1], bits[0]);
        }

        Output = GetWireValue(values, instructions, "a").ToString();
    }

    private static ushort GetWireValue(Dictionary<string, ushort> values, Dictionary<string, string> instructions, string key)
    {
        ushort value1 = 0, value2 = 0, valueOut = 0;
        string action, input1, input2;

        string[] bits = instructions[key].Split(' ');
        input1 = bits.Length == 2 ? bits[1] : bits[0];
        action = bits.Length == 3 ? bits[1] : bits.Length == 2 ? bits[0] : "EQUALS";
        input2 = bits.Length == 3 ? bits[2] : string.Empty;

        if (!string.IsNullOrEmpty(input1)
        && !ushort.TryParse(input1, out value1)
        && !values.TryGetValue(input1, out value1))
            value1 = GetWireValue(values, instructions, input1);

        if (!string.IsNullOrEmpty(input2)
        && !ushort.TryParse(input2, out value2)
        && !values.TryGetValue(input2, out value2))
            value2 = GetWireValue(values, instructions, input2);

        switch (action)
        {
            case "LSHIFT": valueOut = (ushort)(value1 << value2); break;
            case "RSHIFT": valueOut = (ushort)(value1 >> value2); break;
            case "AND": valueOut = (ushort)(value1 & value2); break;
            case "OR": valueOut = (ushort)(value1 | value2); break;
            case "EQUALS": valueOut = value1; break;
            case "NOT": valueOut = (ushort)~value1; break;
            default:
                break;
        }
        values[key] = Math.Max(valueOut, (ushort)0);
        return values[key];
    }
}
