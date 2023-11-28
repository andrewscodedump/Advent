namespace Advent2022;

public partial class Day06 : Advent.Day
{
    public override void DoWork()
    {
        int len = Part1 ? 4 : 14, pos = len;
        for (; pos < Inputs[0].Length; pos++)
        {
            string packet = Inputs[0][(pos - len)..pos];
            if (!packet.GroupBy(c => c).Where(g => g.Count() > 1).Any())
                break;
        }

        Output = pos.ToString();
    }
}
