namespace Advent2021;

public partial class Day02 : Advent.Day
{
    public override void DoWork()
    {
        Dictionary<string, (int x, int y)> dirns = new() { { "down", (0, 1) }, { "up", (0, -1) }, { "back", (-1, 0) }, { "forward", (1, 0) } };
        int x = 0, y = 0, aim = 0;
        foreach (string instr in InputSplit)
        {
            string dirn = instr.Split(' ')[0]; int dist = int.Parse(instr.Split(' ')[1]); aim += dirns[dirn].y * dist;
            (x, y) = (x + (dirns[dirn].x * dist), y + (WhichPart == 1 ? (dirns[dirn].y * dist) : (dirns[dirn].x * dist * aim)));
        }
        Output = (x * y).ToString();
    }
}
