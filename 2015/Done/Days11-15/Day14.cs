namespace Advent2015;

public partial class Day14 : Advent.Day
{
    public override void DoWork()
    {
        Input = Input.Replace(" can fly ", " ").Replace(" km/s for ", " ").Replace(" seconds, but then must rest for ", " ").Replace(" seconds.", "");

        int totalTime = TestMode ? 1000 : 2503;
        Dictionary<string, int> distances = new();
        Dictionary<string, int> points = new();
        List<(string, int, int, int)> reindeer = new();
        foreach (string stats in InputSplit)
        {
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
