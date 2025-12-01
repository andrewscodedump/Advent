namespace Advent2020;

public partial class Day12 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs

        (int px, int py) = (0, 0); (int wx, int wy) = (10, 1);
        int heading = 90;
        Dictionary<char, (int x, int y)> dirns = new() { { 'N', (0, 1) }, { 'S', (0, -1) }, { 'E', (1, 0) }, { 'W', (-1, 0) } };
        Dictionary<int, (int x, int y)> headings = new() { { 0, (0, 1) }, { 90, (1, 0) }, { 180, (0, -1) }, { 270, (-1, 0) } };
        Dictionary<char, int> turns = new() { { 'L', -1 }, { 'R', 1 } };

        #endregion Setup Variables and Parse Inputs

        foreach (string instr in Inputs)
        {
            char cmd = instr[0];
            int num = int.Parse(instr[1..^0]);
            if (Part1)
            {
                if (dirns.TryGetValue(cmd, out (int x, int y) value))
                    (px, py) = (px + (value.x * num), py + (value.y * num));
                if (cmd == 'F')
                    (px, py) = (px + (headings[heading].x * num), py + (headings[heading].y * num));
                if (turns.TryGetValue(cmd, out int value2))
                    heading = (heading + 360 + (value2 * num)) % 360;
            }
            else
            {
                if (dirns.TryGetValue(cmd, out (int x, int y) value))
                    (wx, wy) = (wx + (value.x * num), wy + (value.y * num));
                if (cmd == 'F')
                    (px, py) = (px + (wx * num), py + (wy * num));
                if (turns.TryGetValue(cmd, out int value2))
                {
                    (int sin, int cos) = headings[(360 + (value2 * num)) % 360];
                    (wx, wy) = ((wx * cos) + (wy * sin), (wy * cos) - (wx * sin));
                }
            }
        }
        Output = (Math.Abs(px) + Math.Abs(py)).ToString();
    }
}
