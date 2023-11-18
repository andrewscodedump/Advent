namespace Advent2017;

public partial class Day18 : Advent.Day
{
    public override void DoWork()
    {
        long soundPlayed = 0;
        int[] currPos = new int[] { 0, 0 };
        long result, received = 0;
        Queue<long>[] queues = new Queue<long>[2] { new(), new() };
        string instruction;
        string[] instructions = Inputs;
        int numSends = 0;
        bool[] waiting = new bool[] { false, false };
        Dictionary<string, long>[] registers = new Dictionary<string, long>[2] { new() { { "p", 0 } }, new() { { "p", 1 } } };

        if (Part1)
        {
            do
            {
                instruction = instructions[currPos[0]];
                result = DoOperation(registers[0], instruction, ref currPos[0], queues[0], queues[0], ref waiting[0], WhichPart);
                if (instruction.StartsWith("snd"))
                    soundPlayed = result;
                if (instruction.StartsWith("rcv") && result != 0)
                    received = soundPlayed;
            } while (!(currPos[0] >= instructions.Length || (instruction.StartsWith("rcv") && result != 0)));
        }
        else
        {
            do
            {
                for (int i = 0; i < 2; i++)
                {
                    if (currPos[i] < instructions.Length)
                    {
                        instruction = instructions[currPos[i]];
                        if (i == 1 && instruction.StartsWith("snd")) numSends++;
                        DoOperation(registers[i], instruction, ref currPos[i], queues[i], queues[Math.Abs(i - 1)], ref waiting[i], WhichPart);
                    }
                }

            } while (currPos[0] < instructions.Length && currPos[1] < instructions.Length && !(waiting[0] && waiting[1]));
        }

        Output = (Part1 ? received : numSends).ToString();
    }

    private static long DoOperation(Dictionary<string, long> registers, string instruction, ref int position, Queue<long> myQueue, Queue<long> otherQueue, ref bool waiting, int whichPart)
    {
        string[] words = instruction.Split(' ');
        string operation = words[0];
        string param1 = words[1];
        string param2 = words.Length == 3 ? words[2] : string.Empty;

        return DoOperation(registers, operation, param1, param2, ref position, myQueue, otherQueue, ref waiting, whichPart);
    }

    private static long DoOperation(Dictionary<string, long> registers, string operation, string param1, string param2, ref int position, Queue<long> myQueue, Queue<long> otherQueue, ref bool waiting, int whichPart)
    {
        long output = 0;
        position++;
        switch (operation)
        {
            case "snd":
                switch (whichPart)
                {
                    case 1: output = GetValue(param1, registers); break;
                    case 2: myQueue.Enqueue(GetValue(param1, registers)); break;
                    default: break;
                }
                break;

            case "rcv":
                switch (whichPart)
                {
                    case 1: output = GetValue(param1, registers); break;
                    case 2:
                        if (otherQueue.Count == 0)
                        {
                            position--;
                            waiting = true;
                        }
                        else
                        {
                            registers[param1] = otherQueue.Dequeue();
                            waiting = false;
                        }
                        break;
                    default:
                        break;
                }
                break;

            case "add": registers[param1] = GetValue(param1, registers) + GetValue(param2, registers); break;
            case "mul": registers[param1] = GetValue(param1, registers) * GetValue(param2, registers); break;
            case "mod": registers[param1] = GetValue(param1, registers) % GetValue(param2, registers); break;
            case "set": registers[param1] = GetValue(param2, registers); break;
            case "jgz": if (GetValue(param1, registers) > 0) { position--; position += (int)GetValue(param2, registers); } break;
            default: break;
        }
        return output;
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
