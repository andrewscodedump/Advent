namespace Advent2019;

public partial class Day10 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs


        List<(int, int)> asteroids = new();
        Dictionary<(int, int), int> counts = new();
        int maxCount = 0;
        (int x, int y) baseLocation = (0, 0);
        List<((int x, int y) pos, double angle, int distance)> targets = new();
        double currentAngle = -1;
        Dictionary<((int, int), (int, int)), double> angles = new();
        int height = InputSplit.Length, width = InputSplit[0].Length;
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                if (InputSplit[y][x] == '#')
                {
                    asteroids.Add((x, y));
                    counts.Add((x, y), 0);
                }
            }
        foreach ((int x, int y) a in asteroids)
        {
            foreach ((int x, int y) b in asteroids)
            {
                if (a == b) continue;
            }
        }

        #endregion Setup Variables and Parse Inputs

        foreach ((int x, int y) a in asteroids)
        {
            foreach ((int x, int y) b in asteroids)
            {
                if (a == b) continue;
                bool canSee = true;
                foreach ((int x, int y) c in asteroids)
                {
                    if (a == c || b == c) continue;

                    if (IsBlocked(a, b, c))
                    {
                        canSee = false;
                        break;
                    }
                }
                counts[a] += canSee ? 1 : 0;
            }
            if (counts[a] > maxCount)
            {
                maxCount = Math.Max(counts[a], maxCount);
                baseLocation = a;
            }
        }

        asteroids.Remove(baseLocation);
        foreach ((int x, int y) asteroid in asteroids)
            targets.Add((asteroid, GetAngle(baseLocation, asteroid), Math.Abs(baseLocation.x - asteroid.x) + Math.Abs(baseLocation.y - asteroid.y)));

        int numberKilled = 0;
        ((int x, int y) loc, double angle, int distance) nextTarget = ((0, 0), 0, 0);
        do
        {
            nextTarget = targets.Where(x => x.angle > currentAngle).OrderBy(x => x.angle).ThenBy(x => x.distance).FirstOrDefault();
            if (nextTarget == ((0, 0), 0, 0))
                currentAngle = -1;
            else
            {
                currentAngle = nextTarget.angle;
                targets.Remove(nextTarget);
                numberKilled++;
            }
        } while (numberKilled < 200);

        Output = (Part1 ? maxCount : (nextTarget.loc.x * 100) + nextTarget.loc.y).ToString();
    }

    #region Private Classes and Methods

    private static double GetAngle((int x, int y) a, (int x, int y) b)
    {
        double angle = Math.Atan2((double)(b.x - a.x), a.y - b.y) / Math.PI * 180;
        if (angle < 0) angle = 360 + angle;
        return angle;
    }

    private static bool IsBlocked((int x, int y) a, (int x, int y) b, (int x, int y) c)
    {
        return (c.x <= a.x || c.x <= b.x) && (c.x >= a.x || c.x >= b.x) && (c.y <= a.y || c.y <= b.y) && (c.y >= a.y || c.y >= b.y)
&& ((c.x == a.x && c.x == b.x) || (c.y == a.y && c.y == b.y)
|| (double)(a.x - b.x) / (double)(a.y - b.y) == (double)(a.x - c.x) / (double)(a.y - c.y));
        //return true;
    }

    #endregion Private Classes and Methods
}
