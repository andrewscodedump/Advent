namespace Advent2017;

public partial class Day23 : Advent.Day
{
    public override void DoWork()
    {
        long numMul = 0;
        int[] currPos = new int[] { 0, 0 };
        long oldB = 0; long oldC = 0; long oldD = 0;
        string instruction;
        string[] instructions = InputSplit;
        Dictionary<string, long> registers = new() { { "a", WhichPart - 1 }, { "b", 0 }, { "c", 0 }, { "d", 0 }, { "e", 0 }, { "f", 0 }, { "g", 0 }, { "h", 0 } };

        if (WhichPart == 1)
            do
            {
                instruction = instructions[currPos[0]];
                DoOperation(registers, instruction, ref currPos[0]);
                if (instruction.StartsWith("mul")) numMul++;
                if (oldB != registers["b"] || oldC != registers["c"] || oldD != registers["d"]) (oldB, oldC, oldD) = (registers["b"], registers["c"], registers["d"]);
            } while (!(currPos[0] >= instructions.Length || currPos[0] < 0));

        else
        {
            // Analyzing what the code's actually doing, it's setting b and c and then counting the number of factorisable values between them (inclusive), which boils down to this:
            registers["b"] = 107900; registers["c"] = 124900;
            for (long test = registers["b"]; test <= registers["c"]; test += 17)
            {
                for (long factor = 2; factor <= Math.Sqrt(test); factor++)
                    if (test % factor == 0)
                    {
                        registers["h"]++;
                        break;
                    }
            }
            #region C# Interpretation of assembly code
            /*
            // Trying to run the interpreted code is waaaaaaay too slow, so translating into C# (via pseudocode) gives the following.
            // Still too slow, so figuring out what it's actually doing gives the above
            do
            {
                registers["b"] = 107900;
                registers["c"] = 124900;
                registers["f"] = 1;
                registers["d"] = 2;
                do
                {
                    registers["e"] = 2;
                    do
                    {
                        if (registers["d"] * registers["e"] == registers["b"])
                        {
                            registers["f"] = 0;
                            registers["d"] = registers["b"]-1;
                            registers["e"] = registers["b"]-1;
                            break;
                        }
                        registers["e"]++;
                    } while (registers["e"] != registers["b"]);
                    registers["d"]++;
                } while (registers["d"] != registers["b"]);
                if (registers["f"] == 0)
                    registers["h"]++;
                if (registers["b"] == registers["c"])
                    break;
                registers["b"] += 17;
            } while (true);
            */
            #endregion C# Interpretation of assembly code 
        }

        Output = (WhichPart == 1 ? numMul : registers["h"]).ToString();
    }

    private static void DoOperation(Dictionary<string, long> registers, string instruction, ref int position)
    {
        string[] words = instruction.Split(' ');
        DoOperation(registers, words[0], words[1], words.Length == 3 ? words[2] : string.Empty, ref position);
    }

    private static void DoOperation(Dictionary<string, long> registers, string operation, string param1, string param2, ref int position)
    {
        position++;
        switch (operation)
        {
            case "sub": registers[param1] = GetValue(param1, registers) - GetValue(param2, registers); break;
            case "mul": registers[param1] = GetValue(param1, registers) * GetValue(param2, registers); break;
            case "set": registers[param1] = GetValue(param2, registers); break;
            case "jnz": if (GetValue(param1, registers) != 0) { position += (int)GetValue(param2, registers) - 1; } break;

            default: break;
        }
    }

    private static long GetValue(string registerOrValue, Dictionary<string, long> registers)
    {
        if (!long.TryParse(registerOrValue, out long output))
        {
            if (!registers.ContainsKey(registerOrValue))
                registers.Add(registerOrValue, 0);
            output = registers[registerOrValue];
        }
        return output;
    }
}
