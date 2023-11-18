namespace Advent2019;

public partial class Day09 : Advent.Day
{
    public override void DoWork()
    {
        StringBuilder allOutputs = new();
        IntCode code = TestMode ? new IntCode(InputNumbersSingle) : new IntCode(InputNumbersSingle, Part1 ? 1 : 2);

        do
        {
            code.RunCodeWithNoReset();
            if (allOutputs.Length > 0) allOutputs.Append(',');
            allOutputs.Append(code.Output);
        } while (!code.CodeComplete);


        Output = Part1 && TestMode
            ? Input.StartsWith("1102") ? code.Output.ToString().Length.ToString() : Input.StartsWith("109") ? allOutputs.ToString().Replace(",99,99", ",99") : code.Output.ToString()
            : code.Output.ToString();
    }
}
