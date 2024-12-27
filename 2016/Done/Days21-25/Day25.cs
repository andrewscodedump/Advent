namespace Advent2016;

public partial class Day25 : Advent.Day
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
    public override void DoWork()
    {
        if (Part2 || TestMode) return;

        int pos = 0;
        int output;
        int currentNumber = 0;
        bool notValid = false;
        int currDigit = int.MaxValue;
        int numDigits = 0;

        Dictionary<string, int> registers = new() { { "a", 0 }, { "b", 0 }, { "c", 0 }, { "d", 0 } };
        do
        {
            string instr = Inputs[pos];
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
                        pos += (int.TryParse(parts[2], out _) ? int.Parse(parts[2]) : registers[parts[2]]) - 1;
                    break;
                case "out":
                    output = int.TryParse(parts[1], out _) ? int.Parse(parts[1]) : registers[parts[1]];
                    if (currDigit != int.MaxValue && (output == currDigit || (output != 0 && output != 1)))
                        notValid = true;
                    else
                    {
                        currDigit = output;
                        numDigits++;
                    }

                    break;
                case "tgl":
                    isNumber = int.TryParse(parts[1], out test);
                    if (!isNumber)
                        test = registers[parts[1]];

                    if (pos + test < Inputs.Length)
                    {
                        switch (Inputs[pos + test][..3])
                        {
                            case "cpy":
                                Inputs[pos + test] = "jnz" + Inputs[pos + test][3..];
                                break;
                            case "inc":
                                Inputs[pos + test] = "dec" + Inputs[pos + test][3..];
                                break;
                            case "jnz":
                                Inputs[pos + test] = "cpy" + Inputs[pos + test][3..];
                                break;
                            case "dec":
                            case "tgl":
                            case "out":
                                Inputs[pos + test] = "inc" + Inputs[pos + test][3..];
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }
            if (notValid)
            {
                currentNumber++;
                ResetRegisters(registers);
                pos = 0;
                registers["a"] = currentNumber;
                notValid = false;
                currDigit = int.MaxValue;
                numDigits = 0;
                continue;
            }
            if (numDigits == 1000)
                break;
            pos++;

        } while (pos < Inputs.Length);

        Output = currentNumber.ToString();
    }

    private static void ResetRegisters(Dictionary<string, int> registers)
    {
        registers["a"] = 0;
        registers["b"] = 0;
        registers["c"] = 0;
        registers["d"] = 0;
    }
}
