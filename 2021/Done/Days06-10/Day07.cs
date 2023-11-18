namespace Advent2021;

public partial class Day07 : Advent.Day
{
    public override void DoWork()
    {
        Dictionary<long, long> positions = new();
        for (int pos = (int)InputNumbersSingle.Min(); pos <= InputNumbersSingle.Max(); pos++)
        {
            if ((Part1 && !InputNumbersSingle.Contains(pos)) || positions.ContainsKey(pos)) continue;
            positions[pos] = 0;
            foreach (long sub in InputNumbersSingle)
            {
                long moves = Math.Abs(sub - pos);
                positions[pos] += Part1 ? moves : moves * (moves + 1) / 2;
            }
        }
        Output = positions.Values.Min().ToString();
    }
}
