namespace Advent2021;

public partial class Day07 : Advent.Day
{
    public override void DoWork()
    {
        Dictionary<int, int> positions = new();
        for (int pos = InputSplitCInt.Min(); pos <= InputSplitCInt.Max(); pos++)
        {
            if ((WhichPart == 1 && !InputSplitCInt.Contains(pos)) || positions.ContainsKey(pos)) continue;
            positions[pos] = 0;
            foreach (int sub in InputSplitCInt)
            {
                int moves = Math.Abs(sub - pos);
                positions[pos] += WhichPart == 1 ? moves : moves * (moves + 1) / 2;
            }
        }
        Output = positions.Values.Min().ToString();
    }
}
