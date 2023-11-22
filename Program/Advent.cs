
using Advent.Controls;
using System.ComponentModel;

[assembly: CLSCompliant(true)]
namespace Advent;

public partial class AdventOfCode : Form
{

    #region Constructors and Declarations

    private Day theDay;
    private DateTime startTime;
    bool noReset;

    public AdventOfCode()
    {
        InitializeComponent();
        SetupForm();
    }

    #endregion Constructors and Declarations

    #region Private Methods

    private void ResetScreen()
    {
        if (noReset) return;
        try
        {
            theDay = (Day)Activator.CreateInstance(Type.GetType($"Advent{updYear.Value}.Day{(int)updDay.Value:D2}"), new object[] { chkTestMode.Checked, (int)updPuzzle.Value });
        }
        catch
        {
            theDay = (Day)Activator.CreateInstance(Type.GetType($"Advent{updYear.Value}.Day{(int)updDay.Value:D2}"));
            theDay.SetMode(chkTestMode.Checked, (int)updPuzzle.Value);
        }
        Description.Text = theDay.Description;
        List<List<string>> Inputs = GetInputs();
        List<string> Expecteds = GetExpecteds();
        if (Inputs.Count != Expecteds.Count)
        {
            MessageBox.Show("Mismatch between Inputs and Expected Outputs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        return CheckStatus(theDay.BatchStatus) || !theDay.Expecteds.Any() ? new() { theDay.StatusText } : theDay.Expecteds;
    }

    private string GetInput()
    {
        theDay.CurrentInput = int.Parse(inputNumber.Text);
        return CheckStatus(theDay.BatchStatus) ? theDay.StatusText : string.Join('¶', theDay.Inputs);
    }

    private List<List<string>> GetInputs()
    {
        return CheckStatus(theDay.BatchStatus) ? new() : theDay.AllInputs;
    }

    private static bool CheckStatus(Day.DayBatchStatus status)
    {
        return status switch
        {
            Day.DayBatchStatus.NotDoneYet
            or Day.DayBatchStatus.NonCoded
            or Day.DayBatchStatus.NoTestData
            or Day.DayBatchStatus.NoPart2
            or Day.DayBatchStatus.Future
            or Day.DayBatchStatus.NoInputs => true,
            _ => false,
        };
    }

    private static bool TryGetDefaults(out (int year, int day, int puzzle, bool testMode) defaults, out string errorMessage)
    {
        defaults = (2015, 1, 2, true);
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

    private string DoPuzzle()
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
        if (!TryGetDefaults(out (int year, int day, int puzzle, bool testMode) defaults, out string errorMessage))
        {
            MessageBox.Show($"Error in defaults file\r\n\r\n{errorMessage}");
        }
        else
        {
            updYear.Value = defaults.year;
            updDay.Value = defaults.day;
            updPuzzle.Value = defaults.puzzle;
            chkTestMode.Checked = defaults.testMode;
        }
        noReset = false;
        ResetScreen();
    }

    private void BatchTest() => BatchTest(1);

    private void BatchTest(int reps)
    {
        btnBatch.Enabled = false;
        btnSuperBatch.Enabled = false;
        btnProcess.Enabled = false;
        btnExit.Text = "&Cancel";
        BatchWorker.RunWorkerAsync(reps);
    }

    private string DoBatch(int reps, BackgroundWorker worker)
    {
        int year = (int)updYear.Value;
        StringBuilder output = new();
        Debug.WriteLine($"Starting Batch Run for {year}");
        output.AppendLine($"Starting Batch Run for {year}");
        output.AppendLine("\r\n    Day          Part1           Part2");
        DateTime overallStart = DateTime.Now;
        for (int day = 1; day <= 25; day++)
        {
            for (int puzzle = 1; puzzle <= 2; puzzle++)
            {
                if (worker.CancellationPending)
                    return output.ToString();
                worker.ReportProgress(((day - 1) * 2) + puzzle, $"{day}/{puzzle}");
                Stopwatch sw = Stopwatch.StartNew();
                try
                {
                    theDay = (Day)Activator.CreateInstance(Type.GetType($"Advent{year}.Day{day:D2})"), new object[] { chkTestMode.Checked, puzzle });
                }
                catch
                {
                    theDay = (Day)Activator.CreateInstance(Type.GetType($"Advent{year}.Day{day:D2}"));
                    theDay.SetMode(chkTestMode.Checked, puzzle);
                }

                if (theDay.BatchStatus == Day.DayBatchStatus.NotDoneYet || theDay.BatchStatus == Day.DayBatchStatus.Future || theDay.BatchStatus == Day.DayBatchStatus.NoInputs) continue;
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
                        for (int rep = 0; rep < reps; rep++)
                        {
                            if (worker.CancellationPending) break;
                            theDay.DoWork();
                            totalTime += sw.ElapsedTicks;
                            sw.Restart();
                        }
                        if (theDay.Output != theDay.Expecteds[0] && theDay.BatchStatus != Day.DayBatchStatus.ManualIntervention)
                        {
                            output.Append("     Test Failed)");
                            Debug.WriteLine($"Day: {day}, part {puzzle} Test Failed");
                        }
                        else
                        {
                            output.Append(FormatTime(totalTime, reps));
                            Debug.WriteLine($"Day: {day}, part {puzzle}{FormatTime(totalTime, reps)}");
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
        output.AppendLine($"Total Time = {(DateTime.Now - overallStart).TotalSeconds / reps:0.000000} secs");
        Debug.WriteLine($"Total Time = {(DateTime.Now - overallStart).TotalSeconds / reps:0.000000} secs");
        //Clipboard.SetText(output.ToString());
        return output.ToString();
    }

    private static string FormatTime(double ticks, int reps)
    {
        string displayTime;
        double repSeconds = ticks / Stopwatch.Frequency / reps;
        displayTime = repSeconds switch
        {
            >= 60 => $"{repSeconds / 60.0:0.000} mins",
            >= 1 => $"{repSeconds:0.000} secs",
            >= 0.001 => $"{repSeconds * 1000:0.000} ms",
            _ => $"{repSeconds * 1000000:0} µs"
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
        startTime = DateTime.Now;
        Cursor = Cursors.WaitCursor;
        txtOutput.Text = "";
        Update();
        txtOutput.Text = DoPuzzle();
        txtOutput.BackColor = txtOutput.Text == txtExpected.Text || txtExpected.Text == "NotDoneYet" ? Color.White : Color.Pink;
        double timeTaken = (DateTime.Now - startTime).TotalSeconds;
        if (timeTaken >= 1)
            txtTimeTaken.Text = $"{timeTaken:0.000000} secs";
        else if (timeTaken >= 0.001)
            txtTimeTaken.Text = $"{timeTaken * 1000:0.000} ms";
        else
            txtTimeTaken.Text = $"{timeTaken * 1000000:0} µs";
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

    private void SuperBatch_Click(object sender, EventArgs e) => BatchTest(100);

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

    private void BatchWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
    {
        BackgroundWorker worker = sender as BackgroundWorker;
        e.Result = DoBatch((int)e.Argument, worker);
    }

    private void BatchWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
    {
        Progress.Value = e.ProgressPercentage;
        ProgressText.Text = e.UserState.ToString();
    }

    private void BatchWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
    {
        ShowText(e.Result.ToString());
        btnBatch.Enabled = true;
        btnSuperBatch.Enabled = true;
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
            Centre = this.Bounds,
        };
        showMe.ShowDialog();
    }

    private void Website_Click(object sender, EventArgs e)
    {
        var psi = new ProcessStartInfo
        {
            FileName = $"https://adventofcode.com/{updYear.Value}/day/{updDay.Value}",
            UseShellExecute = true
        };
        Process.Start(psi);
    }
}
