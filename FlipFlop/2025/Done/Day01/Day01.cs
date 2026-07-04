namespace FlipFlop2025;

public partial class Day01 : Advent.Day
{
    public override void DoWork()
    {
        int result = 0;
        foreach (string input in Inputs)
        {
            if (WhichPart == 3 && input.Contains("ne")) continue;
            int localResult = Regex.Matches(input, "ba|na|ne").Count;
            if (WhichPart == 2 && localResult % 2 == 1) continue;
            result += localResult;
        }
        Output = result;
    }
}
