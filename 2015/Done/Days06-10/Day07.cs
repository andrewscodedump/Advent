namespace Advent2015;

public partial class Day07 : Advent.Day
{
    public override void DoWork()
    {
        Dictionary<string, string> instructions = new();
        Dictionary<string, ushort> values = new();

        if (WhichPart == 2) values.Add("b", 16076);
        foreach (string op in InputSplit)
            instructions.Add(op.Split(new string[] { " -> " },StringSplitOptions.RemoveEmptyEntries)[1], op.Split(new string[] { " -> " }, StringSplitOptions.RemoveEmptyEntries)[0]);

        Output = GetWireValue(values, instructions, "a").ToString();
    }

    private ushort GetWireValue(Dictionary<string, ushort> values, Dictionary<string, string> instructions, string key)
    {
        ushort value1 = 0, value2 = 0, valueOut = 0;
        string action, input1, input2;

        string[] bits = instructions[key].Split(' ');
        input1 = bits.Length == 2 ? bits[1] : bits[0];
        action = bits.Length == 3 ? bits[1] : bits.Length == 2 ? bits[0] : "EQUALS";
        input2 = bits.Length == 3 ? bits[2] : string.Empty;

        if (!string.IsNullOrEmpty(input1))
            if (!ushort.TryParse(input1, out value1))
                if (!values.TryGetValue(input1, out value1))
                    value1 = GetWireValue(values, instructions, input1);

        if (!string.IsNullOrEmpty(input2))
            if (!ushort.TryParse(input2, out value2))
                if (!values.TryGetValue(input2, out value2))
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
