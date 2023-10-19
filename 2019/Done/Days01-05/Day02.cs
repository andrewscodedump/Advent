namespace Advent2019;

public partial class Day02 : Advent.Day
{
    public override void DoWork()
    {
        IntCode code = Part1 && !TestMode ? new(Input, 12, 2) : new(Input);
        Output = (Part1 ? code.RunCode() : code.FindResult(19690720)).ToString();
    }
}
