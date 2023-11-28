namespace Advent2020;
public partial class Day01 : Advent.Day
{
    public override void DoWork()
    {
        long result = 0;

        for (int i = 0; i < InputNumbers.Length; i++)
            for (int j = i + 1; j < InputNumbers.Length; j++)
                for (int k = j + 1; k < (Part1 ? j + 2 : InputNumbers.Length); k++)
                    if (InputNumbers[i][0] + InputNumbers[j][0] + (Part1 ? 0 : InputNumbers[k][0]) == 2020)
                    {
                        result = InputNumbers[i][0] * InputNumbers[j][0] * (Part1 ? 1 : InputNumbers[k][0]);
                        goto foundOne;
                    }
                foundOne:
        Output = result.ToString();
    }
}
