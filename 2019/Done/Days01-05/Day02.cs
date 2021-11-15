namespace Advent2019;

public partial class Day02 : Advent.Day
{
    public override void DoWork()
    {
        IntCode code = WhichPart == 1 && !TestMode ? new(Input, 12, 2) : new(Input);
        Output = (WhichPart == 1 ? code.RunCode() : code.FindResult(19690720)).ToString();
    }
}
