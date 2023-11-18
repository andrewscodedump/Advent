namespace Advent2022;

public partial class Day02 : Advent.Day
{
    public override void DoWork()
    {
        Dictionary<string, (int wrong, int right)> scores = new() { { "A X", (4, 3) }, { "A Y", (8, 4) }, { "A Z", (3, 8) }, { "B X", (1, 1) }, { "B Y", (5, 5) }, { "B Z", (9, 9) }, { "C X", (7, 2) }, { "C Y", (2, 6) }, { "C Z", (6, 7) } };
        Output = Inputs.Select(combo => Part1 ? scores[combo].wrong : scores[combo].right).Sum().ToString();
    }
}
