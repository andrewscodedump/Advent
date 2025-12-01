using AndrewWatson.Library;

namespace Advent;

public abstract partial class Day
{
    #region Constructors and Declarations

    private string challenge;
    private int year;
    private int day;

    protected Day() { }
    protected Day(bool testMode, int whichPart) : this(testMode, whichPart, DayBatchStatus.Available) { }
    protected Day(bool testMode, int whichPart, DayBatchStatus batchStatus) => SetMode(testMode, whichPart, batchStatus);

    protected System.Security.Cryptography.MD5 MD5 { get; private set; }
    public bool TestMode { get; set; }
    public int WhichPart { get; set; }
    public bool Part1 { get; set; }
    public bool Part2 { get; set; }
    public abstract void DoWork();
    public DayBatchStatus BatchStatus { get; set; }
    public string StatusText
    {
        get => BatchStatus switch
        {
            DayBatchStatus.NotDoneYet => "Not Done Yet",
            DayBatchStatus.NonCoded => "Solved by non-code method",
            DayBatchStatus.NoTestData => "No Test Data",
            DayBatchStatus.NoPart2 => "There is no part 2 for this puzzle",
            DayBatchStatus.NoInputs => "No inputs available",
            DayBatchStatus.Available => "Available",
            DayBatchStatus.Performance => "Performance problems",
            DayBatchStatus.NotWorking => "Not working",
            DayBatchStatus.ManualIntervention => "Requires manual intervention",
            _ => string.Empty,
        };
    }

    public bool BatchRun { get; set; }
    public string Output { get; set; }
    public string Description { get; private set; }
    private int currentInput;
    public int CurrentInput
    {
        get { return currentInput; }
        set
        {
            currentInput = value;
            SetInputs();
        }
    }

    public string[] Inputs { get; private set; }
    public string[][] InputBlocks { get; private set; }
    protected long[][] InputNumbers { get; private set; }
    protected string Input { get; private set; }

    private List<List<string>> allInputs;
    public List<List<string>> AllInputs
    {
        get { return allInputs; }
        private set
        {
            allInputs = value;
            SetInputs();
        }
    }

    public string Expected
    {
        get
        {
            return Expecteds.Count > currentInput ? Expecteds[CurrentInput] : string.Empty;
        }
    }
    public List<string> Expecteds { get; set; }
    private readonly string rootFolder = Properties.Settings.Default["RootFolder"].ToString();
    private string inputPath;

    #endregion Constructors

    #region Public Methods
    public void SetMode(bool testMode, int whichPart) => SetMode(testMode, whichPart, DayBatchStatus.Available);

    public void SetMode(bool testMode, int whichPart, DayBatchStatus batchStatus)
    {
        challenge = string.Concat(GetType().Namespace.Where(char.IsAsciiLetter));
        year = int.Parse(string.Concat(GetType().Namespace.Where(char.IsDigit)));
        day = int.Parse(GetType().Name[^2..]);
        WhichPart = whichPart;
        Part1 = whichPart == 1;
        Part2 = !Part1;
        TestMode = testMode;
        BatchStatus = batchStatus;
        inputPath = GetInputPath();
        AllInputs = GetInputs();
        SetInputs();
        BatchStatus = CheckStatus(BatchStatus);
        Expecteds = GetExpecteds();
        Description = GetDescription();
        Output = string.Empty;
        MD5 = System.Security.Cryptography.MD5.Create();
    }

    #endregion Public Methods


    #region Private Methods

    private string GetInputPath()
    {
        string result = (challenge, year) switch
        {
            ("Codyssi", _) or ("Everybody", _) => $@"{rootFolder}\{challenge}\{year}\Inputs\Days{((day - 1) / 5 * 5) + 1:D2}-{((day - 1) / 5 * 5) + 5:D2}\Day{day:D2}",
            ("Advent", 2025 or <= 21) => $@"{rootFolder}\{challenge}\{year}\Inputs\Days{((day - 1) / 6 * 6) + 1:D2}-{((day - 1) / 6 * 6) + 6:D2}\Day{day:D2}",
            ("Euler", _) => $@"{rootFolder}\{challenge}\Pages{((year - 1) / 5 * 5) + 1:D2}-{((year - 1) / 5 * 5) + 5:D2}\Page{year:D2}\Inputs\Parts{((day - 1) / 5 * 5) + 1:D2}-{((day - 1) / 5 * 5) + 5:D2}\Part{day:D2}",
            _ => $@"{rootFolder}\{year}\Inputs\Days{((day - 1) / 5 * 5) + 1:D2}-{((day - 1) / 5 * 5) + 5:D2}\Day{day:D2}",
        };
        return result;
    }

    private List<List<string>> GetInputs()
    {
        List<List<string>> inputs = [];
        bool fileExists = false, encFileExists = false;
        string mode = TestMode ? "Test" : "Live";
        string fileName = $"{mode}Both.txt", encFileName = $"{mode}Both.enc";

        if (File.Exists($@"{inputPath}\{fileName}"))
        {
            fileExists = true;
        }
        else
        {
            fileName = $"{mode}Part{WhichPart}.txt";
            if (File.Exists($@"{inputPath}\{fileName}"))
                fileExists = true;
        }

        if (File.Exists($@"{inputPath}\{encFileName}"))
        {
            encFileExists = true;
        }
        else
        {
            encFileName = $"{mode}Part{WhichPart}.enc";
            if (File.Exists($@"{inputPath}\{encFileName}"))
                encFileExists = true;
        }

        if (fileExists && !encFileExists && mode == "Live")
        {
            // Create encrypted file - for live inputs only
            encFileName = Path.ChangeExtension(fileName, "enc");
            _ = Encryption.TryEncrypt($@"{inputPath}\{fileName}", $@"{inputPath}\{encFileName}", "Advent", out _);
        }
        else if (!fileExists && encFileExists)
        {
            // Decrypt file
            fileName = Path.ChangeExtension(encFileName, "txt");
            fileExists = Encryption.TryDecrypt($@"{inputPath}\{encFileName}", $@"{inputPath}\{fileName}", "Advent", out _);
        }

        if (!fileExists || File.ReadAllLines($@"{inputPath}\{fileName}").Length == 0)
        {
            BatchStatus = DayBatchStatus.NoInputs;
            return inputs;
        }

        List<string> simpleInputs = [.. File.ReadAllLines($@"{inputPath}\{fileName}")];
        int[] breaks = [.. Enumerable.Range(0, simpleInputs.Count).Where(i => simpleInputs[i] == "***AdditionalInput***")];
        breaks = [-1, ..breaks, simpleInputs.Count];
        breaks.Skip(1).Zip(breaks, (second, first) => (first, second)).ForEach(p => inputs.Add(simpleInputs[(p.first + 1)..p.second]));
        return inputs;
    }

    private List<string> GetExpecteds()
    {
        List<string> expecteds = [];
        bool reading = false;
        string mode = TestMode ? "Test" : "Live";
        //string 
        string filePath = $@"{inputPath}\Expected.txt";

        if (!File.Exists(filePath) || File.ReadAllLines(filePath).Length == 0) return expecteds;

        foreach (string line in File.ReadAllLines(filePath))
        {
            if (line == $"{mode}{WhichPart}")
            {
                reading = true;
                continue;
            }
            if (reading)
            {
                string[] skips = ["***NotDoneYet***", "***Performance***", "***NoTestData***", "***NonCoded***", "***NotWorking***", "***ManualIntervention***"];
                if (line.StartsWith("Test") || line.StartsWith("Live"))
                    reading = false;
                else if (skips.Contains(line))
                    continue;
                else
                    expecteds.Add(line);
            }
        }
        return expecteds;
    }

    private string GetDescription()
    {
        StringBuilder description = new();

        string filePath = $@"{inputPath}\Description.txt";
        if (!File.Exists(filePath) || File.ReadAllLines(filePath).Length == 0) return string.Empty;

        foreach (string line in File.ReadAllLines(filePath))
        {
            description.AppendLine(line);
        }
        return description.ToString();
    }

    private DayBatchStatus CheckStatus(DayBatchStatus current)
    {
        bool reading = false;
        string mode = TestMode ? "Test" : "Live";
        bool firstLine = true;
        string filePath = $@"{inputPath}\Expected.txt";

        if (!File.Exists(filePath) || File.ReadAllLines(filePath).Length == 0) return current;

        foreach (string line in File.ReadAllLines(filePath))
        {
            if (line == $"{mode}{WhichPart}")
            {
                reading = true;
                firstLine = false;
                continue;
            }
            if (line.StartsWith("Test") || line.StartsWith("Live"))
                reading = false;
            if (reading || firstLine)
            {
                DayBatchStatus test = line switch
                {
                    "***NotDoneYet***" => DayBatchStatus.NotDoneYet,
                    "***NoTestData***" => DayBatchStatus.NoTestData,
                    "***Performance***" => DayBatchStatus.Performance,
                    "***NonCoded***" => DayBatchStatus.NonCoded,
                    "***NotWorking***" => DayBatchStatus.NotWorking,
                    "***ManualIntervention***" => DayBatchStatus.ManualIntervention,
                    _ => current,
                };
                if (test != current)
                        return test;
            }
            firstLine = false;
        }
        return current;
    }

    private void SetInputs()
    {
        if (allInputs == null || allInputs.Count == 0 || BatchStatus == DayBatchStatus.NotDoneYet) return;
        if (BatchStatus == DayBatchStatus.NoInputs) BatchStatus = DayBatchStatus.Available;
        Inputs = [.. AllInputs[CurrentInput]];
        InputBlocks = [];
        int[] breaks = [.. Enumerable.Range(0, Inputs.Length).Where(i => Inputs[i] == "")];
        breaks = [-1, .. breaks, Inputs.Length];
        breaks.Skip(1).Zip(breaks, (second, first) => (first, second)).ForEach(p => InputBlocks = [..InputBlocks, Inputs[(p.first + 1)..p.second]]);

        List<long[]> inputNumbers = [];
        Input = Inputs[0];
        try
        {
            inputNumbers.AddRange(from string inp in Inputs
                                  let m = Numbers().Matches(inp)
                                  let numbers = m.Select(m => m.ToString()).Where(n => long.TryParse(n, out _)).Select(i => long.Parse(i))
                                  where numbers.Any()
                                  select numbers.ToArray());
            InputNumbers = [.. inputNumbers];
        }
        catch { /* Unable to create numbers - this is expected */ }
    }

    #endregion Private Methods

    #region Common Objects

    public enum DayBatchStatus { Available, NotDoneYet, Performance, NonCoded, NotWorking, NoTestData, NoPart2, ManualIntervention, NoInputs };

    #endregion Common Objects

    #region Common Methods

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

    protected static string GetMD5Hash(System.Security.Cryptography.HashAlgorithm md5Hash, string input)
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

    [GeneratedRegex(@"[\+-]?[0-9]*")]
    private static partial Regex Numbers();

    public static long Mod(long dividend, long divisor)
    {
        long result = dividend % divisor;
        return result < 0 ? result + divisor : result;
    }
    #endregion Common Methods
}
