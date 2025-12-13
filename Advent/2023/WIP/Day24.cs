namespace Advent2023;

public partial class Day24 : Advent.Day
{
    public override void DoWork()
    {
        long number = 0;
        long[][] hailstones = InputNumbers;
        (decimal lower, decimal upper) = TestMode ? (7, 27) : (200000000000000, 400000000000000);

        for(int i = 0; i < hailstones.Length-1; i++)
            for (int j = i + 1; j < hailstones.Length; j++)
            {
                (decimal x, decimal y, decimal t1, decimal t2) = Solver2D(hailstones[i], hailstones[j]);
                if (x > lower && x < upper && y > lower && y < upper && t1 > 0 && t2 > 0)
                    number++;
            }
        Output = number.ToString();
    }

    private static (decimal, decimal, decimal, decimal) Solver2D(long[] first, long[] second)
    {
        /* General formula

         a, b, c @ d, e, f
         g, h, i @ j, k, l

         x = (dhj - bdj + eaj - gdk) / (ej - dk)
         y = b + ex / d - ea / d
         t1 = (x - a) / d
         t2 = (x - g) / j
         */

        decimal a = first[0], b = first[1], d = first[3], e = first[4];
        decimal g = second[0], h = second[1], j = second[3], k = second[4];

        if ((e * j) == (d * k)) return (decimal.MaxValue, decimal.MaxValue, -1, -1); // Parallel
        decimal x = ((d * h * j) - (b * d * j) + (e * a * j) - (g * d * k)) / ((e * j) - (d * k));
        decimal y = b + (e * x / d) - (e * a / d);
        decimal t1 = (x - a) / d;
        decimal t2 = (x - g) / j;

        return (x, y, t1, t2);
    }
}
