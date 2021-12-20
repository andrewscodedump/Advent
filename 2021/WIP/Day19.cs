namespace Advent2021;

public partial class Day19 : Advent.Day
{
    public override void DoWork()
    {
        List<List<string>> scannersIn = GetInputsByBlankLine();
        List<List<int[]>> scanners = new();
        foreach(List<string> scanner in scannersIn)
        {
            scanner.RemoveAt(0);
            List<int[]> positions = new();
            scanner.ForEach(line => positions.Add(line.Split(',').Select(int.Parse).ToArray()));
            scanners.Add(positions);
        }

        HashSet<(int, int, int)> beacons = new();

        // Assumption - there is only one orientation for each scanner that matches 12 beacons with the reference scanner

        List<int[]> referenceScanner = scanners[0];

        Output = "OutputVariable".ToString();
    }
}
