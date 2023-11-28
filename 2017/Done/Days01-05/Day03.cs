namespace Advent2017;

public partial class Day03 : Advent.Day
{
    public override void DoWork()
    {
        (int X, int Y) pos = (0, 0);
        long num = 1, step = 1, target = InputNumbers[0][0];
        Dictionary<(int X, int Y), long> grid = new() { { pos, 1 } };
        do
        {
            if (num >= target) break;
            for (int i = 0; i < step; i++)
            {
                pos.X++;
                num = GetNum(WhichPart, num, pos, grid);
                grid.Add(pos, num);
                if (num >= target) break;
            }
            if (num >= target) break;

            for (int i = 0; i < step; i++)
            {
                pos.Y--;
                num = GetNum(WhichPart, num, pos, grid);
                grid.Add(pos, num);
                if (num >= target) break;
            }
            if (num >= target) break;
            step++;
            for (int i = 0; i < step; i++)
            {
                pos.X--;
                num = GetNum(WhichPart, num, pos, grid);
                grid.Add(pos, num);
                if (num >= target) break;
            }
            if (num >= target) break;
            for (int i = 0; i < step; i++)
            {
                pos.Y++;
                num = GetNum(WhichPart, num, pos, grid);
                grid.Add(pos, num);
                if (num >= target) break;
            }
            if (num >= target) break;

            step++;
        } while (true);

        Output = Part1 ? (Math.Abs(pos.X) + Math.Abs(pos.Y)).ToString() : num.ToString();
    }

    private static long GetNum(int whichPart, long num, (int X, int Y) pos, Dictionary<(int, int), long> grid)
    {
        if (whichPart == 1) return num + 1;

        long value = 0;
        for (int x = -1; x <= 1; x++)
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;
                if (grid.ContainsKey((pos.X + x, pos.Y + y)))
                    value += grid[(pos.X + x, pos.Y + y)];
            }
        return value;
    }
}
