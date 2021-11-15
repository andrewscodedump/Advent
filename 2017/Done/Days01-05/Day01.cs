namespace Advent2017;

public partial class Day01 : Advent.Day
{
    public override void DoWork()
    {
        int sum = 0;
        int offset = WhichPart == 1 ? 1 : Input.Length / 2;

        for (int pos = 0; pos < Input.Length; pos++)
            sum += Input[pos] == Input[(pos + offset) % Input.Length] ? int.Parse(Input[pos].ToString()) : 0;

        Output = sum.ToString();
    }
}
