namespace Advent2016;

public partial class Day03 : Advent.Day
{
    public override void DoWork()
    {
        long valid = 0, a, b, c;

        if (Part1)
            InputNumbers.ForEach(triangle =>
            {
                (a, b, c) = (triangle[0], triangle[1], triangle[2]);
                if (a + b > c && b + c > a && a + c > b) valid++;
            });
        else
            for (int i = 0; i < Inputs.Length; i += 3)
                for (int j = 0; j < 3; j++)
                {
                    (a, b, c) = (InputNumbers[i][j], InputNumbers[i + 1][j], InputNumbers[i + 2][j]);
                    if (a + b > c && b + c > a && a + c > b) valid++;
                }
        Output = valid.ToString();
    }
}
