namespace Advent2021;

public partial class Day07 : Advent.Day
{
    public override void DoWork()
    {
        Dictionary<long, long> positions = [];
        for (int pos = (int)InputNumbers[0].Min(); pos <= InputNumbers[0].Max(); pos++)
        {
            if ((Part1 && !InputNumbers[0].Contains(pos)) || positions.ContainsKey(pos)) continue;
            positions[pos] = 0;
            foreach (long sub in InputNumbers[0])
            {
                long moves = Math.Abs(sub - pos);
                positions[pos] += Part1 ? moves : moves * (moves + 1) / 2;
            }
        }
        Output = positions.Values.Min().ToString();
    }
}
