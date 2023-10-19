namespace Advent2017;

public partial class Day02 : Advent.Day
{
    public override void DoWork()
    {
        int total = 0, min = int.MaxValue, max = 0;
        foreach (string line in InputSplit)
        {
            int[] numbers = line.Split(' ').Select(int.Parse).ToArray();
            if (Part1)
            {
                foreach (int number in numbers)
                    (min, max) = (Math.Min(min, number), Math.Max(max, number));
                total += max - min;
            }
            else
                for (int i = 0; i < numbers.Length; i++)
                    for (int j = 0; j < numbers.Length; j++)
                    {
                        int num1 = numbers[i], num2 = numbers[j];
                        if (i != j)
                            if (num1 % num2 == 0)
                            {
                                total += Math.Max(num1, num2) / Math.Min(num1, num2);
                                break;
                            }
                    }
        }
        Output = total.ToString();
    }
}
