namespace Advent2019;

public partial class Day12 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs
        ((long x, long y, long z) position, (long x, long y, long z) velocity)[] planets = new ((long, long, long), (long, long, long))[4];
        long totalEnergy;
        int iterations = 0, limit = TestMode ? 100 : 1000;
        for (int p = 0; p < InputNumbers.Count; p++)
        //for (int p = 0; p < InputSplit.Length; p++)
        {
            //string[] coords = InputSplit[p].Split(new char[] { ',', 'x', 'y', 'z', '=', ' ', '<', '>' }, StringSplitOptions.RemoveEmptyEntries);
            //planets[p] = ((int.Parse(coords[0]), int.Parse(coords[1]), int.Parse(coords[2])), (0, 0, 0));
            planets[p] = ((InputNumbers[p][0], InputNumbers[p][1], InputNumbers[p][2]), (0, 0, 0));
        }
        (long, long, long, long, long, long, long, long)[] originalStates = { (planets[0].position.x, planets[1].position.x, planets[2].position.x, planets[3].position.x, 0, 0, 0, 0) ,
            (planets[0].position.y, planets[1].position.y, planets[2].position.y, planets[3].position.y, 0, 0, 0, 0),
            (planets[0].position.z, planets[1].position.z, planets[2].position.z, planets[3].position.z, 0, 0, 0, 0)};
        (long, long, long, long, long, long, long, long)[] states = { (0, 0, 0, 0, 0, 0, 0, 0), (0, 0, 0, 0, 0, 0, 0, 0), (0, 0, 0, 0, 0, 0, 0, 0) };
        int xLoop = 0, yLoop = 0, zLoop = 0;
        if (TestMode && Part1 && planets[0].position.x == -1) limit = 10;
        #endregion Setup Variables and Parse Inputs

        do
        {
            for (int a = 0; a < 4; a++)
                for (int b = a + 1; b < 4; b++)
                    GetSpeeds(ref planets[a], ref planets[b]);

            totalEnergy = 0;
            for (int a = 0; a < 4; a++)
            {
                planets[a].position = (planets[a].position.x + planets[a].velocity.x, planets[a].position.y + planets[a].velocity.y, planets[a].position.z + planets[a].velocity.z);
                totalEnergy += (Math.Abs(planets[a].position.x) + Math.Abs(planets[a].position.y) + Math.Abs(planets[a].position.z)) * (Math.Abs(planets[a].velocity.x) + Math.Abs(planets[a].velocity.y) + Math.Abs(planets[a].velocity.z));
            }
            iterations++;
            states[0] = (planets[0].position.x, planets[1].position.x, planets[2].position.x, planets[3].position.x, planets[0].velocity.x, planets[1].velocity.x, planets[2].velocity.x, planets[3].velocity.x);
            states[1] = (planets[0].position.y, planets[1].position.y, planets[2].position.y, planets[3].position.y, planets[0].velocity.y, planets[1].velocity.y, planets[2].velocity.y, planets[3].velocity.y);
            states[2] = (planets[0].position.z, planets[1].position.z, planets[2].position.z, planets[3].position.z, planets[0].velocity.z, planets[1].velocity.z, planets[2].velocity.z, planets[3].velocity.z);
            if (xLoop == 0 && states[0] == originalStates[0]) xLoop = iterations;
            if (yLoop == 0 && states[1] == originalStates[1]) yLoop = iterations;
            if (zLoop == 0 && states[2] == originalStates[2]) zLoop = iterations;
            if (xLoop != 0 && yLoop != 0 && zLoop != 0) break;
        } while (Part2 || iterations < limit);

        long repeatTime = Part1 ? 0 : Lcm(Lcm(xLoop, yLoop), zLoop);

        Output = (Part1 ? totalEnergy : repeatTime).ToString();
    }

    #region Private Classes and Methods

    static long Gcf(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    static long Lcm(long a, long b)
    {
        return a / Gcf(a, b) * b;
    }

    private static void GetSpeeds(ref ((long x, long y, long z) pos, (long x, long y, long z) vel) a, ref ((long x, long y, long z) pos, (long x, long y, long z) vel) b)
    {
        int xDiff = a.pos.x == b.pos.x ? 0 : a.pos.x > b.pos.x ? 1 : -1;
        int yDiff = a.pos.y == b.pos.y ? 0 : a.pos.y > b.pos.y ? 1 : -1;
        int zDiff = a.pos.z == b.pos.z ? 0 : a.pos.z > b.pos.z ? 1 : -1;
        a.vel = (a.vel.x - xDiff, a.vel.y - yDiff, a.vel.z - zDiff);
        b.vel = (b.vel.x + xDiff, b.vel.y + yDiff, b.vel.z + zDiff);
    }

    #endregion Private Classes and Methods
}
