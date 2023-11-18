namespace Advent2018;

public partial class Day23 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs
        List<Bot> bots = new();
        (int x, int y, int z) bestLocation = (0, 0, 0);
        int inRange = 0, maxInRange = 0, bestSum = 0;
        (int minX, int minY, int minZ, int maxX, int maxY, int maxZ) limits;
        int grain = TestMode ? 1 : (int)Math.Pow(2, 26);

        foreach (string input in Inputs)
        {
            string[] bits = input.Split(new char[] { '=', '<', '>', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            bots.Add(new Bot((int.Parse(bits[1]), int.Parse(bits[2]), int.Parse(bits[3])), int.Parse(bits[5])));
        }
        limits = (bots.Min(bot => bot.Location.X), bots.Min(bot => bot.Location.Y), bots.Min(bot => bot.Location.Z), bots.Max(bot => bot.Location.X), bots.Max(bot => bot.Location.Y), bots.Max(bot => bot.Location.Z));
        int xRange = limits.maxX - limits.minX, yRange = limits.maxY - limits.minY, zRange = limits.maxZ - limits.minZ;

        #endregion Setup Variables and Parse Inputs

        int inRangeOfBiggest = bots.Count(bot => bots.OrderByDescending(b => b.Radius).FirstOrDefault().InRange(bot));

        if (Part2)
            do
            {
                maxInRange = 0;
                bestSum = int.MaxValue;
                for (int x = limits.minX; x < limits.maxX; x += grain)
                    for (int y = limits.minY; y < limits.maxY; y += grain)
                        for (int z = limits.minZ; z < limits.maxZ; z += grain)
                            if ((inRange = bots.Count(bot => bot.InRange(x, y, z))) > maxInRange || (inRange == maxInRange && Math.Abs(x) + Math.Abs(y) + Math.Abs(z) < bestSum))
                            {
                                maxInRange = inRange;
                                bestLocation = (x, y, z);
                                bestSum = Math.Abs(x) + Math.Abs(y) + Math.Abs(z);
                            }
                //Debug.Print("Grain {0}: Location: {1}, {2}, {3} InRange: {4} Sum: {5}", grain, bestLocation.x, bestLocation.y, bestLocation.z, maxInRange, bestSum);
                grain /= 2; xRange /= 2; yRange /= 2; zRange /= 2;
                limits = (bestLocation.x - (xRange / 2), bestLocation.y - (yRange / 2), bestLocation.z - (zRange / 2), bestLocation.x + (xRange / 2), bestLocation.y + (yRange / 2), bestLocation.z + (zRange / 2));
            } while (grain >= 1);

        Output = (Part1 ? inRangeOfBiggest : bestSum).ToString();
    }

    #region Private Classes and Methods
    private class Bot
    {
        public (int X, int Y, int Z) Location { get; private set; }
        public int Radius { get; private set; }
        public Bot((int x, int y, int z) location, int radius) { Location = location; Radius = radius; }
        public bool InRange(int x, int y, int z) => Distance(x, y, z) <= Radius;
        public bool InRange(Bot otherBot) => InRange(otherBot.Location.X, otherBot.Location.Y, otherBot.Location.Z);
        public int Distance(int x, int y, int z) => Math.Abs(Location.X - x) + Math.Abs(Location.Y - y) + Math.Abs(Location.Z - z);
        public int Distance(Bot otherBot) => Distance(otherBot.Location.X, otherBot.Location.Y, otherBot.Location.Z);
    }
    #endregion Private Classes and Methods
}
