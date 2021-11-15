namespace Advent2015;

public partial class Day23 : Advent.Day
{
    public override void DoWork()
    {
        Dictionary<string, int> registers = new() { { "a", Math.Abs(1 - WhichPart) }, { "b", 0 } };

        int curPos = 0;
        string[] instructions = Input.Replace(",", "").Split(';');

        do
        {
            curPos = RunProgram(instructions[curPos], curPos, ref registers);
        } while (curPos < instructions.Length && curPos >= 0);

        Output = registers["b"].ToString();
    }

    private static int RunProgram(string instr, int curPos, ref Dictionary<string, int> registers)
    {
        string[] bits = instr.Split(' ');
        string register;
        int offset = 1;

        switch (bits[0])
        {
            case "hlf":
                register = bits[1];
                registers[register] /= 2;
                break;
            case "tpl":
                register = bits[1];
                registers[register] *= 3;
                break;
            case "inc":
                register = bits[1];
                registers[register]++;
                break;
            case "jmp":
                offset = int.Parse(bits[1]);
                break;
            case "jie":
                register = bits[1];
                if (registers[register] % 2 == 0)
                {
                    offset = int.Parse(bits[2]);
                }
                break;
            case "jio":
                register = bits[1];
                if (registers[register] == 1)
                {
                    offset = int.Parse(bits[2]);
                }
                break;
            default:
                break;
        }
        return curPos + offset;
    }
}
