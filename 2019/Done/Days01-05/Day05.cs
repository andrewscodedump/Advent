namespace Advent2019;

public partial class Day05 : Advent.Day
{
    public override void DoWork()
    {
        int input = Part1 ? 1 : TestMode ? 8 : 5;
        IntCode code = new(Input, input);
        code.RunCode();
        Output = code.Output.ToString();
    }
}
