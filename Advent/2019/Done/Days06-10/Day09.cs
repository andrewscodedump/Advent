namespace Advent2019;

public partial class Day09 : Advent.Day
{
    public override void DoWork()
    {
        StringBuilder allOutputs = new();
        IntCode code = TestMode ? new IntCode(InputNumbers[0]) : new IntCode(InputNumbers[0], Part1 ? 1 : 2);

        do
        {
            code.RunCodeWithNoReset();
            if (allOutputs.Length > 0) allOutputs.Append(',');
            allOutputs.Append(code.Output);
        } while (!code.CodeComplete);


        Output = Part1 && TestMode
            ? InputNumbers[0][0] == 1102 ? code.Output.ToString().Length.ToString() : InputNumbers[0][0] == 109 ? allOutputs.ToString().Replace(",99,99", ",99") : code.Output.ToString()
            : code.Output.ToString();
    }
}
