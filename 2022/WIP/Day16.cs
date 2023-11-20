using Windows.ApplicationModel.Resources.Core;

namespace Advent2022;

public partial class Day16 : Advent.Day
{
    public override void DoWork()
    {
        int bestFlow = 0;
        Queue<CurrentStatus> bfs = new();
        Dictionary<String, Location> map = PopulateMap();
        List<string> ClosedValves = new();
        bfs.Enqueue(new("AA", 0, 0, 30, ClosedValves));

        do
        {

        } while (bfs.Count > 0);

        Output = bestFlow.ToString();
    }
    private class CurrentStatus
    {
        public CurrentStatus(Location location, int flowperMinute, int totalFlow, int timeLeft, List<string> closedValves)
        {
            Location = location;
            FlowperMinute = flowperMinute;
            TotalFlow = totalFlow;
            TimeLeft = timeLeft;
            ClosedValves = closedValves;
        }

        public Location Location { get; set; }
        public int FlowperMinute { get; set; }
        public int TotalFlow { get; set; }
        public int TimeLeft { get; set; }
        public List<string> ClosedValves { get; set; }
    }

    public class Location
    {
        public Location(string name, List<string> targets, int valveSize)
        {
            Name = name;
            Targets = targets;
            ValveSize = valveSize;
        }

        public string Name { get; set; }
        public List<string> Targets { get; set; }
        public int ValveSize { get; set; }
        
    }

    public Dictionary<String, Location> PopulateMap()
    {
        Dictionary<string, Location> map = new();
        foreach (string tunnel in InputSplit)
        {
            string[] words = tunnel.Split(new char[]{ ' ',';','=',';',',' });
            Location loc = new(words[1], words[10..].ToList(), int.Parse(words[5]));
            map[words[1]] = loc;
        }
        return map;
    }
}
