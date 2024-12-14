namespace Advent2024;

public partial class Day14 : Advent.Day
{
    public override void DoWork()
    {
        long[][] robots = InputNumbers.Select(s => s.ToArray()).ToArray();
        int width = TestMode ? 11 : 101, height = TestMode ? 7 : 103;
        long result = 1;

        int time = 1;
        do
        {
            foreach (long[] robot in robots)
            {
                robot[0] = (robot[0] + robot[2]) % width;
                if (robot[0] < 0) robot[0] += width;
                robot[1] = (robot[1] + robot[3]) % height;
                if (robot[1] < 0) robot[1] += height;
            }
            if (Part2)
            {
                string[] map = Enumerable.Range(0, height).Select(n => new string('.', width)).ToArray();
                foreach (long[] robot in robots)
                    map[robot[1]] = string.Concat(map[robot[1]].AsSpan(0, (int)robot[0]), "*", map[robot[1]].AsSpan((int)robot[0] + 1));
                if (map.Count(l => FiveStars().IsMatch(l)) > 3)
                    result = time;
            }
            time++;
        } while ((Part1 && time <= 100) || (Part2 && result == 1));
        if (result == 1)
        {
            result *= robots.Count(r => r[0] < width / 2 && r[1] < height / 2)
                * robots.Count(r => r[0] < width / 2 && r[1] > height / 2)
                * robots.Count(r => r[0] > width / 2 && r[1] < height / 2)
                * robots.Count(r => r[0] > width / 2 && r[1] > height / 2);
        }

        Output = result.ToString();
    }

    [GeneratedRegex(@"\.\*{5,}\.")]
    private static partial Regex FiveStars();
}
