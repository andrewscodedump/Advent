namespace Everybody2025;

public class Day16 : Advent.Day
{
    public override void DoWork()
    {
        long[] input = [.. InputNumbers[0]];
        switch (WhichPart) {
            case 1:
                Output = BricksForLength(input, 90).ToString();
                break;
            case 2:
                Output = GetSpell(input).Aggregate((long)1, (a, b) => b * a).ToString();
                break;
            case 3:
                Output = BinarySearch(GetSpell(input), 202520252025000).ToString();
                break;
        }
    }

    private static long[] GetSpell(long[] fragment)
    {
        List<long> spell = [];
        for (int i = 1; i <= fragment.Length; i++)
        {
            if (fragment[i - 1] == 0) continue;
            spell.Add(i);
            for (int j = i; j <= fragment.Length; j += i)
            {
                fragment[j - 1]--;
            }
        }
        return [.. spell];
    }

    private static long BricksForLength(long[] spell, long length) => spell.Sum(v => length / v);

    private static long BinarySearch(long[] spell, long target)
    {
        long lower = 0, upper = target, curr = upper;
        do
        {
            long check = BricksForLength(spell, curr);
            if (check == target) break;
            if (check < target)
                lower = curr;
            else // check > target
                upper = curr;
            curr = lower + ((upper - lower) / 2);
        } while (upper - lower > 1);
        return curr;
    }
    
    private long Fibonacci(int n)
    {
        // Wound up doing this my mistake at one point and don't actually use it, but keep it anyway because it's pretty cool
        return (long)(1 / Math.Sqrt(5) * (Math.Pow((1 + Math.Sqrt(5)) / 2, n) - Math.Pow((1 - Math.Sqrt(5)) / 2, n)));
    }
}