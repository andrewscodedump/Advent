namespace Advent2020;
public partial class Day01 : Advent.Day
{
    public override void DoWork()
    {
        int result = 0;
        int[] numbers = InputSplit.Select(n => int.Parse(n)).ToArray();

        for (int i = 0; i < numbers.Length; i++)
            for (int j = i + 1; j < numbers.Length; j++)
                for (int k = j + 1; k < (Part1 ? j + 2 : numbers.Length); k++)
                    if (numbers[i] + numbers[j] + (Part1 ? 0 : numbers[k]) == 2020)
                    {
                        result = numbers[i] * numbers[j] * (Part1 ? 1 : numbers[k]);
                        goto foundOne;
                    }
                foundOne:
        Output = result.ToString();
    }
}
