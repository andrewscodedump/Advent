using System.Security.Cryptography;

namespace Advent;

public abstract class Day
{
    #region Constructors and Declarations

    protected Day() : this(false, 1, string.Empty) { }
    protected Day(bool testMode, int whichPart, string input) : this(testMode, whichPart, input, DayBatchStatus.Available) { }
    protected Day(bool testMode, int whichPart, string input, DayBatchStatus batchStatus)
    {
        Inputs = new List<string>();
        Expecteds = new List<string>();
        Output = string.Empty;
        TestMode = testMode;
        WhichPart = whichPart;
        this.input = input;
        Rand = new Random();
        MD5 = MD5.Create();
        BatchStatus = batchStatus;
    }
    public DayStatus Status { get; set; }
    protected MD5 MD5 { get; private set; }
    public bool TestMode { get; set; }
    public int WhichPart { get; set; }
    public abstract void DoWork();
    public DayBatchStatus BatchStatus { get; set; }
    public bool BatchRun { get; set; }
    public string Output { get; set; }
    public int CurrentInput { get; set; }

    private string input;
    public string Input
    {
        get => Inputs.Count > 0 ? Inputs[CurrentInput] : input;
        set => input = value;
    }
    public List<string> Inputs { get; set; }
    protected string[] InputSplit => (string.IsNullOrEmpty(input) ? Inputs[0] : input).Split(';', StringSplitOptions.RemoveEmptyEntries);
    protected int[] InputSplitInt => Array.ConvertAll(InputSplit, int.Parse);
    protected string[] InputSplitC => (string.IsNullOrEmpty(input) ? Inputs[0] : input).Split(',');
    protected int[] InputSplitCInt => Array.ConvertAll(InputSplitC, int.Parse);
    protected long[] InputSplitCLong => Array.ConvertAll(InputSplitC, long.Parse);
    protected string[] InputSplitter(char delimiter) => InputSplitter(new char[] { delimiter });
    protected string[] InputSplitter(char[] delimiters) => Input.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

    private string expected;
    public string Expected { get => Expecteds.Count > 0 ? Expecteds[0] : expected; set => expected = value; }
    public List<string> Expecteds { get; set; }

#pragma warning disable IDE0044 // Add readonly modifier
    private Dictionary<string, DateTime> logs = new();
#pragma warning restore IDE0044 // Add readonly modifier

    #endregion Constructors

    #region Common Objects

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
    protected static Random Rand;
    public enum DayStatus { NotStarted, Running, Successful, Failed, Unknown };
    public enum DayBatchStatus { Available, NotDoneYet, Performance, NonCoded, NotWorking, NoTestData, NoPart2, ManualIntervention };

    protected readonly Dictionary<char, (int, int)> Directions = new() { { 'N', (0, 1) }, { 'S', (0, -1) }, { 'E', (1, 0) }, { 'W', (-1, 0) }, { 'U', (0, 1) }, { 'D', (0, -1) }, { 'L', (-1, 0) }, { 'R', (1, 0) }, { '^', (0, 1) }, { 'v', (0, -1) }, { '>', (1, 0) }, { '<', (-1, 0) } };
    protected readonly Dictionary<char, (int x, int y)> DirectionsYDown = new() { { 'N', (0, -1) }, { 'S', (0, 1) }, { 'E', (1, 0) }, { 'W', (-1, 0) }, { 'U', (0, -1) }, { 'D', (0, 1) }, { 'L', (-1, 0) }, { 'R', (1, 0) }, { '^', (0, -1) }, { 'v', (0, 1) }, { '>', (1, 0) }, { '<', (-1, 0) } };
    protected readonly List<(int, int)> Offsets = new() { (0, 1), (1, 0), (0, -1), (-1, 0) };
    protected readonly List<(int, int)> Neighbours = new() { (-1, 1), (0, 1), (1, 1), (-1, 0), (1, 0), (-1, -1), (0, -1), (1, -1) };
    protected readonly Dictionary<(char, char), char> turns = new() { { ('^', 'L'), '<' }, { ('^', 'R'), '>' }, { ('>', 'L'), '^' }, { ('>', 'R'), 'v' }, { ('v', 'L'), '>' }, { ('v', 'R'), '<' }, { ('<', 'L'), 'v' }, { ('<', 'R'), '^' } };
    protected Dictionary<(int, int), char> SimpleMap = new();
    protected int GetNeighbours(Dictionary<(int, int), char> area, int x, int y, char type) => Neighbours.Where(nbr => area[(x + nbr.Item1, y + nbr.Item2)] == type).Count();

    #endregion Common Objects

    #region Common Methods

    public void DrawMap() => DrawMap(true, false);

    public void DrawMap(bool yUp, bool showCoords)
    {
        StringBuilder s = new();
        Debug.Print("---------------------------------------------------------------------");
        int maxX = SimpleMap.Keys.Max(x => x.Item1), maxY = SimpleMap.Keys.Max(x => x.Item2);
        int minX = this.SimpleMap.Keys.Min(x => x.Item1), minY = this.SimpleMap.Keys.Min(x => x.Item2);
        if (showCoords)
        {
            s.Append("     ");
            for (int x = minX; x <= maxX; x++)
                s.Append(x % 10);
            Debug.Print(s.ToString());
        }

        if (yUp)
            for (int y = maxY; y >= minY; y--)
            {
                s.Clear();
                if (showCoords)
                    s.Append(y.ToString("D4") + " ");
                for (int x = minX; x <= maxX; x++)
                    s.Append(SimpleMap.ContainsKey((x, y)) ? SimpleMap[(x, y)] : ' ');
                Debug.Print(s.ToString());
            }
        else
            for (int y = 0; y <= maxY; y++)
            {
                s.Clear();
                if (showCoords)
                    s.Append(y.ToString("D4") + " ");
                for (int x = minX; x <= maxX; x++)
                    s.Append(SimpleMap.ContainsKey((x, y)) ? SimpleMap[(x, y)] : ' ');
                Debug.Print(s.ToString());
            }
        if (showCoords)
        {
            s.Clear();
            s.Append("     ");
            for (int x = minX; x <= maxX; x++)
                s.Append(x % 10);
            Debug.Print(s.ToString());
        }
    }


    public static string AWInputBox(string Title, string Prompt, string DefaultValue)
    {
        using AWInputBox inputBox = new()
        {
            Title = Title,
            Input = DefaultValue,
            Label = Prompt
        };
        inputBox.Show();
        return inputBox.Result == DialogResult.Cancel || inputBox.Result == DialogResult.Ignore ? DefaultValue : inputBox.Input;
    }

    protected static string GetMD5Hash(HashAlgorithm md5Hash, string input)
    {
        // Convert the input string to a byte array and compute the hash.
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        // Create a new Stringbuilder to collect the bytes and create a string.
        StringBuilder sBuilder = new();

        // Loop through each byte of the hashed data and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++)
            sBuilder.Append(data[i].ToString("x2"));

        // Return the hexadecimal string.
        return sBuilder.ToString();
    }

    protected static bool DictionaryCompare<TKey, TValue>(Dictionary<TKey, TValue> dictionary1, Dictionary<TKey, TValue> dictionary2)
    {
        EqualityComparer<TValue> valueComparer = EqualityComparer<TValue>.Default;
        return dictionary1.Count == dictionary2.Count && dictionary1.Keys.All(key => dictionary2.ContainsKey(key) && valueComparer.Equals(dictionary1[key], dictionary2[key]));
    }

    public static string ReverseString(string inp)
    {
        char[] arr = inp.ToCharArray();
        Array.Reverse(arr);
        return new string(arr);
    }
    #region Logging
    private string CurTime(string message) => (DateTime.Now - logs[message]).TotalSeconds.ToString("N3");
    protected void LogRelative(string message) => Debug.WriteLine(string.Format("{0}: {1} secs", message, CurTime(message)));
    protected void LogStartRelative(string message) => LogRelative(message + " started");
    protected void LogEndRelative(string message) => LogRelative(message + " completed");
    protected void LogStart(string message)
    {
        logs.Add(message, DateTime.Now);
        Debug.WriteLine(string.Format("{0} started", message));
    }
    protected void LogEnd(string message)
    {
        if (logs.ContainsKey(message))
        {
            Debug.WriteLine(string.Format("{0} completed: {1} secs", message, CurTime(message)));
        }
    }
    #endregion Logging

    #endregion Common Methods

    #region Year Specific Common Methods

    #region 2017

    public static string KnotHash(string inputValue)
    {
        string denseHash = "";
        int skip = 0;
        int pos = 0;
        int len = 256;
        int repeats = 64;
        List<int> work = new(len);
        for (int i = 0; i < len; i++)
            work.Add(i);

        byte[] inputBytes = Encoding.ASCII.GetBytes(inputValue + (char)17 + (char)31 + (char)73 + (char)47 + (char)23);

        for (int j = 0; j < repeats; j++)
            for (int i = 0; i < inputBytes.Length; i++)
                TieKnot(work, inputBytes[i], ref pos, ref skip);

        for (int i = 0; i < 16; i++)
        {
            int hash = 0;
            for (int j = 0; j < 16; j++)
                hash ^= work[(i * 16) + j];
            denseHash += hash.ToString("x2");
        }

        return denseHash;
    }

    public static void TieKnot(List<int> work, int length, ref int position, ref int skip)
    {
        for (int i = 0; i < length / 2; i++)
        {
            int temp = work[(position - i + length - 1) % work.Count];
            work[(position - i + length - 1) % work.Count] = work[(position + i) % work.Count];
            work[(position + i) % work.Count] = temp;
        }
        position = (position + length + skip) % work.Count;
        skip++;
    }
    #endregion 2017

    #region 2018

    public static int[] InterpretCode(string op, int[] inRegisters, int[] inputs)
    {
        int[] regs = inRegisters.ToArray();
        int output, arg1, arg2 = 0;
        output = inputs[2]; ;
        if (op.StartsWith("set"))
        {
            arg1 = op.EndsWith("r") ? regs[inputs[0]] : inputs[0];
            op = op[..3];
        }
        else if (op.EndsWith("ir"))
        {
            arg1 = inputs[0];
            arg2 = regs[inputs[1]];
            op = op[..2];
        }
        else if (op.EndsWith("rr"))
        {
            arg1 = regs[inputs[0]];
            arg2 = regs[inputs[1]];
            op = op[..2];
        }
        else if (op.EndsWith("ri"))
        {
            arg1 = regs[inputs[0]];
            arg2 = inputs[1];
            op = op[..2];
        }
        else
        {
            arg1 = regs[inputs[0]];
            arg2 = op.EndsWith("r") ? regs[inputs[1]] : inputs[1];
            op = op[..3];
        }

        switch (op)
        {
            case "add": regs[output] = arg1 + arg2; break;
            case "mul": regs[output] = arg1 * arg2; break;
            case "ban": regs[output] = arg1 & arg2; break;
            case "bo": regs[output] = arg1 | arg2; break;
            case "set": regs[output] = arg1; break;
            case "gt": regs[output] = arg1 > arg2 ? 1 : 0; break;
            case "eq": regs[output] = arg1 == arg2 ? 1 : 0; break;
            default: break;
        }
        return regs;
    }

    public static (int ipVal, int[] regs) RunCode(List<(string op, int[] args)> code, (int ipVal, int ipReg, int[] inregs) input)
    {
        int[] regs = input.inregs.ToArray();
        int ipVal = input.ipVal;

        regs[input.ipReg] = ipVal;
        regs = InterpretCode(code[ipVal].op, regs, code[ipVal].args);
        ipVal = regs[input.ipReg];

        return (++ipVal, regs);
    }

    #endregion 2018

    #region 2019
    public class IntCode
    {
        protected Dictionary<long, long> code;
        private Dictionary<long, long> baseDict;
        public long ErrorCode { get; private set; }
        public long Result { get; private set; }
        private long noun;
        private long verb;
        private long relativeBase = 0;
        private long[] inputs;
        long nextInput = 0;
        bool haveArgs = false;
        long pointer = 0;
        public bool HasOutput { get; private set; }

        public bool CodeComplete { get; private set; }

        public long Output { get; private set; }

        public IntCode() { }
        public IntCode(string Code) : this(Code, long.MaxValue) { }

        public IntCode(string Code, long Input) : this(Code, long.MaxValue, long.MaxValue, Input) { }

        public IntCode(string Code, long Noun, long Verb) : this(Code, Noun, Verb, long.MaxValue) { }

        public IntCode Clone()
        {
            IntCode newCode = new()
            {
                code = new Dictionary<long, long>(this.code),
                pointer = this.pointer,
                nextInput = 0
            };
            return newCode;
        }

        public IntCode(string Code, long Noun, long Verb, long Input)
        {
            noun = Noun;
            verb = Verb;
            haveArgs = Noun != long.MaxValue;
            if (Input != long.MaxValue)
                inputs = new long[1] { Input };
            ResetCode(Code);
        }

        private void ResetCode(string input)
        {
            long[] array = Array.ConvertAll(input.Split(','), long.Parse);
            baseDict = Enumerable.Range(0, array.Length).ToDictionary(x => (long)x, x => array[x]);
            ResetCode(true);
        }

        private void ResetCode(bool rebase)
        {
            if (rebase)
            {
                code = new Dictionary<long, long>(baseDict);
                pointer = 0;
                relativeBase = 0;
            }
            if (inputs == null || nextInput >= inputs.Length)
                nextInput = 0;
            CodeComplete = false;
            //Output = 0;
            if (haveArgs)
            {
                code[1] = noun;
                code[2] = verb;
            }
        }

        (long, (long, long), (long, long), (long, long)) GetArgs(ref long pointer)
        {
            long work = code[pointer], op = work % 100;
            work /= 100;
            (long, long)[] args = new (long, long)[3];
            long mode, numArgs = Arguments[op], arg;
            for (int i = 0; i < numArgs; i++)
            {
                mode = work % 10;
                work /= 10;
                arg = code[pointer + i + 1];
                switch (mode)
                {
                    case 0:
                        if (!code.ContainsKey(arg)) code[arg] = 0;
                        args[i] = (arg, code[arg]);
                        break;
                    case 1:
                        args[i] = (0, arg);
                        break;
                    case 2:
                        if (!code.ContainsKey(arg + relativeBase)) code[arg + relativeBase] = 0;
                        args[i] = (arg + relativeBase, code[arg + relativeBase]);
                        break;
                }
            }
            pointer += numArgs + 1;
            return (op, args[0], args[1], args[2]);
        }

        private readonly Dictionary<long, long> Arguments = new() { { 1, 3 }, { 2, 3 }, { 3, 1 }, { 4, 1 }, { 5, 2 }, { 6, 2 }, { 7, 3 }, { 8, 3 }, { 9, 1 }, { 99, 0 } };

        public long RunCode() { return RunCode(true); }
        public long RunCodeWithNoReset() { return RunCode(false); }
        public long RunCode(long[] Inputs)
        {
            inputs = Inputs;
            return RunCode();
        }

        public long RunCodeWithNoReset(long[] Inputs)
        {
            inputs = Inputs;
            return RunCodeWithNoReset();
        }

        public long RunCodeWithNoReset(long Input) { return RunCodeWithNoReset(new long[] { Input }); }

        private long RunCode(bool doRebase)
        {
            ResetCode(doRebase);
            HasOutput = false;
            do
            {
                (long action, (long address, long value) argument1, (long address, long value) argument2, (long address, long value) argument3) = GetArgs(ref pointer);
                switch (action)
                {
                    case 1:
                        code[argument3.address] = argument1.value + argument2.value;
                        break;
                    case 2:
                        code[argument3.address] = argument1.value * argument2.value;
                        break;
                    case 3:
                        //input = int.Parse(AWInputBox("Please enter value", "Please enter value", "0"));
                        code[argument1.address] = inputs[nextInput];
                        nextInput++;
                        break;
                    case 4:
                        // MessageBox.Show(argument1.value.ToString());
                        Output = argument1.value;
                        HasOutput = true;
                        break;
                    case 5:
                        if (argument1.value != 0) pointer = argument2.value;
                        break;
                    case 6:
                        if (argument1.value == 0) pointer = argument2.value;
                        break;
                    case 7:
                        code[argument3.address] = argument1.value < argument2.value ? 1 : 0;
                        break;
                    case 8:
                        code[argument3.address] = argument1.value == argument2.value ? 1 : 0;
                        break;
                    case 9:
                        relativeBase += argument1.value;
                        break;
                    case 99:
                        CodeComplete = true;
                        break;
                }
            } while (!CodeComplete && !HasOutput);
            Result = code[0];
            return Result;
        }

        public long FindResult(int ValueToFind)
        {
            haveArgs = true;
            bool foundResult = false;
            for (noun = 0; noun < 100; noun++)
            {
                for (verb = 0; verb < 100; verb++)
                {
                    if (foundResult = RunCode() == ValueToFind)
                    {
                        ErrorCode = (noun * 100) + verb;
                        break;
                    }
                }
                if (foundResult)
                    break;
            }
            return ErrorCode;
        }

    }

    #endregion 2019

    #endregion Year Specific Common Methods
}
