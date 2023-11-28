namespace Advent2017;

public partial class Day22 : Advent.Day
{
    public override void DoWork()
    {
        (int X, int Y) curPos = (0, 0);
        int direction = 0, rowLen = Inputs[0].Length, maxSteps = Part1 ? 10_000 : 10_000_000, infections = 0;
        Dictionary<(int, int), int> grid = new();

        for (int col = 0; col < rowLen; col++)
            for (int row = 0; row < Inputs.Length; row++)
                grid[(row - ((Inputs.Length - 1) / 2), -col + ((rowLen - 1) / 2))] = Inputs[col][row] == '#' ? 2 : 0;

        for (int step = 0; step < maxSteps; step++)
        {
            if (!grid.ContainsKey(curPos))
                grid[curPos] = 0;

            direction = (direction + 3 + grid[curPos]) % 4;
            infections += (WhichPart - grid[curPos]) == 1 ? 1 : 0;
            grid[curPos] = (grid[curPos] + 3 - WhichPart) % 4;

            curPos.X -= direction % 2 == 0 ? 0 : direction - 2;
            curPos.Y -= direction % 2 != 0 ? 0 : direction - 1;
        }
        Output = infections.ToString();
    }
}
