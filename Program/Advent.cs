using Advent.Controls;
using System.ComponentModel;

[assembly: CLSCompliant(true)]
namespace Advent;

public partial class AdventOfCode : Form
{
    // todo Defaults for non-advent

    #region Constructors and Declarations

    private Day theDay;
    bool noReset;
    private List<Challenge> Challenges;
    public AdventOfCode()
    {
        InitializeComponent();
        PopulateDropDown();
        SetupForm();
    }

    private void PopulateDropDown()
    {
        Challenges = [
            new("Advent", "Advent of Code", "https://adventofcode.com/"),
            new("Everybody", "Everybody Codes", "https://everybody.codes/event/"),
            new("Codyssi", "Codyssi", "https://www.codyssi.com/view_problem_"),
            new("Euler", "Project Euler", "https://projecteuler.net/problem="),
        ];
        ChallengeType.DataSource = Challenges;
        ChallengeType.DisplayMember = "Description";

    }

    #endregion Constructors and Declarations

    #region Private Methods

    private void ResetScreen()
    {
        string challenge = ((Challenge)ChallengeType.SelectedItem).Abbreviation;
        SetLimits(challenge, (int)updYear.Value);
        if (noReset) return;
        btnProcess.Enabled = true;
        string typeName = $"{challenge}{(int)updYear.Value:D4}.Day{(int)updDay.Value:D2}";
        try
        {
            theDay = (Day)Activator.CreateInstance(Type.GetType(typeName), [chkTestMode.Checked, (int)updPuzzle.Value]);
        }
        catch
        {
            try
            {
                theDay = (Day)Activator.CreateInstance(Type.GetType(typeName));
                theDay.SetMode(chkTestMode.Checked, (int)updPuzzle.Value);
            }
            catch
            {
                txtExpected.Text = "Unable to fetch code";
                btnProcess.Enabled = false;
                return;
            }
        }
        Description.Text = theDay.Description;
        List<List<string>> Inputs = GetInputs();
        List<string> Expecteds = GetExpecteds();
        if (Inputs.Count != Expecteds.Count)
        {
            txtExpected.Text = "Mismatch between Inputs and Expected Outputs";
            if (Inputs.Count > 0)
                Inputs = Inputs.GetRange(0, 1);
        }
        inputNumber.Text = "0";
        prevInput.Visible = Inputs.Count > 1;
        prevInput.Enabled = false;
        nextInput.Visible = Inputs.Count > 1;
        nextInput.Enabled = true;
        ResetInputs();
    }

    private void ResetInputs()
    {
        if (inputNumber.Text == "0")
        {
            txtInput.Text = string.Join('¶', GetInput());
            txtExpected.Text = GetExpected() == "" ? theDay.StatusText : GetExpected();
        }
        else
        {
            int counter = int.Parse(inputNumber.Text);
            txtInput.Text = GetInput();
            txtExpected.Text = GetExpected();
            prevInput.Enabled = counter > 0;
            nextInput.Enabled = counter < GetInputs().Count - 1;
        }
        txtOutput.Text = string.Empty;
        txtOutput.BackColor = Color.White;
        txtTimeTaken.Text = string.Empty;
        FullInput.Enabled = GetInputs().Count != 0;
    }

    private string GetExpected()
    {
        return CheckStatus(theDay.BatchStatus) ? theDay.StatusText : theDay.Expected;
    }
    private List<string> GetExpecteds()
    {
        return CheckStatus(theDay.BatchStatus) || theDay.Expecteds.Count == 0 ? [theDay.StatusText] : theDay.Expecteds;
    }

    private string GetInput()
    {
        theDay.CurrentInput = int.Parse(inputNumber.Text);
        return CheckStatus(theDay.BatchStatus) ? theDay.StatusText : string.Join('¶', theDay.Inputs);
    }

    private List<List<string>> GetInputs()
    {
        return CheckStatus(theDay.BatchStatus) ? [] : theDay.AllInputs;
    }

    private static bool CheckStatus(Day.DayBatchStatus status)
    {
        return status switch
        {
            Day.DayBatchStatus.NotDoneYet
            or Day.DayBatchStatus.NonCoded
            or Day.DayBatchStatus.NoTestData
            or Day.DayBatchStatus.NoPart2
            or Day.DayBatchStatus.NoInputs => true,
            _ => false,
        };
    }

    private sealed class Challenge (string Abbreviation, string Description, string Website)
    {
        public string Abbreviation { get; set; } = Abbreviation;
        public string Description { get; set; } = Description;
        public string Website { get; set; } = Website;
    }

    private bool TryGetDefaults(out (Challenge challenge, int year, int day, int puzzle, bool testMode) defaults, out string errorMessage)
    {
        defaults = (Challenges.First(c => c.Abbreviation == "Advent"), 2015, 1, 2, true);
        StringBuilder error = new();
        string filePath = $@"{Properties.Settings.Default["RootFolder"]}\Defaults.txt";
        if (!File.Exists(filePath) || File.ReadAllLines(filePath).Length == 0)
        {
            error.AppendLine("File is missing or empty");
        }
        else
        {
            foreach (string line in File.ReadAllLines(filePath))
            {
                string[] bits = line.Split('=');
                switch (bits[0])
                {
                    case "Challenge":
                        if (Challenges.Any(c => c.Abbreviation == bits[1]))
                            defaults.challenge = Challenges.First(c => c.Abbreviation == bits[1]);
                        else
                            error.AppendLine("Invalid Challenge type");
                        break;
                    case "Day":
                        if (!int.TryParse(bits[1], out defaults.day))
                            error.AppendLine("Invalid Day value");
                        break;
                    case "Puzzle":
                        if (!int.TryParse(bits[1], out defaults.puzzle))
                            error.AppendLine("Invalid Puzzle value");
                        break;
                    case "Mode":
                        if (bits[1] == "Test") defaults.testMode = true;
                        else if (bits[1] == "Live") defaults.testMode = false;
                        else error.AppendLine("Invalid Mode value");
                        break;
                    case "Year":
                        if (!int.TryParse(bits[1], out defaults.year))
                            error.AppendLine("Invalid Year value");
                        break;
                }

            }
        }
        errorMessage = error.ToString();
        return string.IsNullOrEmpty(errorMessage);
    }

    private object DoPuzzle()
    {
        if (CheckStatus(theDay.BatchStatus))
            return theDay.StatusText;
        else
        {
            theDay.DoWork();
            return theDay.Output;
        }
    }

    private void SetupForm()
    {
        noReset = true;
        if (!TryGetDefaults(out (Challenge challenge, int year, int day, int puzzle, bool testMode) defaults, out string errorMessage))
        {
            MessageBox.Show($"Error in defaults file\r\n\r\n{errorMessage}");
        }
        else
        {
            SetLimits(defaults.challenge.Abbreviation, defaults.year);
            ChallengeType.SelectedItem = defaults.challenge;
            updYear.Value = defaults.year;
            updDay.Value = defaults.day;
            updPuzzle.Value = defaults.puzzle;
            chkTestMode.Checked = defaults.testMode;
        }
        noReset = false;
        ResetScreen();
    }

    private void SetLimits(string challenge, int year)
    {
        switch (challenge)
        {
            case "Everybody":
                updYear.Minimum = 2024;
                updYear.Maximum = 2025;
                updDay.Maximum = 20;
                updPuzzle.Maximum = 3;
                break;
            case "Codyssi":
                updYear.Minimum = 2024;
                updYear.Maximum = 2025;
                updDay.Maximum = year == 2024 ? 4 : 18;
                updPuzzle.Maximum = 3;
                break;
            case "Euler":
                updYear.Minimum = 1;
                updYear.Maximum = 20;
                updDay.Maximum = year == 20 ? 6 : 50;
                updPuzzle.Maximum = 1;
                break;
            default:
                updYear.Minimum = 2016;
                updYear.Maximum = 2025;
                updDay.Maximum = year == 2025 ? 12 : 25;
                updPuzzle.Maximum = 2;
                break;
        }

    }

    private void BatchTest()
    {
        btnBatch.Enabled = false;
        btnProcess.Enabled = false;
        btnExit.Text = "&Cancel";
        BatchWorker.RunWorkerAsync();
    }

    private string DoBatch(BackgroundWorker worker)
    {
        string challenge = "";
        ChallengeType.Invoke(new MethodInvoker(delegate { challenge = ((Challenge)ChallengeType.SelectedItem).Abbreviation; }));
        int year = (int)updYear.Value;
        int puzzles = challenge switch
        {
            "Euler" => 1,
            "Codyssi" => 3,
            "Everybody" => 4,
            _ => 2
        };
        int days = challenge switch
        {
            "Euler" => 50,
            "Codyssi" => year == 2024 ? 4 : 18,
            "Everybody" => 20,
            _ => year == 2025 ? 12 : 25
        };
        StringBuilder output = new();
        Debug.WriteLine($"Starting Batch Run for {year}");
        output.AppendLine($"Starting Batch Run for {year}");
        output.AppendLine("\r\n    Day          Part1           Part2");
        Stopwatch overall = new();
        overall.Start();
        for (int day = 1; day <= days; day++)
        {
            for (int puzzle = 1; puzzle <= puzzles; puzzle++)
            {
                if (worker.CancellationPending)
                    return output.ToString();
                worker.ReportProgress(((day - 1) * 2) + puzzle, $"{day}/{puzzle}");
                Stopwatch sw = new();
                theDay = (Day)Activator.CreateInstance(Type.GetType($"{challenge}{year:D4}.Day{day:D2}"));
                theDay.SetMode(chkTestMode.Checked, puzzle);

                if (theDay.BatchStatus == Day.DayBatchStatus.NotDoneYet || theDay.BatchStatus == Day.DayBatchStatus.NoInputs) continue;
                if (puzzle == 1)
                {
                    output.AppendLine();
                    output.Append($"    {day}{(day < 10 ? " " : "")}");
                }
                switch (theDay.BatchStatus)
                {
                    case Day.DayBatchStatus.NonCoded:
                        output.Append($"     (non-coded)");
                        Debug.WriteLine($"Day: {day} (non-coded)");
                        break;
                    case Day.DayBatchStatus.NotDoneYet:
                        output.Append($"  (not done yet)");
                        Debug.WriteLine($"Day: {day} not done yet");
                        break;
                    case Day.DayBatchStatus.NoTestData:
                        output.Append($"  (no test data)");
                        Debug.WriteLine($"Day: {day} no test data");
                        break;
                    case Day.DayBatchStatus.NotWorking:
                        output.Append($"   (not working)");
                        Debug.WriteLine($"Day: {day} not working");
                        break;
                    case Day.DayBatchStatus.Performance:
                        output.Append($"   (performance)");
                        Debug.WriteLine($"Day: {day} (performance)");
                        break;
                    case Day.DayBatchStatus.NoPart2:
                        output.Append($"     (no part 2)");
                        Debug.WriteLine($"Day: {day} (no part 2)");
                        break;
                    case Day.DayBatchStatus.Available:
                    case Day.DayBatchStatus.ManualIntervention:
                        theDay.BatchRun = true;
                        double totalTime = 0;
                        if (worker.CancellationPending) break;
                        sw.Restart();
                        theDay.DoWork();
                        totalTime += sw.ElapsedTicks;
                        sw.Stop();
                        if (theDay.Output.ToString() != theDay.Expecteds[0] && theDay.BatchStatus != Day.DayBatchStatus.ManualIntervention)
                        {
                            output.Append(" **Test Failed**");
                            Debug.WriteLine($"Day: {day}, part {puzzle} Test Failed");
                        }
                        else
                        {
                            output.Append(FormatTime(totalTime));
                            Debug.WriteLine($"Day: {day}, part {puzzle}{FormatTime(totalTime)}");
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        output.AppendLine().AppendLine();
        output.AppendLine("Batch Run Completed");
        Debug.WriteLine("Batch Run Completed");
        output.AppendLine($"Total Time = {overall.Elapsed.TotalSeconds:0.000000} secs");
        Debug.WriteLine($"Total Time = {overall.Elapsed.TotalSeconds:0.000000} secs");
        return output.ToString();
    }

    private static string FormatTime(double ticks)
    {
        double seconds = ticks / Stopwatch.Frequency;
        string displayTime = seconds switch
        {
            >= 60 => $"{seconds / 60.0:0.000} mins",
            >= 1 => $"{seconds:0.000} secs",
            >= 0.001 => $"{seconds * 1000:0.000} ms",
            _ => $"{seconds * 1000000:0} µs"
        };
        return $"{Pad(displayTime, 16)}";
    }

    private static string Pad(string text, int len)
    {
        string spaces = string.Empty;
        if (text.Length < len)
            spaces = new(' ', len - text.Length);
        return $"{spaces}{text}";
    }
    #endregion Private Methods

    #region Control methods

    private void Process_Click(object sender, EventArgs e)
    {
        Stopwatch timer = new();
        timer.Start();
        Cursor = Cursors.WaitCursor;
        txtOutput.Text = "";
        Update();
        txtOutput.Text = DoPuzzle().ToString();
        txtOutput.BackColor = txtOutput.Text == txtExpected.Text || txtExpected.Text == "NotDoneYet" ? Color.White : Color.Pink;
        double timeTaken = timer.Elapsed.TotalMilliseconds;
        if (timeTaken >= 1000)
            txtTimeTaken.Text = $"{timeTaken / 1000:0.000000} secs";
        else if (timeTaken >= 1)
            txtTimeTaken.Text = $"{timeTaken:0.000} ms";
        else
            txtTimeTaken.Text = $"{timeTaken * 1000:0} µs";
        Cursor = Cursors.Default;
    }

    private void Exit_Click(object sender, EventArgs e)
    {
        if (btnExit.Text == "&Cancel")
            BatchWorker.CancelAsync();
        else
            Close();
    }

    private void ResetScreenHandler(object sender, EventArgs e) => ResetScreen();

    private void Batch_Click(object sender, EventArgs e) => BatchTest();

    private void PrevInput_Click(object sender, EventArgs e)
    {
        if (int.TryParse(inputNumber.Text, out int counter) && counter > 0)
        {
            inputNumber.Text = (--counter).ToString();
            theDay.CurrentInput = counter;
        }
        ResetInputs();
    }

    private void NextInput_Click(object sender, EventArgs e)
    {
        if (int.TryParse(inputNumber.Text, out int counter) && counter < GetInputs().Count - 1)
        {
            inputNumber.Text = (++counter).ToString();
            theDay.CurrentInput = counter;
        }
        ResetInputs();
    }

    private void BatchWorker_DoWork(object sender, DoWorkEventArgs e)
    {
        BackgroundWorker worker = sender as BackgroundWorker;
        e.Result = DoBatch(worker);
    }

    private void BatchWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        Progress.Value = e.ProgressPercentage;
        ProgressText.Text = e.UserState.ToString();
    }

    private void BatchWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        ShowText(e.Result.ToString());
        btnBatch.Enabled = true;
        btnProcess.Enabled = true;
        btnExit.Text = "E&xit";
        Progress.Value = 0;
        ProgressText.Text = "";
    }
    #endregion Control methods

    private void FullInput_Click(object sender, EventArgs e)
    {
        ShowText(string.Join("\r\n", theDay.Inputs));
    }

    private void ShowText(string text)
    {
        ShowMe showMe = new()
        {
            Contents = text,
            Centre = Bounds,
        };
        showMe.ShowDialog();
    }

    private void Website_Click(object sender, EventArgs e)
    {
        Challenge selected = (Challenge)ChallengeType.SelectedItem;
        string webPage = selected.Abbreviation switch
        {
            "Codyssi" => $"{(((int)updYear.Value - 2024) * 4) + (int)updDay.Value}",
            "Euler" => $"{(((int)updYear.Value - 1) * 20) + (int)updDay.Value}",
            "Everybody" => $"{updYear.Value}/quests/{updDay.Value}",
            _ => $"{updYear.Value}/day/{updDay.Value}",
        };
        var psi = new ProcessStartInfo
        {
            UseShellExecute = true,
            FileName = $"{selected.Website}{webPage}",
        };
        Process.Start(psi);
    }
}
