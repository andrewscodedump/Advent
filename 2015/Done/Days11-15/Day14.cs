namespace Advent2015;

public partial class Day14 : Advent.Day
{
    public override void DoWork()
    {
        int totalTime = TestMode ? 1000 : 2503;
        Dictionary<string, int> distances = [];
        Dictionary<string, int> points = [];
        List<(string, int, int, int)> reindeer = [];
        foreach (string statsBase in Inputs)
        {
            string stats = statsBase.Replace(" can fly ", " ").Replace(" km/s for ", " ").Replace(" seconds, but then must rest for ", " ").Replace(" seconds.", "");
            reindeer.Add((stats.Split(' ')[0], int.Parse(stats.Split(' ')[1]), int.Parse(stats.Split(' ')[2]), int.Parse(stats.Split(' ')[3])));
            points[stats.Split(' ')[0]] = 0;
        }

        for (int time = 1; time <= totalTime; time++)
        {
            int maxDist = 0;
            foreach ((string name, int speed, int duration, int rest) in reindeer)
            {
                int wholePeriods = time / (duration + rest);
                int remainder = time % (duration + rest);
                distances[name] = (remainder <= duration) ? (wholePeriods * speed * duration) + (remainder * speed) : (wholePeriods * speed * duration) + (duration * speed);
                maxDist = Math.Max(distances[name], maxDist);
            }
            foreach (string deer in distances.Keys)
                if (distances[deer] == maxDist)
                    points[deer]++;
        }
        Output = (Part1 ? distances.Values.Max() : points.Values.Max()).ToString();
    }
}
