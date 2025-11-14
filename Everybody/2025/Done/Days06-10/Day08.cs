namespace Everybody2025;
public class Day08 : Advent.Day
{
    public override void DoWork()
    {

        long[] order = InputNumbers[0];
        int result = 0;
        List<(long first, long second)> chords = [];

        switch (WhichPart)
        {
            case 1:
                int number = TestMode ? 8 : 32;
                for (int i = 0; i < order.Length - 1; i++)
                {
                    if (Math.Abs(order[i] - order[i + 1]) == number / 2) result++;
                }
                Output = result.ToString();
                break;
            case 2:
                for (int i = 0; i < order.Length - 1; i++)
                {
                    (long, long) newString = (Math.Min(order[i], order[i + 1]), Math.Max(order[i], order[i + 1]));
                    result += CountKnots(newString, chords);
                    chords.Add(newString);
                }
                Output = result.ToString();
                break;
            case 3:
                number = TestMode ? 8 : 256;    
                for (int i = 0; i < order.Length - 1; i++)
                {
                    (long a, long b) = (Math.Min(order[i], order[i + 1]), Math.Max(order[i], order[i + 1]));
                    chords.Add((a, b));
                }
                for (int i = 1; i <= number; i++)
                    for (int j = i + 1; j <= number; j++)
                        result = Math.Max(result, CountStrings((i, j), chords, true));
                Output = result.ToString();
                break;
        }
    }
    private static bool Between (long a, (long c, long d) pair)
    {
        if (a == pair.c || a == pair.d) return false;
        return a > pair.c && a < pair.d;
    }

    private static int CountKnots((long a, long b) test, List<(long c, long d)> chords) => CountStrings(test, chords, false);

    private static int CountStrings((long a, long b) test, List<(long c, long d)> chords, bool includeSelf)
    {
        int result = 0;
        foreach ((long c, long d) in chords)
        {
            if (includeSelf && (((c,d) == test) || ((d, c) == test))) result++;
            if (test.a == c || test.a == d || test.b == c || test.b == d) continue;
            if (Between(test.a, (c, d)) != Between(test.b, (c, d))) result++;
        }
        return result;
    }
}
