namespace Advent2020;

public partial class Day08 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs

        int result;
        Dictionary<int, (string, int)> program = new();

        for (int i = 0; i < InputSplit.Length; i++)
            program.Add(i, (InputSplit[i].Split(' ')[0], int.Parse(InputSplit[i].Split(' ')[1])));

        #endregion Setup Variables and Parse Inputs

        if (WhichPart == 1) FindLoop(program, out result); else result = FixProgram(program);
        Output = result.ToString();
    }

    private static int FixProgram(Dictionary<int, (string, int)> program)
    {
        for (int i = 0; i < program.Count; i++)
        {
            Dictionary<int, (string, int)> modifiedProgram = new(program);
            (string instr, int param) = program[i];
            if (instr == "nop") modifiedProgram[i] = ("jmp", param);
            else if (instr == "jmp") modifiedProgram[i] = ("nop", param);
            if (!FindLoop(modifiedProgram, out int acc)) return acc;
        }
        return 0;
    }

    private static bool FindLoop(Dictionary<int, (string, int)> program, out int acc)
    {
        int next = 0;
        acc = 0;
        HashSet<int> done = new();
        do
        {
            done.Add(next);
            (next, acc) = Exec(next, acc, program[next]);
        } while (!done.Contains(next) && next < program.Count);
        return next < program.Count;
    }

    private static (int, int) Exec(int next, int acc, (string cmd, int param) instr)
    {
        switch (instr.cmd)
        {
            case "nop": next++; break;
            case "acc": acc += instr.param; next++; break;
            case "jmp": next += instr.param; break;
        }
        return (next, acc);
    }
}
