namespace Advent;

public abstract partial class Day
{
    #region Constructors and Declarations

    private int year;
    private int day;

    protected Day() { }
    protected Day(bool testMode, int whichPart) : this(testMode, whichPart, DayBatchStatus.Available) { }
    protected Day(bool testMode, int whichPart, DayBatchStatus batchStatus) => SetMode(testMode, whichPart, batchStatus);

    public DayStatus Status { get; set; }
    protected System.Security.Cryptography.MD5 MD5 { get; private set; }
    public bool TestMode { get; set; }
    public int WhichPart { get; set; }
    public bool Part1 { get; set; }
    public bool Part2 { get; set; }
    public abstract void DoWork();
    public DayBatchStatus BatchStatus { get; set; }
    public bool BatchRun { get; set; }
    public string Output { get; set; }
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

    protected string[] InputSplit;
    protected int[] InputSplitInt;
    protected string[] InputSplitC;
    protected int[] InputSplitCInt;
    protected long[] InputSplitCLong;
    protected string[] InputSplitter(char delimiter) => InputSplitter(new char[] { delimiter });
    protected string[] InputSplitter(char[] delimiters) => Input.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
    public string Input
    {
        get => Inputs[CurrentInput];
        set
        {
            Inputs[CurrentInput] = value;
            SetInputs();
        }
    }

    private List<string> inputs;

    public List<string> Inputs
    {
        get { return inputs; }
        set 
        {
            inputs = value;
            SetInputs();
        }
    }

    public string Expected { get => Expecteds[CurrentInput]; }
    public List<string> Expecteds { get; set; }
    private string inputPath;

    #pragma warning disable IDE0044 // Add readonly modifier
    private Dictionary<string, DateTime> logs = new();
    #pragma warning restore IDE0044 // Add readonly modifier

    #endregion Constructors

    #region Public Methods
    public void SetMode(bool testMode, int whichPart) => SetMode(testMode, whichPart, DayBatchStatus.Available);

    public void SetMode(bool testMode, int whichPart, DayBatchStatus batchStatus)
    {
        year = int.Parse(this.GetType().Namespace[^4..]);
        day = int.Parse(this.GetType().Name[^2..]);
        WhichPart = whichPart;
        Part1 = whichPart == 1;
        Part2 = !Part1;
        TestMode = testMode;
        BatchStatus = batchStatus;
        inputPath = $@"C:\Userfiles\Hobbies\Computer\Sources\Advent\{year}\Inputs\Days{((day - 1) / 5 * 5) + 1:D2}-{((day - 1) / 5 * 5) + 5:D2}\Day{day:D2}";
        Inputs = GetInputs();
        SetInputs();
        Expecteds = GetExpecteds();
        Output = string.Empty;
        Rand = new Random();
        MD5 = System.Security.Cryptography.MD5.Create();
        //TODO this needs to be changed to use a config file
    }

public void AddInput(string newInput)
    {
        Inputs.Add(newInput);
        SetInputs();
    }

    #endregion Public Methods

    #region Private Methods

    private List<string> GetInputs()
    {
        List<string> inputs = new();
        if (new DateTime(year, 12, day, 05, 00, 00) > DateTime.Now)
        {
            BatchStatus = DayBatchStatus.Future;
            return inputs;
        }
        bool fileExists = false;
        string mode = TestMode ? "Test" : "Live";
        string fileName = $"{mode}Both.txt";

        if (File.Exists(inputPath + "\\" + fileName))
        {
            fileExists = true;
        }
        else
        {
            fileName = $"{mode}Part{WhichPart}.txt";
            if (File.Exists(inputPath + "\\" + fileName))
                fileExists = true;
        }

        if (!fileExists)
        {
            if (!TestMode && WhichPart == 1 && BatchStatus == DayBatchStatus.Available)
            {
                // Get input from AoC website and save to file
                if (!GetInputFromAoC())
                {
                    BatchStatus = DayBatchStatus.NoInputs;
                    return inputs;
                }
            }
            else
            {
                BatchStatus = DayBatchStatus.NoInputs;
                return inputs;
            }
        }
        else if (File.ReadAllLines(inputPath + "\\" + fileName).Length == 0)
        {
            BatchStatus = DayBatchStatus.NoInputs;
            return inputs;
        }

        StringBuilder input = new();
        foreach (string line in File.ReadAllLines(inputPath + "\\" + fileName))
        {
            if (line == "***AdditionalInput***")
            {
                inputs.Add(input.ToString());
                input.Clear();
                continue;
            }
            if (input.Length > 0)
                input.Append(';');
            input.Append(line);
        }
        inputs.Add(input.ToString());
        return inputs;
    }

    private static bool GetInputFromAoC()
    {
        return false;
        /*if (!File.Exists(filename))
        {
            var uri = new Uri("https://adventofcode.com");
            var cookies = new CookieContainer();
            cookies.Add(uri, new System.Net.Cookie("session", cookie));
            using var file = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
            using var handler = new HttpClientHandler() { CookieContainer = cookies };
            using var client = new HttpClient(handler) { BaseAddress = uri };
            using var response = await client.GetAsync($"/{year}/day/{day}/input");
            using var stream = await response.Content.ReadAsStreamAsync();
            await stream.CopyToAsync(file);
        }*/
    }

    private List<string> GetExpecteds()
    {
        List<string> expecteds = new();
        if (new DateTime(year, 12, day, 05, 00, 00) > DateTime.Now) return expecteds;
        bool reading = false;
        string mode = TestMode ? "Test" : "Live";

        //TODO this needs to be changed to use a config file
        string filePath = $@"{inputPath}\Expected.txt";

        if (!File.Exists(filePath) || File.ReadAllLines(filePath).Length == 0) return expecteds;

        foreach (string line in File.ReadAllLines(filePath))
        {
            if (line == $"{mode}{WhichPart}")
            {
                reading = true;
                continue;
            }
            if (line.StartsWith("Test") || line.StartsWith("Live"))
                reading = false;
            if (reading)
            {
                expecteds.Add(line);
            }
        }
        return expecteds;
    }

    private void SetInputs()
    {
        if (inputs == null || inputs.Count == 0 || BatchStatus == DayBatchStatus.NotDoneYet) return;
        if (BatchStatus == DayBatchStatus.NoInputs) BatchStatus = DayBatchStatus.Available;
        InputSplit = Inputs[CurrentInput].Split(';', StringSplitOptions.RemoveEmptyEntries);
        InputSplitC = Inputs[CurrentInput].Split(',');
        try
        {
            InputSplitInt = Array.ConvertAll(InputSplit, int.Parse);
        }
        catch
        {
            InputSplitInt = null;
        }
        try
        {
            InputSplitCInt = Array.ConvertAll(InputSplitC, int.Parse);
        }
        catch
        {
            InputSplitCInt = null;
        }
        try
        {
            InputSplitCLong = Array.ConvertAll(InputSplitC, long.Parse);
        }
        catch
        {
            InputSplitCLong = null;
        }
    }

    #endregion Private Methods

    #region Common Objects

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
    protected static Random Rand;
    public enum DayStatus { NotStarted, Running, Successful, Failed, Unknown };
    public enum DayBatchStatus { Available, NotDoneYet, Performance, NonCoded, NotWorking, NoTestData, NoPart2, ManualIntervention, Future, NoInputs };

    protected readonly Dictionary<char, (int x, int y)> Directions = new() { { 'N', (0, 1) }, { 'S', (0, -1) }, { 'E', (1, 0) }, { 'W', (-1, 0) }, { 'U', (0, 1) }, { 'D', (0, -1) }, { 'L', (-1, 0) }, { 'R', (1, 0) }, { '^', (0, 1) }, { 'v', (0, -1) }, { '>', (1, 0) }, { '<', (-1, 0) } };
    protected readonly Dictionary<char, (int x, int y)> DirectionsYDown = new() { { 'N', (0, -1) }, { 'S', (0, 1) }, { 'E', (1, 0) }, { 'W', (-1, 0) }, { 'U', (0, -1) }, { 'D', (0, 1) }, { 'L', (-1, 0) }, { 'R', (1, 0) }, { '^', (0, -1) }, { 'v', (0, 1) }, { '>', (1, 0) }, { '<', (-1, 0) } };
    protected readonly List<(int, int)> Offsets = new() { (0, 1), (1, 0), (0, -1), (-1, 0) };
    protected readonly List<(int, int)> Neighbours = new() { (-1, 1), (0, 1), (1, 1), (-1, 0), (1, 0), (-1, -1), (0, -1), (1, -1) };
    protected readonly Dictionary<(char, char), char> turns = new() { { ('^', 'L'), '<' }, { ('^', 'R'), '>' }, { ('>', 'L'), '^' }, { ('>', 'R'), 'v' }, { ('v', 'L'), '>' }, { ('v', 'R'), '<' }, { ('<', 'L'), 'v' }, { ('<', 'R'), '^' } };
    protected Dictionary<(int, int), char> SimpleMap = new();
    protected int CountNeighbours(Dictionary<(int, int), char> area, int x, int y, char type) => Neighbours.Where(nbr => area[(x + nbr.Item1, y + nbr.Item2)] == type).Count();

    #endregion Common Objects

    #region Common Methods

    public void PopulateMapFromInput()
    {
        for (int y = 0; y < InputSplit.Length; y++)
        {
            string work = InputSplit[y];
            for (int x = 0; x < work.Length; x++)
            {
                SimpleMap[(x, y)] = work[x];
            }
        }
    }
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
}
