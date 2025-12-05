namespace Advent2025;

public class Day02 : Advent.Day
{
    public override void DoWork()
    {
        long result = 0;
        long[] numbers = [.. Input.Split([',', '-']).Select(long.Parse)];
        for (int i = 0; i < numbers.Length; i += 2)
            result += LongRange(numbers[i], numbers[i + 1]).Where(HasRepeats).Sum();
        Output = result.ToString();
    }

    private bool HasRepeats(long num)
    {
        string s = num.ToString();
        int len = s.Length;
        for (int n = 1; n <= len / 2; n++)
        {
            if ((WhichPart == 1 && n != len / 2.0) || len % n != 0) continue;
            if (string.Concat(Enumerable.Repeat(s[..n], len / n)) == s) return true;
        }
        return false;
    }
}