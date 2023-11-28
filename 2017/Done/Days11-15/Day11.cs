namespace Advent2017;

public partial class Day11 : Advent.Day
{
    public override void DoWork()
    {
        (int x, int y) = (0, 0);
        int maxDist = 0, dist = 0;
        foreach (string move in Inputs[0].Split(','))
        {
            if (move.Length == 2)
                (x, y) = (x + (move[1] == 'e' ? 1 : -1), y + (move[0] == 'n' ? 1 : -1));
            else
                y += move[0] == 'n' ? 2 : -2;

            dist = Math.Abs(x) >= Math.Abs(y) ? Math.Abs(x) : (Math.Abs(x) + Math.Abs(y)) / 2;
            maxDist = Math.Max(maxDist, dist);
        }
        Output = Part1 ? dist.ToString() : maxDist.ToString();
    }
}
