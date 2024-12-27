using System.Linq;

namespace Advent2018;

public partial class Day16 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs
        List<(int[] before, int op, int[] args, int[] after)> tests = [];
        List<(int, int[])> code = [];
        List<string> ops = ["addr", "addi", "mulr", "muli", "banr", "bani", "borr", "bori", "setr", "seti", "gtir", "gtri", "gtrr", "eqir", "eqri", "eqrr"];
        Dictionary<int, List<string>> options = [];
        Dictionary<int, string> knownOps = [];
        int[] regs = [0, 0, 0, 0];
        int result = 0;
        char[] splitter = [' ', '[', ']', ',', '|'];

        for (int i = 0; i < Inputs.Length; i++)
        {
            string[] bits = Inputs[i].Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            if (bits[0] == "Before:")
                tests.Add((new int[] { int.Parse(bits[1]), int.Parse(bits[2]), int.Parse(bits[3]), int.Parse(bits[4]) }, int.Parse(bits[5]), new int[] { int.Parse(bits[6]), int.Parse(bits[7]), int.Parse(bits[8]) }, new int[] { int.Parse(bits[10]), int.Parse(bits[11]), int.Parse(bits[12]), int.Parse(bits[13]) }));
            else
                code.Add((int.Parse(bits[0]), [int.Parse(bits[1]), int.Parse(bits[2]), int.Parse(bits[3])]));
        }
        #endregion Setup Variables and Parse Inputs
        foreach ((int[] before, int op, int[] args, int[] after) test in tests)
        {
            int valid = 0;
            foreach (string op in ops.Where(op => InterpretCode(op, test.before, test.args).SequenceEqual(test.after)))
            {
                if (!options.TryGetValue(test.op, out List<string> value))
                    options.Add(test.op, [op]);
                else if (!value.Contains(op))
                    value.Add(op);
                valid++;
            }
            result += valid >= 3 ? 1 : 0;
        }
        if (Part2)
        {
            do
                for (int i = 0; i < 16; i++)
                    if (options.TryGetValue(i, out List<string> value) && value.Count == 1)
                    {
                        string op = value[0];
                        knownOps.Add(i, op);
                        options.Remove(i);
                        foreach (int key in options.Keys)
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
