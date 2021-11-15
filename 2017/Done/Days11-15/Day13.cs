namespace Advent2017;

public partial class Day13 : Advent.Day
{
    public override void DoWork()
    {
        List<(int, int)> scanners = new();
        int score = 0, delay = 0;
        bool caught = false;

        foreach (string scanner in InputSplit)
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
                    if (WhichPart == 2) break;
                }
            }
            delay++;
        } while (caught && WhichPart != 1);

        Output = (WhichPart == 1 ? score : delay - 1).ToString();
    }

}
