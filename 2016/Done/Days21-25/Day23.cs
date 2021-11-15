namespace Advent2016;

public partial class Day23 : Advent.Day
{
    public override void DoWork()
    {
        int pos = 0;
        Dictionary<string, int> registers = new() { { "a", 0 }, { "b", 0 }, { "c", WhichPart == 2 ? 1 : 0 }, { "d", 0 } };
        do
        {
            string instr = InputSplit[pos];
            string[] parts = instr.Split(' ');
            int test;
            bool isNumber;
            switch (parts[0])
            {
                case "cpy":
                    registers[parts[2]] = int.TryParse(parts[1], out _) ? int.Parse(parts[1]) : registers[parts[1]];
                    break;
                case "inc":
                    registers[parts[1]]++;
                    break;
                case "dec":
                    registers[parts[1]]--;
                    break;
                case "jnz":
                    isNumber = int.TryParse(parts[1], out test);
                    if ((isNumber && test != 0) || (!isNumber && registers[parts[1]] != 0))
                        pos += int.Parse(parts[2]) - 1;
                    break;
                case "tgl":
                    isNumber = int.TryParse(parts[1], out test);
                    if ((isNumber && test != 0) || (!isNumber && registers[parts[1]] != 0))
                        pos += int.Parse(parts[2]) - 1;
                    break;
                default:
                    break;
            }
            pos++;

        } while (pos < InputSplit.Length);

        Output = registers["a"].ToString();
    }
}
