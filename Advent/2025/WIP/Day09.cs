namespace Advent2025;

public class Day09 : Advent.Day
{
    public override void DoWork()
    {
        long result = 0;
        List<(long, long)> corners = [.. InputNumbers.Select(i => (i[0], i[1]))];
        ((long, long), (long, long))[] pairs = [.. corners.SelectMany((x, i) => corners.Skip(i + 1), (a, b) => (a, b))];
        if (WhichPart == 2)
        {
            pairs = [.. pairs.Where(p => AllInside(corners, p))];
            pairs = [.. pairs.Where(p => DiagonalsIntersect(corners, p))];
        }
        result = pairs.Max(GetArea);

        Output = result;
    }

    private long GetArea(((long x, long y) a, (long x, long y) b) p)
    {
        return (Math.Abs(p.a.x - p.b.x) + 1) * (Math.Abs(p.a.y - p.b.y) + 1);
    }

    private static bool AllInside(List<(long x, long y)> corners, ((long x, long y) a, (long x, long y) b) p)
    {
        return IsInside((p.a.x, p.a.y), corners) && IsInside((p.a.x, p.b.y), corners) && IsInside((p.b.x, p.a.y), corners) && IsInside((p.b.x, p.b.y), corners);
    }

    private static bool DiagonalsIntersect(List<(long x, long y)> corners, ((long x, long y) a, (long x, long y) b) p)
    {
        return DiagonalIntersects(corners, (p.a, p.b)) || DiagonalIntersects(corners, ((p.a.x, p.b.y), (p.b.x, p.a.y)));
    }

    private static bool DiagonalIntersects(List<(long x, long y)> corners, ((long x, long y) a, (long x, long y) b) p)
    {
        // Check if a line intersects with an edge
        // (remembering to handle width or height = 1)
        return false;
    }

    private static bool IsInside((long x, long y) point, List<(long x, long y)> corners)
    {
        bool result = false;
        (long x, long y) a = corners.Last();
        foreach ((long x, long y) b in corners)
        {
            if ((b.x == point.x) && (b.y == point.y)) // test point is on the second corner
                return true;

            if ((b.y == a.y) && (point.y == a.y)) // the test point and the two corners are on the same vertical line
            {
                if (((a.x <= point.x) && (point.x <= b.x)) || ((b.x <= point.x) && (point.x <= a.x))) // and the test point is between the two corners
                    return true;
            }

            if (((b.y < point.y) && (a.y >= point.y)) || ((a.y < point.y) && (b.y >= point.y)))
            {
                if (b.x + ((point.y - (double)b.y) / (a.y - b.y) * (a.x - (double)b.x)) <= point.x) // we've crossed a vertical line - flip the result
                    result = !result;
            }
            a = b;
        }
        return result;
    }

}
