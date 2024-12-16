namespace Advent;

public abstract partial class Day
{
    private readonly Dictionary<string, DateTime> logs = new();
    private string CurTime(string message) => (DateTime.Now - logs[message]).TotalSeconds.ToString("N3");
    protected void LogRelative(string message) => WriteLog(string.Format("{0}: {1} secs", message, CurTime(message)));
    protected void LogStartRelative(string message) => LogRelative(message + " started");
    protected void LogEndRelative(string message) => LogRelative(message + " completed");
    protected void LogStart(string message)
    {
        logs.Add(message, DateTime.Now);
        WriteLog(string.Format("{0} started", message));
    }
    protected void LogEnd(string message)
    {
        if (logs.ContainsKey(message))
        {
            WriteLog(string.Format("{0} completed: {1} secs", message, CurTime(message)));
        }
    }

    protected bool ShowLogs = false;
    protected void WriteLog(string message)
    {
        if (!ShowLogs) return;
        Debug.WriteLine(message);
    }

}
