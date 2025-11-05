namespace Everybody2025;

public class Day02 : Advent.Day
{
    public override void DoWork()
    {
        (long r, long i) seed = (InputNumbers[0][0], InputNumbers[0][1]);

        switch (WhichPart)
        {
            case 1:
                (long r, long i) = Process((10,10), seed, 3);
                Output = $"[{r},{i}]";
                break;
            case 2:
                Output = Mandelise(seed, 10).ToString();
                break;
            case 3:
                Output = Mandelise(seed, 1).ToString();
                break;
        }
    }

    private static (long r, long i) Add((long r, long i) a, (long r, long i) b) => (a.r + b.r, a.i + b.i);
    private static (long r, long i) Square((long r, long i) a) => ((a.r * a.r) - (a.i * a.i), (a.r * a.i) + (a.i * a.r));
    private static (long r, long i) Div((long r, long i) a, (long r, long i) b) => (a.r / b.r, a.i / b.i);

    private static (long r, long i) Process((long r, long i) divisor, (long r, long i) seed, int repetitions)
    {
        (long r, long i) result = (0, 0);
        for (int j = 0; j < repetitions; j++)
        {
            result = Square(result);
            result = Div(result, divisor);
            result = Add(result, seed);
            if (Math.Abs(result.r) > 1000000 || Math.Abs(result.i) > 1000000) return result;
        }
        return result;
    }

    private static int Mandelise((long r, long i) coords, int granularity)
    {
        int count = 0;
        for (long j = coords.r; j <= coords.r + 1000; j += granularity)
        {
            for (long k = coords.i; k <= coords.i + 1000; k += granularity)
            {
                (long r, long i) = Process((100000, 100000), (j, k), 100);
                if (Math.Abs(r) < 1000000 && Math.Abs(i) < 1000000) count++;
            }
        }
        return count;
    }
}