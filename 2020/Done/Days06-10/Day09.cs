namespace Advent2020;

public partial class Day09 : Advent.Day
{
    public override void DoWork()
    {
        long[] numbers = InputSplit.Select(long.Parse).ToArray();
        int range = TestMode ? 5 : 25, pos = 0, start = 0, end = 1;
        long sum;

        for (pos = range + 1; pos < numbers.Length; pos++)
        {
            bool valid = false;
            for (int i = pos - range; i < pos; i++)
                for (int j = i + 1; j < pos; j++)
                    if (valid = numbers[i] + numbers[j] == numbers[pos]) goto valid;
                    valid: if (!valid) break;
        }
        long target = numbers[pos];

        if (WhichPart == 2)
            while ((sum = numbers[start..end].Sum()) != target)
            {
                if (sum > target) start++;
                if (sum < target) end++;
            }

        Output = (WhichPart == 1 ? target : numbers[start..end].Min() + numbers[start..end].Max()).ToString();
    }
}
