namespace Advent2017;

public partial class Day01 : Advent.Day
{
    public override void DoWork()
    {
        int sum = 0;
        int offset = Part1 ? 1 : Inputs[0].Length / 2;

        for (int pos = 0; pos < Inputs[0].Length; pos++)
            sum += Inputs[0][pos] == Inputs[0][(pos + offset) % Inputs[0].Length] ? int.Parse(Inputs[0][pos].ToString()) : 0;

        Output = sum.ToString();
    }
}
