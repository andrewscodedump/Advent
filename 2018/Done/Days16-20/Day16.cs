namespace Advent2018;

public partial class Day16 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs
        List<(int[] before, int op, int[] args, int[] after)> tests = new();
        List<(int, int[])> code = new();
        List<string> ops = new() { "addr", "addi", "mulr", "muli", "banr", "bani", "borr", "bori", "setr", "seti", "gtir", "gtri", "gtrr", "eqir", "eqri", "eqrr" };
        Dictionary<int, List<string>> options = new();
        Dictionary<int, string> knownOps = new();
        int[] regs = new int[] { 0, 0, 0, 0 };
        int result = 0;

        for (int i = 0; i < InputSplit.Length; i++)
        {
            string[] bits = InputSplit[i].Split(new char[] { ' ', '[', ']', ',', '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (bits[0] == "Before:")
                tests.Add((new int[] { int.Parse(bits[1]), int.Parse(bits[2]), int.Parse(bits[3]), int.Parse(bits[4]) }, int.Parse(bits[5]), new int[] { int.Parse(bits[6]), int.Parse(bits[7]), int.Parse(bits[8]) }, new int[] { int.Parse(bits[10]), int.Parse(bits[11]), int.Parse(bits[12]), int.Parse(bits[13]) }));
            else
                code.Add((int.Parse(bits[0]), new int[3] { int.Parse(bits[1]), int.Parse(bits[2]), int.Parse(bits[3]) }));
        }
        #endregion Setup Variables and Parse Inputs
        foreach ((int[] before, int op, int[] args, int[] after) test in tests)
        {
            int valid = 0;
            foreach (string op in ops)
            {

            if (InterpretCode(op, test.before, test.args).SequenceEqual(test.after))
                {
                    if (!options.ContainsKey(test.op))
                        options.Add(test.op, new List<string> { op });
                    else if (!options[test.op].Contains(op))
                        options[test.op].Add(op);
                    valid++;
                }
            }
            result += valid >= 3 ? 1 : 0;
        }
        if (Part2)
        {
            do
                for (int i = 0; i < 16; i++)
                    if (options.ContainsKey(i) && options[i].Count == 1)
                    {
                        string op = options[i][0];
                        knownOps.Add(i, op);
                        options.Remove(i);
                        foreach (int key in options.Keys)
                            if (options[key].Contains(op))
                                options[key].Remove(op);
                    }
            while (options.Count != 0);

            foreach ((int op, int[] args) in code)
                regs = InterpretCode(knownOps[op], regs, args);
            result = regs[0];
        }
        Output = result.ToString();
    }
}
