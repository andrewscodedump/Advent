#region Preamble

using System.ComponentModel;

[assembly: CLSCompliant(true)]
namespace Advent;

public partial class AdventOfCode : Form
{
    #endregion Preamble
    private readonly int defaultDay = 19;
    private readonly int defaultPuzzle = 1;
    private readonly bool defaultTestMode = true;
    private readonly int defaultYear = 2021;

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
        List<string> Inputs = GetInputs();
        List<string> Expecteds = GetExpecteds();
        if (Inputs.Count != Expecteds.Count)
        {
            MessageBox.Show("Mismatch between Inputs and Expected Outputs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Inputs = Inputs.GetRange(0, 1);
            #pragma warning disable IDE0059 // Unnecessary assignment of a value
            Expecteds = Expecteds.GetRange(0, 1);
            #pragma warning restore IDE0059 // Unnecessary assignment of a value
        }
        inputNumber.Text = "0";
        prevInput.Visible = Inputs.Count > 1;
        nextInput.Visible = Inputs.Count > 1;
        ResetInputs();
    }

    private void ResetInputs()
    {
        if (inputNumber.Text == "0")
        {
            txtInput.Text = GetInput();
            txtExpected.Text = GetExpected();
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
    }

    private string GetExpected()
    {
        return CheckStatus(theDay.BatchStatus, out string result) ? result : theDay.Expected;
    }
    private List<string> GetExpecteds()
    {
        return CheckStatus(theDay.BatchStatus, out List<string> results) ? results : theDay.Expecteds;
    }

    private string GetInput()
    {
        theDay.CurrentInput = int.Parse(inputNumber.Text);
        return CheckStatus(theDay.BatchStatus, out string result) ? result : theDay.Input;
    }

    private List<string> GetInputs()
    {
        return CheckStatus(theDay.BatchStatus, out List<string> results) ? results : theDay.Inputs;
    }

    private string DoPuzzle()
    {
        if (CheckStatus(theDay.BatchStatus, out string result))
            return result;
        else
        {
            theDay.DoWork();
            return theDay.Output;
        }
    }

    private static bool CheckStatus(Day.DayBatchStatus status, out string result) => CheckStatus(status, out result, out List<string> _);
    private static bool CheckStatus(Day.DayBatchStatus status, out List<string> results) => CheckStatus(status, out string _, out results);
    private static bool CheckStatus(Day.DayBatchStatus status, out string result, out List<string> results)
    {
        bool handled = true;
        switch (status)
        {
            case Day.DayBatchStatus.NotDoneYet:
                result = "Not Done Yet";
                break;
            case Day.DayBatchStatus.NonCoded:
                result = "Solved by non-code method";
                break;
            case Day.DayBatchStatus.NoTestData:
                result = "No Test Data";
                break;
            case Day.DayBatchStatus.NoPart2:
                result = "There is no part 2 for this puzzle";
                break;
            case Day.DayBatchStatus.Future:
                result = "Task is in the future - no inputs available";
                break;
            case Day.DayBatchStatus.NoInputs:
                result = "No inputs available";
                break;
            default:
                result = string.Empty;
                handled = false;
                break;
        }
        results = new List<string> { result };
        return handled;
    }

    private void SetupForm()
    {
        noReset = true;
        updYear.Value = defaultYear;
        updDay.Value = defaultDay;
        updPuzzle.Value = defaultPuzzle;
        chkTestMode.Checked = defaultTestMode;
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
        Debug.WriteLine("Starting Batch Run for " + year);
        output.AppendLine("Starting Batch Run for " + year);
        DateTime overallStart = DateTime.Now;
        for (int day = 1; day <= 25; day++)
        {
            for (int puzzle = 1; puzzle <= 2; puzzle++)
            {
                if (worker.CancellationPending)
                    return output.ToString();
                worker.ReportProgress(((day - 1) * 2) + puzzle, $"{day}/{puzzle}");
                DateTime start = DateTime.Now;
                try
                {
                    theDay = (Day)Activator.CreateInstance(Type.GetType($"Advent{year}.Day{day:D2})"), new object[] { chkTestMode.Checked, puzzle });
                }
                catch
                {
                    theDay = (Day)Activator.CreateInstance(Type.GetType($"Advent{year}.Day{day:D2}"));
                    theDay.SetMode(chkTestMode.Checked, (int)updPuzzle.Value);
                }

                if (theDay.BatchStatus == Day.DayBatchStatus.NotDoneYet || theDay.BatchStatus == Day.DayBatchStatus.Future || theDay.BatchStatus == Day.DayBatchStatus.NoInputs) continue;
                if (puzzle == 1)
                {
                    output.AppendLine();
                    output.Append("Day: " + day.ToString());
                }
                output.Append(", part " + puzzle.ToString() + " ");
                switch (theDay.BatchStatus)
                {
                    case Day.DayBatchStatus.NonCoded:
                        output.Append(" skipped (non-coded).");
                        Debug.WriteLine($"Day: {day} skipped (non-coded.");
                        break;
                    case Day.DayBatchStatus.NotDoneYet:
                        output.Append(" not done yet.");
                        Debug.WriteLine($"Day: {day} not done yet.");
                        break;
                    case Day.DayBatchStatus.NoTestData:
                        output.Append(" no test data.");
                        Debug.WriteLine($"Day: {day} no test data.");
                        break;
                    case Day.DayBatchStatus.NotWorking:
                        output.Append(" not currently working.");
                        Debug.WriteLine($"Day: {day} not currently working.");
                        break;
                    case Day.DayBatchStatus.Performance:
                        output.Append(" skipped (performance).");
                        Debug.WriteLine($"Day: {day} skipped (performance).");
                        break;
                    case Day.DayBatchStatus.NoPart2:
                        output.Append(" has no part 2.");
                        Debug.WriteLine($"Day: {day} has no part 2.");
                        break;
                    case Day.DayBatchStatus.Available:
                    case Day.DayBatchStatus.ManualIntervention:
                        theDay.BatchRun = true;
                        double totalTime = 0;
                        for (int rep = 0; rep < reps; rep++)
                        {
                            if (worker.CancellationPending) break;
                            theDay.DoWork();
                            totalTime += (DateTime.Now - start).TotalSeconds;
                            start = DateTime.Now;
                        }
                        if (theDay.Output != theDay.Expecteds[0] && theDay.BatchStatus != Day.DayBatchStatus.ManualIntervention)
                        {
                            output.Append(" Test Failed");
                            Debug.WriteLine($"Day: {day}, part {puzzle} Test Failed");
                        }
                        else
                        {
                            output.Append($" = {totalTime / reps:0.000000} secs");
                            Debug.WriteLine($"Day: {day}, part {puzzle} = {totalTime / reps:0.000000} secs");
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
        txtTimeTaken.Text = $"{(DateTime.Now - startTime).TotalSeconds:0.000000} secs";
        Cursor = Cursors.Default;
    }

    private void Exit_Click(object sender, EventArgs e)
    {
        if (btnExit.Text=="&Cancel")
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
        MessageBox.Show(e.Result.ToString(), "Batch Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
        btnBatch.Enabled = true;
        btnSuperBatch.Enabled = true;
        btnProcess.Enabled = true;
        btnExit.Text = "E&xit";
        Progress.Value = 0;
        ProgressText.Text = "";
    }
    #endregion Control methods
}
