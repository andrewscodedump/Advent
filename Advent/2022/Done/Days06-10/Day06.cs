namespace Advent2022;

public partial class Day06 : Advent.Day
{
    public override void DoWork()
    {
        int len = Part1 ? 4 : 14, pos = len;
        for (; pos < Input.Length; pos++)
        {
            string packet = Input[(pos - len)..pos];
            if (!packet.GroupBy(c => c).Any(g => g.Count() > 1))
                break;
        }

        Output = pos.ToString();
    }
}
