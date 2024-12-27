namespace Advent2017;

public partial class Day13 : Advent.Day
{
    public override void DoWork()
    {
        List<(int, int)> scanners = [];
        int score = 0, delay = 0;
        bool caught = false;

        foreach (string scanner in Inputs)
        {
            string[] bits = scanner.Split(": ");
            scanners.Add((int.Parse(bits[0]), int.Parse(bits[1])));
        }

        do
        {
            foreach ((int X, int Y) in scanners)
            {
                caught = (delay + X) % ((Y - 1) * 2) == 0;
                if (caught)
                {
                    score += X * Y;
                    if (Part2) break;
                }
            }
            delay++;
        } while (caught && Part2);

        Output = (Part1 ? score : delay - 1).ToString();
    }

}
